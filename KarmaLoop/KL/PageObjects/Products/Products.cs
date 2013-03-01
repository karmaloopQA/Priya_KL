using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using NUnit.Framework;

namespace KarmaLoop.KL
{
    class Products : Home
    {
        #region Public Methods

        public Products(IWebDriver driver) : base(driver) { }

        /// <summary> Function to verify products page after searching. </summary>
        /// <returns> Products Driver </returns>
        public Products VerifyProductsPage_afterSearch()
        { 
            Results.WriteDescription("Verify that the keyword searched page redirected correctly or not?");
            string str = driver._GetText("id", "num-results").Split('"')[1];
            Assert.AreEqual(Common.searchedKeyword, str);
            Results.WriteStatus("Pass", "Searched page verified successfully!");

            return new Products(driver);
        }

        /// <summary> Function to verify products page. </summary>
        /// <returns> Products Driver </returns>
        public Products VerifyProductsPage()
        {
            Results.WriteDescription("Verify Text/URL on Products Page.");
            Assert.AreEqual(true, driver._VerifyPartialURL("http://www.karmaloop.com/browse"));
            Results.WriteStatus("Pass", "Products page verified successfully!");

            return new Products(driver);
        }

        /// <summary> Function to open products page by redirecting to http://www.karmaloop.com/browse page. </summary>
        /// <returns> Products Driver </returns>
        public Products NavigatesToProductsPage()
        {
            Results.WriteDescription("Browse products page");
            driver.Navigate().GoToUrl("http://www.karmaloop.com/browse");
            driver._Wait(1);
            Results.WriteStatus("Pass", "Products page reached successfully");

            return new Products(driver);
        }

        /* public Mens NavigateToMensPage()
        {
            Results.WriteDescription("Select Men's category on page");
            driver.FindElement(By.LinkText("MEN")).Click();
            Results.WriteStatus("Pass", "Men's category selected successfully!");

            return new Mens(driver);
        }
        public Womens NavigateToWomensPage()
        {
            Results.WriteDescription("Select Women's category on page");
            driver.FindElement(By.LinkText("WOMEN")).Click();
            Results.WriteStatus("Pass", "Women's category selected successfully!");

            return new Womens(driver);
        }*/

        #endregion
    }
}
