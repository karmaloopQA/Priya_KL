using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;

namespace KarmaLoop.KL
{
    class OrderReceipt : ReviewYourOrder
    {
        #region Public Methods

        public OrderReceipt(IWebDriver driver): base(driver) { }

        /// <summary> Function to verify Receipt page. </summary>
        /// <returns> Checkout Driver </returns>
        public KarmaLoop.KL.CheckOut VerifyReceiptPage()
        {
            Results.WriteDescription("Verify Text/URL on Order Receipt page.");
            Assert.AreEqual("https://www.karmaloop.com/cart#checkout", driver.Url, "URL doesn't Matched.");
            bool isReceiptPage = driver._IsElementPresent("xpath", "//*[@id='wrapper-inner']/div[5]/div[6]/div[1]/h1") && driver._VerifyText("xpath", "//*[@id='wrapper-inner']/div[5]/div[6]/div[1]/h1", "Thank you for your order on Karmaloop!");
            Assert.AreEqual(true, isReceiptPage, "Expected Text isn't available.");
            Results.WriteStatus("Pass", "Text verified.");

            return new CheckOut(driver);
        }

        /// <summary> Function to get order number. </summary>
        /// <returns> Checkout Driver </returns>
        public void GetOrderNumber()
        {
            VerifyReceiptPage();
            Results.WriteDescription("Get Order number from Order Receipt step.");
            string orderNumber = driver._GetText("xpath", "//*[@id='final-summary']/div[2]/ul[1]/li/span[2]");
            Results.WriteStatus("Pass", "Order #" + orderNumber);
        }

        #endregion
    }
}


        