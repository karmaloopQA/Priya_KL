using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;

namespace KarmaLoop.KL
{
    class Women : TopHeader
    {
        #region Public Methods

        public Women(IWebDriver driver) : base(driver) {  }

        public Products SelectBrandForWomen()
        {
            string xpath = "//*[@class='mini-cart-wrapper']/ul/li[8]/select[1]";

            Results.WriteDescription("Check whether the 'Women's Brands' drop list is available in Nav bar.");
            Assert.AreEqual(true, driver._IsElementPresent("xpath", xpath), "Element Not Found.");
            IList<IWebElement> brands = driver._GetDropDownOptions("xpath", xpath);
            Assert.AreNotEqual(0, brands.Count, "No Brands available.");
            Results.WriteStatus("Pass", "Drop List available and it contains " + brands.Count + " brands.");

            Results.WriteDescription("Select any random brand from 'Women's Brands' drop list.");
            int rand = new Random().Next(0, brands.Count);
            brands[rand].Click();
            Results.WriteStatus("Pass", "'" + brands[rand] + "' brand chosen.");

            return new Products(driver);
        }

        public Products SelectCategoryForWomen()
        {
            string xpath = "//*[@class='mini-cart-wrapper']/ul/li[8]/select[2]";

            Results.WriteDescription("Check whether the 'Women's Categories' drop list is available in Nav bar.");
            Assert.AreEqual(true, driver._IsElementPresent("xpath", xpath), "Element Not Found.");
            IList<IWebElement> categories = driver._GetDropDownOptions("xpath", xpath);
            Assert.AreNotEqual(0, categories.Count, "No Categories available.");
            Results.WriteStatus("Pass", "Drop List available and it contains " + categories.Count + " categories.");

            Results.WriteDescription("Select any random brand from 'Women's Categories' drop list.");
            int rand = new Random().Next(0, categories.Count);
            categories[rand].Click();
            Results.WriteStatus("Pass", "'" + categories[rand] + "' category chosen.");

            return new Products(driver);
        }

        public Products ClickNewForWomen()
        {
            Results.WriteDescription("Check whether the 'New' link available for Women or not? and click(if available)");
            Assert.AreEqual(true, driver._IsElementPresent("xpath", "//*[@class='mini-cart-wrapper']/ul/li[6]/a"), "Link isn't available.");
            driver._Click("xpath", "//*[@class='mini-cart-wrapper']/ul/li[6]/a");
            Results.WriteStatus("Pass", "Link is available and clicked successfully.");

            return new Products(driver);
        }

        public Products ClickSaleForWomen()
        {
            Results.WriteDescription("Check whether the 'Sale' link available for Women or not? and click(if available)");
            Assert.AreEqual(true, driver._IsElementPresent("xpath", "//*[@class='mini-cart-wrapper']/ul/li[7]/a"), "Link isn't available.");
            driver._Click("xpath", "//*[@class='mini-cart-wrapper']/ul/li[7]/a");
            Results.WriteStatus("Pass", "Link is available and clicked successfully.");

            return new Products(driver);
        }

        #endregion

        #region Private Methods

        private void VerifyWomenNavBar()
        {
            Results.WriteDescription("Verify that the Navigation Links are available for Women.");
            Assert.AreEqual(true, driver._IsElementPresent("class", "mini-cart-wrapper") && driver._IsElementPresent("id", "girls"), "Navigation links are not available.");
            Results.WriteStatus("Pass", "Navigation Links available for Women.");
        }

        #endregion
    }
}
