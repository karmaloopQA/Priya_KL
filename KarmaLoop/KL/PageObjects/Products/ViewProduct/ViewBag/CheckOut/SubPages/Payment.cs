using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;

namespace KarmaLoop.KL
{
    class Payment : CheckOut
    {
        #region Public Methods

        public Payment(IWebDriver driver) : base(driver) { }

        /// <summary> Function to verify payment tab. </summary>
        /// <returns> ReviewYourOrder Driver </returns>
        public Payment VerifyPaymentTab()
        {
            Results.WriteDescription("Verify Text on Payment Info tab.");
            bool isPaymentPage = driver._IsElementPresent("id", "bill-form") && driver._VerifyText("xpath", "//*[@id='bill-form']/div[2]/h2", "Billing Address");
            Assert.AreEqual(true, isPaymentPage, "Expected Text isn't available.");
            Results.WriteStatus("Pass", "Text verified.");

            return new Payment(driver);
        }

        /// <summary> Function to Input billing Information. </summary>
        /// <returns> ReviewYourOrder Driver </returns>
        public ReviewYourOrder InputBillingInfo()
        {
            //verifyPaymentTab();
            if (Common.comingBackToPreviousStep.Equals(false))
            {
                //clickSameAsShippingAddress();
            }
            EnterCreditCardInfo();
            ClickNextButton();

            return new ReviewYourOrder(driver);
        }      

        #endregion

        #region Private Methods

        private void EnterBillingAddress()
        {
            Results.WriteDescription("De-select 'Same As Shipping Address' checkbox to add Billing Address.");
            driver._deselectCheckBox("id", "bill-matches-shipping");
            Assert.AreEqual(false, driver._isChecked("id", "bill-matches-shipping"), "Unable to De-select 'Same As Shipping Address' checkbox");
            Results.WriteStatus("Pass", "Billing Address fields displayed.");

            Results.WriteDescription("Enter billing address information.");
            driver._Type("id", "ship-first-name", "karmaloop1");
            driver._Type("id", "ship-last-name", "yahoo1");
            driver._Type("id", "ship-phone", "123-123-1234");
            driver._Type("id", "ship-email", "karmaloop1@yahoo.com");
            driver._Type("id", "ship-address1", "2nd street");
            driver._Type("id", "ship-address2", "Alkapuri");
            driver._Type("id", "ship-city", "Baroda");
            driver._Type("id", "ship-zip", "390012");
            driver._SelectOption("id", "ship-country", "Canada");
            driver._SelectOption("id", "ship-state", "Alberta");
            Results.WriteStatus("Pass", "Shipping address entered successfully!");
        }

        private void EnterCreditCardInfo()
        {
            if (driver._IsElementPresent("id", "add-new-card"))
                ClickAddNewCreditCard();
            Results.WriteDescription("Enter credit card information");
            driver._Type("id", "payment-name", "karmaloop1 yahoo1");
            driver._Type("id", "payment-card-number", "4111111111111111");
            driver._SelectOption("id", "payment-card-expiration-month", "10 (Oct)");
            driver._SelectOption("id", "payment-card-expiration-year", "20 (2020)");
            driver._Type("id", "payment-csc", "123");
            Results.WriteStatus("Pass", "Credit card information entered successfully.");
        }

        private void ClickAddNewCreditCard()
        {
            Results.WriteDescription("Click on add new credit card link");
            driver._Click("id", "add-new-card");
            Results.WriteStatus("Pass", "link clicked.");
        }

        private void ClickSameAsShippingAddress()
        {
            Results.WriteDescription("Select any shipping method.");
            driver._selectCheckBox("id", "bill-matches-shipping");
            Results.WriteStatus("Pass", "Same As Shipping Adress is checked.");
        }

        private void ClickNextButton()
        {
            Results.WriteDescription("Click on Next Button from Payment tab.");
            driver._Click("xpath", "//*[@id='billing-step']/a[2]");
            Assert.AreEqual(true, driver._WaitForElementToBeHidden("xpath", "//*[@id='billing-step']/a[2]"), "Page isn't changed yet.");
            Results.WriteStatus("Pass","Button Clicked.");
        }

        #endregion
    }
}


               