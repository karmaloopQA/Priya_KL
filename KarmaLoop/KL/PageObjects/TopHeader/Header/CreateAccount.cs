using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;

namespace KarmaLoop.KL
{
    class CreateAccount : TopHeader
    {
        #region Public Methods

        public CreateAccount(IWebDriver driver) : base(driver) { }

        public Home EnterValidAccountInfo(bool isFromWishListClass = false)
        {
            if (!isFromWishListClass)
            {
                VerifyCreateAccountPopup();
                driver._SelectFrameFromDefaultContent("id", "fancybox-frame");
            }
            Results.WriteDescription("Enter valid account information and click on Submit button.");
            string email = Controls.GenerateDummyEmail();
            driver._Type("name", "Email", email);
            driver._Type("name", "Password", "test");
            driver._Type("name", "ConfirmPassword", "test");
            driver._Type("name", "FirstName", "Ftest");
            driver._Type("name", "LastName", "Ltest");
            driver._Click("id", "account-create-submit");
            Results.WriteStatus("Pass", "Account information Entered.");

            Results.WriteDescription("Verify new account has been created or not");
            IList<IWebElement> list = driver._FindElementsFromElement("id", "account-create-form", "class", "field-validation-error");
            bool isValidationDisplayed = false;
            foreach (IWebElement e in list)
            { 
                bool isPresent = false;
                try { isPresent = e.FindElement(By.TagName("span")).Displayed; }
                catch (NoSuchElementException) { isPresent = false; }
                if (isPresent)
                {
                    isValidationDisplayed = true;
                    break;
                }
            }
            Assert.AreEqual(false, isValidationDisplayed, "Received validation messages.");
            Results.WriteStatus("Pass", "Create account pop up closed.");
            driver._SelectFrameToDefaultContent();
            return new Home(driver);
        }

        #endregion

        #region Private Methods

        private void VerifyCreateAccountPopup()
        {
            Results.WriteDescription("Verify header Text on Create Account Popup.");
            driver._SelectFrameFromDefaultContent("id", "fancybox-frame");
            Console.WriteLine(driver.PageSource);
            Assert.AreEqual("Create your karmaloop.com account", driver._GetText("class", "popup-header"), "Expected Pop up isn't displayed.");
            driver._SelectFrameToDefaultContent();
            Results.WriteStatus("Pass", "Expected Text - 'Create your karmaloop.com account' verified.");
        }

        #endregion
    }
}
