using System;
using OpenQA.Selenium;
using NUnit.Framework;
using System.Collections.Generic;

namespace KarmaLoop.KL
{
    class Home : TopHeader
    {
        #region Public Methods

        public Home(IWebDriver driver) : base(driver) {   }

        /// <summary>Function to verify Home page.</summary>
        /// <returns>Home Driver</returns>
        public Home VerifyHomePage()
        {
            Results.WriteDescription("Verify Text/URL on Home Page.");
            Assert.AreEqual(true, driver._IsElementPresent("id", "logo"), "Home Page not displayed, try again");
            Results.WriteStatus("Pass", "Application Launched successfully");

            return new Home(driver);
        }

        /// <summary> Function to view specific product.</summary>
        /// <returns> View Product Driver</returns>
        public ViewProduct ViewSpecificProduct(string productID)
        {
            Results.WriteDescription("Open product page of ProductID: " + productID);
            driver.Navigate().GoToUrl(Common.KarmaloopTestSite + "product/" + productID);
            driver._Wait(1);
            Assert.AreEqual(Common.KarmaloopTestSite + "product/" + productID, driver._GetUrl());
            Results.WriteStatus("Pass", "Product page successfully Opened.");

            return new ViewProduct(driver);
        }

        /// <summary> Function to Launch website home page.</summary>
        /// <returns>Home Driver</returns>
        public Home NavigateToApplication(bool closeSubscribePopup = true)
        {
            Results.WriteDescription("Launching a application - " + Common.KarmaloopTestSite);
            driver.Manage().Cookies.DeleteAllCookies();
            driver.Navigate().GoToUrl(Common.KarmaloopTestSite);
            driver._Wait(1);
            Assert.AreEqual(Common.KarmaloopTestSite, driver._GetUrl());
            Results.WriteStatus("Pass", "Application Launched successfully");
            if (closeSubscribePopup)
            {               
                if (driver.WaitForElement("id", "monetate_emailSocialLbx", 5))
                {
                    Results.WriteDescription("Closing 'Subscribe to E-List' pop up.");
                    if (Common.WebBrowser.ToLower().Equals("chrome"))
                    {
                        driver._sendKeys("name", "Email","esc");
                        //((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].type ='" + Keys.Escape + "';", driver.FindElement(By.Id("monetate_emailSocialLbx")));
                    }
                    else
                        driver._sendKeys("id", "monetate_emailSocialLbx", "esc");
                    
                    System.Threading.Thread.Sleep(2000);
                    Assert.AreEqual(false, driver._IsElementPresent("id", "monetate_emailSocialLbx"),"Pop up is still present.");
                    Results.WriteStatus("Pass", "pop up closed.");    
                }
            }
         
            return new Home(driver);
        }

        /// <summary> Function to click a link from "New Style" panel on Home page.</summary>
        /// <returns> Products Driver</returns>
        public Products ClickNewStyle(string styleName = "any")
        {
            Results.WriteDescription("Check whether the 'New Styles' panel is displayed or not?");
            
            if (!driver._VerifyText("xpath", "//*[@class='new-summary-content']/h3", "NEW STYLES"))
            {
                Results.WriteStatus("Pass with warning", "'New Styles' panel is not available on Home page.");
            }
            else
            {
                IList<IWebElement> newStyleBrands = driver._FindElementsFromElement("id", "new-styles", "class", "new-styles-brand");
                if (newStyleBrands.Count == 0)
                {
                    Results.WriteStatus("Pass with warning", "'New Styles' panel does displayed, but brands are not found in that panel.");
                }
                else
                {
                    Results.WriteStatus("Pass", "'New Styles' panel does displayed and contains " + newStyleBrands.Count + " brands");
                    int rand = 0;
                    if (styleName.Trim().Equals("any") || styleName.Trim().Equals(""))
                    {
                        rand = new Random().Next(0, newStyleBrands.Count);
                        styleName = newStyleBrands[rand].Text;
                    }
                    Results.WriteDescription("Click on '" + styleName + "' brand link to view its product.");
                    string href = driver._getHref("linktext", styleName.Trim());
                    driver._Click("linktext", styleName.Trim());
                    Assert.AreEqual(href, driver._GetUrl(), "Link clicked but not redirected to expected URL.");
                    Results.WriteStatus("Pass", "Link clicked.");
                }
            }

            return new Products(driver);
        }

        /// <summary> Function to Join Elist from Home page.</summary>
        /// <returns> Home Driver</returns>
        public Home JoinOurEList()
        {
            Results.WriteDescription("Enter valid Email address, phone and click subscribe");
            driver._Type("id", "Email", "testing527.test@gmail.com");
            driver._Type("id", "Phone", "1231231234");
            // male-radio-select     => Male
            // female-radio-select   => Female
            driver._SelectRadioOption("id", "male-radio-select");
            driver._Click("id", "subscribe-submit");
            Assert.AreEqual(false, driver._IsElementPresent("class", "error"), "Received Validation message: " + driver._GetText("class", "error"));
            Results.WriteStatus("Pass", "Subscribe button clicked and no validation message displayed as expected.");

            Results.WriteDescription("Verify that the Success message is displayed or not?");
            System.Threading.Thread.Sleep(5000);
            string successMessage = "Thank you for signing up for the Karmaloop Newsletter!";
            string alreadySignedMessage = "Hey!  You're already signed up for our elist!";
            string receivedMessage = driver._GetText("id", "subscribe-dialog-message");
            Assert.AreEqual(true, receivedMessage.Trim().Equals(successMessage) || receivedMessage.Trim().Equals(alreadySignedMessage), "Expected success message isn't displayed " + receivedMessage);
            driver._Click("id", "ui-button-text");
            Results.WriteStatus("Pass", "Successfully Subscribed to e-list.");

            return new Home(driver);
        }

