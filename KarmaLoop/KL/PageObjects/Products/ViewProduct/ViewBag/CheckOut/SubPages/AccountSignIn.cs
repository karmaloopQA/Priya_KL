using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;

namespace KarmaLoop.KL
{
    class AccountSignIn : CheckOut
    {
        #region Public Methods

        public AccountSignIn(IWebDriver driver) : base(driver) { }

        /// <summary> Function to verify Account Sign-in page. </summary>
        /// <returns> AccountSignIn Driver </returns>
        public AccountSignIn VerifyAcctSignInPage()
        {
            Results.WriteDescription("Verify Text on the Account Sign-In Page.");
            bool isAcctPage = driver._IsElementPresent("id", "account-header") && driver._VerifyText("xpath", "//*[@id='account-header']/h1", "Account Sign-in");
            Assert.AreEqual(true, isAcctPage, "Expected Text isn't displayed.");
            Results.WriteStatus("Pass", "Page Verified.");

            return new AccountSignIn(driver);
        }

        /// <summary> Function to login as Existing User. </summary>
        /// <returns> Shipping Driver </returns>
        public Shipping SignInAsExistingUser()
        {
            Results.WriteDescription("Enter valid credential of Existing User and click on Sign-In button.");
            driver._Type("id", "usernameCart", Common.userID);
            driver._Type("id", "passwordCart", Common.passwd);
            driver._Click("id", "signin-btn");
            Results.WriteStatus("Pass", "Login credential Entered and Sign-In button clicked.<br>Used Account of: " + Common.userID);

            /*if (driver._IsElementPresent("id", "ship-form") && driver._VerifyText("xpath", "//*[@id='ship-form']/div[1]/h2", "Shipping Address"))
                return new Shipping(driver);
            else if (driver._IsElementPresent("id", "bill-form") && driver._VerifyText("xpath", "//*[@id='bill-form']/div[2]/h2", "Shipping Address"))
                return new Payment(driver);
            else
                return new CheckOut(driver);*/

            if (!(driver._IsElementPresent("id", "ship-form") && driver._VerifyText("xpath", "//*[@id='ship-form']/div[1]/h2", "Shipping Address")))
            {
                Common.comingBackToPreviousStep = true;
                driver._Click("xpath", "//*[@id='breadcrumbs']/li[2]/a");
            }

            return new Shipping(driver);

        }

        /// <summary> Function to enter guest email. </summary>
        /// <returns> Shipping Driver </returns>
        public Shipping CheckOutAsGuestUser()
        {
            Results.WriteDescription("Enter valid email to checkout As Guest and click on Sign-In button.");
            string guestUserEmail = "test1234@test.com";
            driver._Type("id", "guest-email", guestUserEmail);
            driver._Click("id", "checkout-as-guest");
            Results.WriteStatus("Pass", "Valid Email entered and Check Out As Guest button clicked.<br>Used Account of: " + guestUserEmail);

            return new Shipping(driver);
        }

        #endregion

        #region Private Methods

        private Shipping RedirectToShipping()
        {

            return new Shipping(driver);
        }

        #endregion
    }
}


        