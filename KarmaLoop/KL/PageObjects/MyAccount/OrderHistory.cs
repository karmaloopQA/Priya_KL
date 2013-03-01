using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;

namespace KarmaLoop.KL
{
    class OrderHistory : MyAccount
    {
        public OrderHistory(IWebDriver driver) : base(driver) { }

        public OrderHistory verifyOrderHistory()
        {
            Results.WriteDescription("Verify Text/URL on Order History page.");
            Assert.AreEqual(4, driver._FindElementsFromElement("class", "account-menu", "tagname", "a"), "My Account menu items are not visible");
            Results.WriteStatus("Pass", "Text verified.");

            return new OrderHistory(driver);
        }
    }
}
