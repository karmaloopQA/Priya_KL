using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;

namespace KarmaLoop.KL
{
    class Shipping : CheckOut
    {
        #region Private Variables

        private bool comingBack = false;

        #endregion

        #region Public Methods

        public Shipping(IWebDriver driver) : base(driver) { }

        /// <summary> Function to verify shipping page. </summary>
        /// <returns> Shipping Driver </returns>
        public Shipping VerifyShippingTab()
        {
            Results.WriteDescription("Verify Text on Shipping address tab.");
            if (driver._IsElementPresent("class", "review-order-step"))
            {
                driver._Click("xpath", "//*[@id='breadcrumbs']/li[2]/a");
                comingBack = true;
            }
            bool isShippingPage = driver._IsElementPresent("id", "ship-form") && driver._VerifyText("xpath", "//*[@id='ship-form']/div[1]/h2", "Shipping Address");
            Assert.AreEqual(true, isShippingPage, "Expected Text isn't available.");
            Results.WriteStatus("Pass", "Text verified.");

            return new Shipping(driver);
        }

        /// <summary> Function to input shipping information. </summary>
        /// <returns> Payment Driver </returns>
        public Payment InputShippingInfo()
        {
            // verifyShippingTab();
            if (Common.comingBackToPreviousStep.Equals(false))
            {
                EnterShippingAddress();
                // selectShippingMethod();
            }
            ClickNextButton();

            return new Payment(driver);
        }

        /// <summary> Function to click next button to navigate to Next step of checkout process. </summary>
        /// <returns> Payment Driver </returns>
        public Payment ClickNextButton()
        {
            Results.WriteDescription("Click on Next Button from Shipping tab.");
            driver._Click("xpath", "//*[@id='shipping-step']/a[2]");
            Assert.AreEqual(true, driver._WaitForElementToBeHidden("xpath", "//*[@id='shipping-step']/a[2]"), "Page isn't changed yet.");
            Results.WriteStatus("Pass", "Button Clicked.");

            return new Payment(driver);
        }

        #endregion

        #region Private Methods

        /// <summary> Function to enter shipping address. </summary>
        /// <returns> Payment Driver </returns>
        private void EnterShippingAddress()
        {
            Results.WriteDescription("Enter shipping address");
            driver._Type("id", "ship-first-name", "karmaloop1");
            driver._Type("id", "ship-last-name", "yahoo1");
            driver._Type("id", "ship-phone", "123-123-1234");
            driver._Type("id", "ship-email", "karmaloop1@yahoo.com");
            driver._Type("id", "ship-address1", "2nd street, ");
            driver._Type("id", "ship-address2", "Suite 1");
            driver._Type("id", "ship-city", "Boston");
            driver._Type("id", "ship-zip", "01719");
            driver._SelectOption("id", "ship-country", "United States");
            driver._SelectOption("id", "ship-state", "Massachusetts");
            Results.WriteStatus("Pass", "Shipping address entered successfully!");
        }

        /// <summary> Function to select shipping option. </summary>
        /// <returns> Payment Driver </returns>
        private void SelectShippingMethod()
        {
            Results.WriteDescription("Select any shipping method.");
            driver._Click("id", "ship-phone");
            string text = driver._SelectRandomRadioOption("id", "ship-form", "name", "ship-method");
            Results.WriteStatus("Pass", "Shipping method chosen as " + text);
        }

        #endregion
    }
}


        