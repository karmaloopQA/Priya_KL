using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using NUnit.Framework;

namespace KarmaLoop.KL
{
    class ViewProduct : Products
    {
        #region Public Methods

        public ViewProduct(IWebDriver driver) : base(driver) { }

        /// <summary> Function to verify View Product page. </summary>
        /// <returns> ViewProduct Driver </returns>
        public ViewProduct VerifyViewProductPage()
        {
            Results.WriteDescription("Verify Text/URL on Product page");
            Assert.AreEqual(true, driver._VerifyPartialURL("http://www.karmaloop.com/product/"), "URL doesn't matched.");
            Assert.AreEqual(false, driver._IsElementPresent("id", "x-404-message"),"Received 404 error message. ProductID: " + Common.productID);
            Results.WriteStatus("Pass", "Searched page verified successfully!");

            return new ViewProduct(driver);
        }

        /// <summary> Function to select a size and then clicks Add To Shopping Bag button. </summary>
        /// <returns> ViewProduct Driver </returns>
        public ViewProduct SelectSizeAndClickAddToShoppingBag()
        {
            // verifyProductPage();
            SelectProductSize();
            ClickAddToShoppingBagButton();

            return new ViewProduct(driver);
        }

        /// <summary>Function to view Shopping Bag.</summary>
        /// <returns>Home Driver</returns>
        public ViewBag OpenShoppingBag()
        {          
            return new TopHeader(driver).ClickViewBag();
        }

        /// <summary>Function to click Add to Shopping Bag button.</summary>
        /// <returns>Home Driver</returns>
        public ViewProduct ClickAddToShoppingBagButton()
        {
            Results.WriteDescription("Click on 'Add To Shopping bag' button.");
            int countInBag = Convert.ToInt32(driver._GetText("id", "mini-cart-details"));
            driver._Click("id", "addtocartButton");
            if (driver._IsElementPresent("xpath", "//*[@id='size-label']/span") && driver._VerifyText("xpath", "//*[@id='size-label']/span", "Please select a size"))
            {
                driver._SelectRandomElementFromElements("id", "sizes", "class", "size");
                driver._Click("id", "addtocartButton");
            }
            Assert.AreEqual(countInBag + 1, Convert.ToInt32(driver._GetText("id", "mini-cart-details")), "Items count in Bag isn't incremented.");
            Results.WriteStatus("Pass", "Item added successfully.");

            return new ViewProduct(driver);
        }

        /// <summary> Function to click EMail a Friend Button.</summary>
        /// <returns> EmailAFriend Driver</returns>
        public EmailAFriend ClickEmailAFriendButton()
        {
            Results.WriteDescription("Click on 'Email A Friend' button.");
            driver._Click("class", "email-product-button");
            Results.WriteStatus("Pass", "Button clicked successfully.");

            return new EmailAFriend(driver);
        }

        /// <summary> Function to click on Add to Wish List button when user already logged in. </summary>
        /// <returns> View Product Driver </returns>
        public ViewProduct ClickAddToWishList_WhenAlreadyLoggedIn()
        {
            SelectProductSize();
            ClickAddToWishListButton();
            IsProductAddedToWishList();

            return new ViewProduct(driver);
        }

        /// <summary> Function to click on Add to Wish List button when user needs to log in. </summary>
        /// <returns> View Product Driver </returns>
        public ViewProduct ClickAddToWishList_WhenLoginNeeded()
        {
            SelectProductSize();
            ClickAddToWishListButton();
            new AddToWishList(driver).LoginAsExistingMember();
            IsProductAddedToWishList();

            return new ViewProduct(driver);
        }

        /// <summary> Function to click on Add to Wish List button when user needs to create new Account. </summary>
        /// <returns> View Product Driver </returns>
        public ViewProduct ClickAddToWishList_WhenNewAccountNeeded()
        {
            SelectProductSize();
            ClickAddToWishListButton();
            new AddToWishList(driver).CreateNewAccount();
            IsProductAddedToWishList();

            return new ViewProduct(driver);
        }
        
        #endregion

        #region Private Methods

        private string GetProductBrand() { return driver._GetText("id", "brand"); }

        private string GetProductTitle() { return driver._GetText("id", "title"); }

        private string GetProductPrice() { return driver._GetText("class", "price"); }

        private string[] GetProductSizes() 
        {
            IList<IWebElement> sizesEle = driver._FindElementsFromElement("id", "sizes", "class", "size");
            string[] sizes = new string[sizesEle.Count];
            int i = 0;
            foreach (IWebElement e in sizesEle)
            {
                sizes[i++] = e.Text;
            }

            return sizes;
        }

        private string SelectProductSize(string size = "any")
        {
            Results.WriteDescription("Select " + size + "size.");
            int count = GetProductSizes().Length;
            Assert.AreNotEqual(0,count ,"size not available");
            if (count != 1)
            {
                if (size.ToLower().Equals("any"))
                    driver._SelectRandomElementFromElements("id", "sizes", "class", "size");
                else
                    driver._SelectSpecificElementFromElements("id", "sizes", "class", "size", size);
            }
            driver._Wait(2);
            Results.WriteStatus("Pass", "size chosen.");

            return size;
        }

        private int DisplayProductSizes()
        {
            Results.WriteDescription("Display all the Size options available for viewing Product.");
            string[] sizes = GetProductSizes();
            string s = string.Empty;
            bool inFor = false;
            for (int i = 0; i < sizes.Length; i++)
            {
                s = s + sizes[i] + ", ";
                inFor = true;
            }

            if(inFor)
                s.Remove(s.Length - 1);

            Assert.AreNotEqual(0, sizes.Length,"Size not available.");
            Results.WriteStatus("Pass", "Size Options available: " + s);

            return sizes.Length;
        }

        private string GetProductColors() { return driver._GetText("id", "title"); }

        private string GetProductDescription() { return driver._GetText("xpath", "//*[@id='info']/div"); }

        private void EnterProductQuantity(int quantity)
        {
            Results.WriteDescription("Enter quantity: " + quantity.ToString());
            driver._Type("id", "quantity-input",quantity.ToString());
            Results.WriteStatus("Pass", "Value Entered.");
        }

        private void ClickAddToWishListButton()
        {
            //SelectProductSize();
            Results.WriteDescription("Click on 'Add To Wish List' button.");
            driver._Click("id", "add-to-wish-list");
            Results.WriteStatus("Pass", "Button clicked successfully.");
        }

        private void IsProductAddedToWishList()
        {
            Results.WriteDescription("Check whether the button caption updated to 'ADDED TO WISH LIST'");
            for (int i = 0; i < 20; i++)
            {
                string text = driver._GetText("id", "add-to-wish-list");
                //Console.WriteLine("Text " + (i+1) + " : " + text);
                if (text.ToLower().Equals("added to wish list"))
                    break;
            }
            Assert.AreEqual(true, driver._VerifyText("id", "add-to-wish-list", "added to wish list"), "Actual: " + driver._GetText("id", "add-to-wish-list") + "\nExpected: ADDED TO WISH LIST");
            Results.WriteStatus("Pass", "Button caption updated successfully.");
        }

        #endregion
    }
}
