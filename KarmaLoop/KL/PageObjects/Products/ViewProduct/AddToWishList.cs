using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;

namespace KarmaLoop.KL
{
    class AddToWishList : ViewProduct
    {
        #region Public Methods

        public AddToWishList(IWebDriver driver) : base(driver) { }

        /// <summary> Function to logs in as existing Member. </summary>
        /// <returns> ViewProduct Driver </returns>
        public ViewProduct LoginAsExistingMember()
        {            
            VerifyLoginRegisterPopUp();
            driver._SelectFrameFromDefaultContent("id", "login-or-register");
            new Login(driver).LoginAs(false);
            driver._SelectFrameToDefaultContent();

            return new ViewProduct(driver);
        }

        /// <summary> Function to create new account. </summary>
        /// <returns> ViewProduct Driver </returns>
        public ViewProduct CreateNewAccount()
        {
            driver._SelectFrameFromDefaultContent("id", "login-or-register");
            VerifyLoginRegisterPopUp();
            new CreateAccount(driver).EnterValidAccountInfo(true);
            driver._SelectFrameToDefaultContent();
            
            return new ViewProduct(driver);
        }

        #endregion

        #region Private Methods

        /// <summary> Function to verify login/register page. </summary>
        /// <returns> AddToWishList Driver</returns>
        private AddToWishList VerifyLoginRegisterPopUp()
        {
            Results.WriteDescription("Verify Text on Login/Create Account Pop up");
            //IList<IWebElement> list = driver._FindElements("tagname", "iframe");
            //foreach (IWebElement e in list)
            //{
            //    Console.WriteLine("ID:(of count " + list.Count + " ) " + e.GetAttribute("id"));
            //}
            //driver._Wait(5);
            //driver._SelectFrameFromDefaultContent("id", "login-or-register");
            driver.WaitForElementToBePopulated("id", "createUserFormContainer", 20);
            bool isLoginRegisterPopUp = driver._IsElementPresent("id", "createUserFormContainer");
            //bool isLoginRegisterPopUp = driver._VerifyText("xpath", "//*[@id='signin-register-wrapper']/h3", "Sign-in or Create an Account to continue");
            Assert.AreEqual(true, isLoginRegisterPopUp, "Expected Text isn't available");
            //driver._SelectFrameToDefaultContent();
            Results.WriteStatus("Pass", "Text on Login/Register popup verified.");

            return new AddToWishList(driver);
        }

        #endregion
    }
}
