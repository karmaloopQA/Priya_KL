using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;

namespace KarmaLoop.KL
{
    class CheckOut : ViewBag
    {
        #region Public Methods

        public CheckOut(IWebDriver driver) : base(driver) { }

        /// <summary> Function to verify Check out page. </summary>
        /// <returns> CheckOut Driver </returns>
        public KarmaLoop.KL.CheckOut VerifyCheckOutPage()
        {
            Results.WriteDescription("Verify Text/URL on Check Out page.");
            Assert.AreEqual("https://www.karmaloop.com/cart#checkout", driver.Url, "URL doesn't Matched.");
            bool isCheckOutPage = driver._IsElementPresent("id", "order-summary-title") && driver._VerifyText("id", "order-summary-title", "Order Summary");
            Assert.AreEqual(true, isCheckOutPage, "Expected Text isn't available.");
            Results.WriteStatus("Pass", "Text verified.");

            return new CheckOut(driver);
        }

        /// <summary> Function to complete the check out the process with credit card. </summary>
        /// <returns> CheckOut Driver </returns>
        public CheckOut CompleteCheckOutProcess()
        {
            if (driver._IsElementPresent("id", "ship-form") && driver._VerifyText("xpath", "//*[@id='ship-form']/div[1]/h2", "Shipping Address"))
            {
                Shipping ship = new Shipping(driver);
                Payment pay = ship.InputShippingInfo();
                ReviewYourOrder rev = pay.InputBillingInfo();
                rev.ClickPlaceYourOrderButton();
            }
            else if (driver._IsElementPresent("id", "bill-form") && driver._VerifyText("xpath", "//*[@id='bill-form']/div[2]/h2", "Billing Address"))
            {
                Payment pay = new Payment(driver);
                ReviewYourOrder rev = pay.InputBillingInfo();
                rev.ClickPlaceYourOrderButton();
            }
            else if (driver._IsElementPresent("id", "checkout-review") && driver._VerifyText("xpath", "//*[@id='checkout-review']/h1", "Review Your Order"))
            {
                new ReviewYourOrder(driver).ClickPlaceYourOrderButton();
            }
            else
                Assert.Fail("Incorrect Page Redirection.");

            return new CheckOut(driver);
        }

        #endregion
    }
}


        