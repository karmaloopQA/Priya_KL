using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;

namespace KarmaLoop.KL
{
    class ViewBag : ViewProduct
    {
        #region Public Methods

        public ViewBag(IWebDriver driver) : base(driver) { }

        /// <summary> Function to verify View Bag page. </summary>
        /// <returns> ViewBag Driver </returns>
        public ViewBag VerifyShoppingBagPage()
        {
            Results.WriteDescription("Verify Text/URL on Shopping bag Page.");
            Assert.AreEqual("https://www.karmaloop.com/cart#view", driver.Url, "URL doesn't matched.");
            Results.WriteStatus("Pass", "Page Verified.");

            return new ViewBag(driver);
        }

        /// <summary> Function to click Continue Shopping button. </summary>
        /// <returns> Products Driver </returns>
        public KarmaLoop.KL.Products ClickContinueShopping()
        {
            Results.WriteDescription("Click on 'Continue Shopping' link.");
            driver._Click("id", "continue-shopping-1");
            Results.WriteStatus("Pass", "Link clicked.");

            return new Products(driver);
        }

        /// <summary> Function to click Checkout With Creditcard when Existing User needs to Login. </summary>
        /// <returns> AccountSignIn Driver </returns>
        public AccountSignIn ClickCheckOutWithCreditCard_AsExistingUser()
        {
            ClickCheckOutWithCC();

            return new AccountSignIn(driver);
        }

        /// <summary> Function to click Checkout With Creditcard when trying to buy as guest user. </summary>
        /// <returns> AccountSignIn Driver </returns>
        public AccountSignIn ClickCheckOutWithCreditCard_AsGuestUser()
        {
            ClickCheckOutWithCC();

            return new AccountSignIn(driver);
        }

        /// <summary> Function to click Checkout With Creditcard when User is already logged In. </summary>
        /// <returns> AccountSignIn Driver </returns>
        public Shipping ClickCheckOutWithCreditCard()
        {
            ClickCheckOutWithCC();

            if (!(driver._IsElementPresent("id", "ship-form") && driver._VerifyText("xpath", "//*[@id='ship-form']/div[1]/h2", "Shipping Address")))
            {
                Common.comingBackToPreviousStep = true;
                driver._Click("xpath", "//*[@id='breadcrumbs']/li[2]/a");
            }

            return new Shipping(driver);
        }

        /// <summary> Function to Confirm no. of products available in the Bag. </summary>
        /// <returns> View Bag Driver </returns>
        public ViewBag ConfirmProductInBag()
        {


            return new ViewBag(driver);
        }

        /// <summary> Function to verify total amounts in the bag. </summary>
        /// <returns> View Bag Driver </returns>
        public void VerifyBagTotal()
        {
            string[] subTotal = GetProductsSubTotal();
            float count = 0;
            for (int i = 0; i < subTotal.Length; i++)
            {
                subTotal[i] = subTotal[i].Trim().Replace('$', ' ');
                //count = Convert.to
            }
        }

        #endregion

        #region Private Methods

        /// <summary> Obsolute </summary>
        /// <returns> View Bag Driver </returns>
        private object ClickCheckOutWithCreditCard_obsolute()
        {
            ClickCheckOutWithCC();
            // for (int i = 0; i < 60; i++)
            //{
            //    Console.WriteLine("Wait: " + i);
            //    if (driver._IsElementPresent("xpath", "//*[@id='cart-header']/div/div[2]/div/a"))
            //        System.Threading.Thread.Sleep(1000);
            //    else
            //        break;
            //}
            if (driver._IsElementPresent("id", "account-header") && driver._VerifyText("xpath", "//*[@id='account-header']/h1", "Account Sign-in"))
            {
                Console.WriteLine("In Acct");
                return new AccountSignIn(driver);
            }
            else if (driver._IsElementPresent("id", "ship-form") && driver._VerifyText("xpath", "//*[@id='ship-form']/div[1]/h2", "Shipping Address"))
            {
                Console.WriteLine("In shipp");
                return new Shipping(driver);
            }
            else if (driver._IsElementPresent("id", "bill-form") && driver._VerifyText("xpath", "//*[@id='bill-form']/div[2]/h2", "Billing Address"))
            {
                Console.WriteLine("In shipp");
                return new Payment(driver);
            }
            else
                return new ReviewYourOrder(driver);
        }

        /// <summary> Function to get no. of products available in the Bag. </summary>
        /// <returns> NoOfProducts </returns>
        private int GetNoOfProducts()
        {
            //Results.WriteDescription("Get No. of Products available in Bag");
            int count = driver._FindElements("class", "product").Count;
            //Results.WriteStatus("Pass", "There are " + count + " Products available in Bag.");

            return count;
        }