        /// <summary> Function to view any random product from Home page.</summary>
        /// <returns> ViewProduct Driver </returns>
        public ViewProduct ViewProduct()
        {
            Results.WriteDescription("Check whether the products are displayed on Home Page or not?");
            IList<IWebElement> products = driver._FindElements("class", "browseImage");
            Assert.Greater(products.Count, 0, "Products are not found.");
            Results.WriteStatus("Pass", "No. of Products Found: " +products.Count);

            Results.WriteDescription("Click on any random product to view");
            int rand = new Random().Next(0, products.Count);
            string expectedTitle = products[rand].GetAttribute("alt");
            string productID = products[rand].GetAttribute("data-productid");
            products[rand].Click();
            driver._Wait(1);
            string actualTitle = driver.Title;
            actualTitle = ((actualTitle.Split(':')[0]).Replace(',',' ').Replace('&',' '));
            // Assert.AreEqual(true, actualTitle.Trim().ToLower().Equals(expectedTitle.Trim().ToLower()), "Expected Title: " + expectedTitle + "\nActual Title: " + actualTitle + ", ProductID: " + productID);
            Assert.AreEqual(true, driver.Url.EndsWith(productID),"Expected Page is Not reched.");
            Results.WriteStatus("Pass", "ProductID: " + productID + " clicked.");

            return new ViewProduct(driver);
        }

        /// <summary> Function to display information of all the products available on Home page.</summary>
        /// <returns> Home Driver </returns>
        public Home displayInfoAllTheProducts()
        {
            IList<IWebElement> products = driver._FindElements("class", "thumbnail-wrap");
            Console.WriteLine("No. of Products: " + products.Count);
            IList<IWebElement> productDetails = driver._FindElements("class", "thumbnail-details");
            string[] productUrls = new string[products.Count];
            string[] productIds = new string[products.Count];
            string[] productAltTitles = new string[products.Count];
            string[] productBrands = new string[products.Count];
            string[] productDescriptions = new string[products.Count];
            string[] productPrice = new string[products.Count];
            string[,] productTags = new string[products.Count, 5];
            int i = 0;
            foreach (IWebElement ele in products)
            {
                IWebElement innerEle = ele._FindElementsWithinElement("tagname", "img")[0];
                productIds[i] = innerEle.GetAttribute("data-productid");
                productAltTitles[i] = innerEle.GetAttribute("alt");
                int k = 0;
                IList<IWebElement> tagList = ele._FindElementsWithinElement("tagname","span");
                productTags[i, k++] = tagList.Count.ToString();
                foreach (IWebElement tag in tagList)
                {
                    productTags[i, k++] = tag.Text;
                }
                i++;
            }

            string detURL = string.Empty;
            i = 0;
            foreach (IWebElement details in products)
            {
                IList<IWebElement> pDetails = details._FindElementsWithinElement("tagname", "a");
                productUrls[i] = pDetails[0].GetAttribute("href");
                productBrands[i] = pDetails[1].Text;
                productDescriptions[i] = pDetails[2].Text;
                if (pDetails.Count > 4 && pDetails.Count <= 6)
                    productPrice[i] = pDetails[3].Text + " (" + pDetails[3].GetAttribute("class") + "), " + pDetails[4].Text + " (" + pDetails[4].GetAttribute("class") + "), " + pDetails[5].Text + " (" + pDetails[5].GetAttribute("class") + ")";
                else
                    productPrice[i] = pDetails[3].Text;
                i++;
            }


            for (i = 0; i < products.Count; i++)
            {
                Console.WriteLine("\n======Product #" + (i + 1) + "======\n");
                Console.WriteLine("Product ID: " + productIds[i]);
                string tags = string.Empty;
                int x = Convert.ToInt32(productTags[i, 0]);
                if(x != 0)
                {
                    for (int j = 1; j <= x; j++)
                    {
                        tags = tags + productTags[i,j] + ", ";
                    }
                    tags = tags.Remove(tags.Trim().Length-1);
                }
                Console.WriteLine("Product Tag(s): " + tags);
                Console.WriteLine("Product Url: " + productUrls[i]);
                Console.WriteLine("Product Title: " + productAltTitles[i]);
                Console.WriteLine("Product Brand: " + productBrands[i]);
                Console.WriteLine("Product Description: " + productDescriptions[i]);
                Console.WriteLine("Product Price: " + productPrice[i]);
            }
            return new Home(driver);
        }

        #endregion
    }
}
