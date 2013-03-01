using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;

namespace KarmaLoop.KL
{
    class ReviewYourOrder : CheckOut
    {
        #region Public Methods

        public ReviewYourOrder(IWebDriver driver) : base(driver) { }

        /// <summary> Function to verify Review Order tab. </summary>
        /// <returns> ReviewYourOrder Driver </returns>
        public ReviewYourOrder VerifyReviewOrderTab()
        {
            Results.WriteDescription("Verify Text/URL on Review Your Order Tab.");
            bool isReviewOrderPage = driver._IsElementPresent("id", "checkout-review") && driver._VerifyText("xpath", "//*[@id='checkout-review']/h1", "Review Your Order");
            Assert.AreEqual(true, isReviewOrderPage, "Expected Text isn't available.");
            Results.WriteStatus("Pass", "Text verified.");

            return new ReviewYourOrder(driver);
        }

        /// <summary> Function to click Place Your Order button to complete the check out process. </summary>
        /// <returns> OrderReceipt Driver </returns>
        public OrderReceipt ClickPlaceYourOrderButton()
        {
            Results.WriteDescription("Click on 'Place Your Order' button to Edit Shipping To information.");
            driver._Click("xpath", "//*[@id='checkout-review']/a[2]");
            driver._WaitForElementToBeHidden("id", "checkout-review", 60);
            Results.WriteStatus("Pass", "Button clicked.");

            return new OrderReceipt(driver);
        }

        /// <summary> Function to apply promo or Gift Code. </summary>
        /// <returns> ReviewYourOrder Driver </returns>
        public ReviewYourOrder ApplyPromoORGiftCode()
        {
            Results.WriteDescription("Enter Promo / Gift code and click on 'Apply' button.");
            string appliedPromo = "30SALE";
            driver._TypeAndHitEnter("id", "promo-code-in", appliedPromo);
            //driver._Click("xpath", "//*[@class='repcode']/div/span");
            Results.WriteStatus("Pass", "Apply Button clicked.");

            Results.WriteDescription("Check whether the entered code is applied or not?");
            bool isDescPresent = driver._IsElementPresent("class", "code-desc") && driver._VerifyText("class", "code-desc","Promo Code");
            bool isValuePresent = driver._IsElementPresent("class", "code-value") && driver._VerifyText("class", "code-value", appliedPromo);
            bool isStatusPresent = driver._IsElementPresent("class", "code-status") && driver._VerifyText("class", "code-status","added");
            if (isDescPresent)
                Console.WriteLine("Description verified.");
            if (isValuePresent)
                Console.WriteLine("value verified.");
            if (isStatusPresent)
                Console.WriteLine("status verified.");

            return new ReviewYourOrder(driver);
        }

        /// <summary> Function to apply Rep code. </summary>
        /// <returns> ReviewYourOrder Driver </returns>
        public ReviewYourOrder ApplyRepCode()
        {
            Results.WriteDescription("Enter Promo / Gift code and click on 'Apply' button.");
            string appliedCode = "wikiwiki";
            driver._Type("id", "rep-code-in", appliedCode);
            driver._Click("xpath", "//*[@class='repcode']/div/span");
            Results.WriteStatus("Pass", "Apply Button clicked.");

            Results.WriteDescription("Check whether the entered code is applied or not?");
            bool isDescPresent = driver._IsElementPresent("class", "code-desc") && driver._VerifyText("class", "code-desc", "Promo Code");
            bool isValuePresent = driver._IsElementPresent("class", "code-value") && driver._VerifyText("class", "code-value", appliedCode);
            bool isStatusPresent = driver._IsElementPresent("class", "code-status") && driver._VerifyText("class", "code-status", "added");
            if (!isDescPresent)
                Console.WriteLine("Description Not verified.");
            if (!isValuePresent)
                Console.WriteLine("value Not verified.");
            if (!isStatusPresent)
                Console.WriteLine("status Not verified.");
            Assert.AreEqual(true, isDescPresent && isValuePresent && isStatusPresent, "Expected code is not applied.");
            Results.WriteStatus("Pass", "Code Applied successfully.");

            return new ReviewYourOrder(driver);
        }

        /// <summary> Function to verify apply gift Certificate or Store Credit. </summary>
        /// <returns> ReviewYourOrder Driver </returns>
        public ReviewYourOrder ApplyGiftCertificateORStoreCredit()
        {
            Results.WriteDescription("Enter Promo / Gift code and click on 'Apply' button.");
            string appliedCode = "12345678";
            driver._Type("id", "credit-in", appliedCode);
            driver._Click("xpath", "//*[@class='gift-certificates-div']/div/span");
            Results.WriteStatus("Pass", "Apply Button clicked.");

            Results.WriteDescription("Check whether the entered code is applied or not?");
            bool isDescPresent = driver._IsElementPresent("class", "code-desc") && driver._VerifyText("class", "code-desc", "Promo Code");
            bool isValuePresent = driver._IsElementPresent("class", "code-value") && driver._VerifyText("class", "code-value", appliedCode);
            bool isStatusPresent = driver._IsElementPresent("class", "code-status") && driver._VerifyText("class", "code-status", "added");
            if (!isDescPresent)
                Console.WriteLine("Description Not verified.");
            if (!isValuePresent)
                Console.WriteLine("value Not verified.");
            if (!isStatusPresent)
                Console.WriteLine("status Not verified.");
            Assert.AreEqual(true, isDescPresent && isValuePresent && isStatusPresent, "Expected code is not applied.");
            Results.WriteStatus("Pass", "Code Applied successfully.");

            return new ReviewYourOrder(driver);
        }

        /// <summary> Function to change the Shipping To. </summary>
        /// <returns> Shipping Driver </returns>
        public Shipping ChangeShippingTo()
        {
            Results.WriteDescription("Click on 'Change' button to Edit Shipping To information.");
            driver._Click("class", "kl-change-button", 1);
            driver._WaitForElementToBeHidden("id", "checkout-review");
            Results.WriteStatus("Pass", "Shipping Tab is activated.");

            return new Shipping(driver);
        }

        /// <summary> Function to change billing address. </summary>
        /// <returns> Payment Driver </returns>
        public Payment ChangeBillingAddress()
        {
            Results.WriteDescription("Click on 'Change' button to Edit Billing Address information.");
            driver._Click("class", "kl-change-button", 2);
            driver._WaitForElementToBeHidden("id", "checkout-review");
            Results.WriteStatus("Pass", "Payment Tab is activated.");

            return new Payment(driver);
        }

        /// <summary> Function to change Shipping method. </summary>
        /// <returns> Shipping Driver </returns>
        public Shipping ChangeShippingMethod()
        {
            Results.WriteDescription("Click on 'Change' button to Edit Shipping Method information.");
            driver._Click("class", "kl-change-button", 3);
            driver._WaitForElementToBeHidden("id", "checkout-review");
            Results.WriteStatus("Pass", "Shipping Tab is activated.");

            return new Shipping(driver);
        }

        /// <summary> Function to change Payment method. </summary>
        /// <returns> Payment Driver </returns>
        public Payment ChangePaymentMethod()
        {
            Results.WriteDescription("Click on 'Change' button to Edit Payment Method information.");
            driver._Click("class", "kl-change-button", 4);
            driver._WaitForElementToBeHidden("id", "checkout-review");
            Results.WriteStatus("Pass", "Payment Tab is activated.");

            return new Payment(driver);
        }

        #endregion
    }
}


        