        /// <summary> Function to Click checkout with credit card. </summary>
        private void ClickCheckOutWithCC()
        {
            Results.WriteDescription("Check whether the items are available in Shopping Bag or not?");
            Assert.AreEqual(false, driver._IsElementPresent("id", "cartEmpty") && driver._VerifyText("xpath", "//*[@id='cartEmpty']/h3", "Your bag is empty."), "Your bag is empty.");
            Results.WriteStatus("Pass", "Items are already added in the bag. No. of Items added: " + GetNoOfProducts());

            Results.WriteDescription("Click on 'Check out With Credit Card' Button.");
            driver._Click("xpath", "//*[@id='cart-header']/div/div[2]/div/a");
            driver._WaitForElementToBeHidden("xpath", "//*[@id='cart-header']/div/div[2]/div/a",60);
            Results.WriteStatus("Pass", "Button clicked.");
        }

        /// <summary> Function to click remove all items link. </summary>
        private void ClickRemoveAllItems()
        {
            Results.WriteDescription("Click on 'Check out With Credit Card' Button.");
            IList<IWebElement> link = driver._FindElements("class", "remove-item clearfix");
            int count = link.Count;
            foreach (IWebElement e in link)
            {
                e.Click();
                driver._Wait(2);
                Assert.AreEqual(count - 1, driver._FindElements("class", "remove-item clearfix").Count);
                count--;
            }
            Assert.AreEqual(0, driver._FindElements("class", "remove-item clearfix").Count);
            Results.WriteStatus("Pass", "All the Items are removed from the bag.");
        }

        /// <summary> Function to click checkout with paypal. </summary>
        private void ClickCheckOutWithPayPal()
        {
            Results.WriteDescription("Click on 'Check out with PayPal' Button.");
            driver._Click("class", "cart-paypal-btn");
            Results.WriteStatus("Pass", "Button clicked.");
        }

        /// <summary> Function to get Information of all the Items available in the Bag. </summary>
        /// <returns> string array which contains information </returns>
        private string[,] GetProductsInfo()
        {
            int noOfProducts = GetNoOfProducts();
            string[,] products = new string[9*noOfProducts, 2];
            string path = string.Empty;
            int j = 0;
            for (int i = 1; i <= noOfProducts; i++)
            {
                path = "//*[@id='cart-items']/table/tbody/tr[" + i.ToString() + "]";
                products[j, 0] = "Brand";
                products[j++, 1] = driver._GetText("xpath", path, "class", "brand");
                products[j, 0] = "Title";
                products[j++, 1] = driver._GetText("xpath", path, "class", "product-title");
                products[j, 0] = "Size";
                products[j++, 1] = driver._GetText("xpath", path, "xpath", "//*[@class=product']/h5[1]/span");
                products[j, 0] = "Color";
                products[j++, 1] = driver._GetText("xpath", path, "xpath", "//*[@class=product']/h5[2]/span");
                products[j, 0] = "SKU";
                products[j++, 1] = driver._GetText("xpath", path, "class", "sku");
                products[j, 0] = "Item Status";
                products[j++, 1] = driver._GetText("xpath", path, "xpath", "//*[@class='status']/p");
                products[j, 0] = "Quantity";
                products[j++, 1] = driver._GetValue("xpath", path, "name", "quantity");
                products[j, 0] = "Price";
                products[j++, 1] = driver._GetValue("xpath", path, "xpath", "//*[@class='price']/p");
                products[j, 0] = "Item Total";
                products[j++, 1] = driver._GetValue("xpath", path, "class", "subtotal");
            }
            return products;
        }

        /// <summary> Function to get the value of specific information of any Item. </summary>
        /// <returns> value of requested field </returns>
        private string FindFieldValue(string field)
        {
            string value = string.Empty;
            string[,] values = GetProductsInfo();

            return value;
        }

        /// <summary> Function to get Information of all the Items available in the Bag. </summary>
        /// <returns> string array which contains information </returns>
        private string[] GetProductsSubTotal()
        {
            int noOfProducts = GetNoOfProducts();
            string[] subTotal = new string[noOfProducts];
            string path = string.Empty;
            int j = 0;
            for (int i = 1; i <= noOfProducts; i++)
            {
                path = "//*[@id='cart-items']/table/tbody/tr[" + i.ToString() + "]";
                subTotal[j++] = driver._GetValue("xpath", path, "class", "subtotal");
            }
            return subTotal;
        }

        /// <summary> Function to get total amount in the Bag. </summary>
        /// <returns> total amount </returns>
        private string GetBagTotal()
        {
            return driver._GetText("id", "subtotal", "class", "value");
        }

        #endregion
    }
}
