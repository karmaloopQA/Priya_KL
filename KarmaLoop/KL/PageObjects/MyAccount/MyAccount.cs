using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;

namespace KarmaLoop.KL
{
    class MyAccount : Home
    {
        public MyAccount(IWebDriver driver) : base(driver) { }

        public MyAccount verifyMyAccountMenu()
        {
            Results.WriteDescription("Verify My Account menu open and items are visible.");
            Assert.AreEqual(4, driver._FindElementsFromElement("class", "account-menu", "tagname", "a"), "My Account menu items are not visible");
            Results.WriteStatus("Pass", "Menu items verified.");

            return new MyAccount(driver);
        }

        public OrderHistory clickOrderHistory()
        {
            Results.WriteDescription("Open Order History Page from My Account Menu.");
            driver._Click("linktext", "ORDER HISTORY");
            Results.WriteStatus("Pass", "Link clicked.");

            return new OrderHistory(driver);
        }

        public OrderHistory clickWishList()
        {
            Results.WriteDescription("Open Wish List Page from My Account Menu.");
            driver._Click("linktext", "WISH LIST");
            Results.WriteStatus("Pass", "Link clicked.");

            return new OrderHistory(driver);
        }

        public OrderHistory clickSignOut()
        {
            Results.WriteDescription("Click on Signout link from My Account Menu.");
            driver._Click("id", "account-signout");
            Results.WriteStatus("Pass", "Link clicked.");

            return new OrderHistory(driver);
        }
    }
}
