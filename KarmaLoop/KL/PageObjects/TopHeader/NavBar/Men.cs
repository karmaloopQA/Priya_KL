﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;

namespace KarmaLoop.KL
{
    class Men : TopHeader
    {
        #region Public Methods

        public Men(IWebDriver driver): base(driver) { }

        private void verifyMenNavBar()
        {
            Results.WriteDescription("Verify that the Navigation Links are available for Men.");
            Assert.AreEqual(true, driver._IsElementPresent("class", "mini-cart-wrapper") && driver._IsElementPresent("id", "guys"), "Navigation links are not available.");
            Results.WriteStatus("Pass", "Navigation Links available for Men.");
        }

        public Products selectBrandForMen()
        {
            string xpath = "//*[@class='mini-cart-wrapper']/ul/li[4]/select[1]";

            Results.WriteDescription("Check whether the 'Men's Brands' drop list is available in Nav bar.");
            Assert.AreEqual(true, driver._IsElementPresent("xpath", xpath), "Element Not Found.");
            IList<IWebElement> brands = driver._GetDropDownOptions("xpath", xpath);
            Assert.AreNotEqual(0, brands.Count, "No Brands available.");
            Results.WriteStatus("Pass", "Drop List available and it contains " + brands.Count + " brands.");

            Results.WriteDescription("Select any random brand from 'Men's Brands' drop list.");
            int rand = new Random().Next(0, brands.Count);
            brands[rand].Click();
            Results.WriteStatus("Pass", "'" + brands[rand] + "' brand chosen.");


            return new Products(driver);
        }

        public Products selectCategoryForMen()
        {
            string xpath = "//*[@class='mini-cart-wrapper']/ul/li[4]/select[2]";

            Results.WriteDescription("Check whether the 'Men's Categories' drop list is available in Nav bar.");
            Assert.AreEqual(true, driver._IsElementPresent("xpath", xpath), "Element Not Found.");
            IList<IWebElement> categories = driver._GetDropDownOptions("xpath", xpath);
            Assert.AreNotEqual(0, categories.Count, "No Categories available.");
            Results.WriteStatus("Pass", "Drop List available and it contains " + categories.Count + " categories.");

            Results.WriteDescription("Select any random brand from 'Men's Categories' drop list.");
            int rand = new Random().Next(0, categories.Count);
            categories[rand].Click();
            Results.WriteStatus("Pass", "'" + categories[rand] + "' category chosen.");
            return new Products(driver);
        }

        public Products clickNewForMen()
        {
            Results.WriteDescription("Check whether the 'New' link available for Men or not? and click(if available)");
            Assert.AreEqual(true, driver._IsElementPresent("xpath", "//*[@class='mini-cart-wrapper']/ul/li[2]/a"), "Link isn't available.");
            driver._Click("xpath", "//*[@class='mini-cart-wrapper']/ul/li[2]/a");
            Results.WriteStatus("Pass", "Link is available and clicked successfully.");

            return new Products(driver);
        }

        public Products clickSaleForMen()
        {
            Results.WriteDescription("Check whether the 'Sale' link available for Men or not? and click(if available)");
            Assert.AreEqual(true, driver._IsElementPresent("xpath", "//*[@class='mini-cart-wrapper']/ul/li[3]/a"), "Link isn't available.");
            driver._Click("xpath", "//*[@class='mini-cart-wrapper']/ul/li[3]/a");
            Results.WriteStatus("Pass", "Link is available and clicked successfully.");

            return new Products(driver);
        }

        #endregion
    }
}
