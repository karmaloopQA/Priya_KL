using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using NUnit.Framework;

namespace KarmaLoop.KL
{
    class EmailAFriend : ViewProduct
    {
        #region Public Methods

        public EmailAFriend(IWebDriver driver) : base(driver) { }

        /// <summary> Function to verify Email a friend popup. </summary>
        /// <returns> Email A Friend Driver </returns>
        public EmailAFriend VerifyEmailAFriendPopUp()
        {
            Results.WriteDescription("Verify Text on Email a Friend pop up.");
            bool isEmailFriendPopUp = driver._IsElementPresent("id", "email-a-friend-form") && driver._VerifyText("xpath", "//*[@id='email-a-friend-form']/h2", "Share this item with your friends.");
            Assert.AreEqual(true, isEmailFriendPopUp, "Pop up isn't visible.");
            Results.WriteStatus("Pass", "pop up verified.");

            return new EmailAFriend(driver);
        }

        /// <summary> Function to enter reuired fields and clicks Send Email. </summary>
        /// <returns> ViewProduct Driver </returns>
        public ViewProduct EnterInfoAndSendEmail()
        {
            Results.WriteDescription("Enter To(email address), From(email address), Note and Click on 'Send' button.");
            bool isEmailFriendPopUp = driver._IsElementPresent("id", "email-a-friend-form") && driver._VerifyText("xpath", "//*[@id='email-a-friend-form']/h2", "Share this item with your friends.");
            Assert.AreEqual(true, isEmailFriendPopUp, "Pop up isn't visible.");
            driver._Type("name", "EmailAddressTo", "pritesh09@hotmail.com");
            driver._Type("name", "EmailAddressFrom", Common.userID);
            driver._Type("name", "Note", "Testing Email sending.");
            driver._Click("xpath", "//*[@id='email-a-friend-form']/input");
            Assert.AreEqual(0, driver._FindElementsFromElement("id", "email-a-friend-form", "class", "error").Count, "Received validation message");
            driver._WaitForElementToBeHidden("id", "email-a-friend-form",10);
            Results.WriteStatus("Pass", "values entered and send button clicked successfully.");

            Results.WriteDescription("Verify confirmation message after clicking Send button.");
            Assert.AreEqual("Your email was successfully sent!".ToLower(), driver._GetText("xpath", "//*[@id='inner-email-container']/div/p").ToLower(),"No message received.");
            driver._Click("id", "fancybox-close");
            Results.WriteStatus("Pass", "Message Received Successfully.");

            return new ViewProduct(driver);
        }

        #endregion
    }
}
