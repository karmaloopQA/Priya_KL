using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;

namespace KarmaLoop.KL
{
    class Login : TopHeader
    {
        #region Public Methods

        public Login(IWebDriver driver) : base(driver) { }

        public Home LoginAs(bool isNotFromWishList = true)
        {
            Results.WriteDescription("Enter login credential and click on Submit button.");
            driver._Type("name", "EmailAddress", Common.userID);
            if (isNotFromWishList)
            {
                driver._Type("name", "password", Common.passwd);
                driver._Click("name", "submitbutton");
            }
            else
                driver._TypeAndHitEnter("name", "password", Common.passwd);

            Results.WriteStatus("Pass", "Login credential Entered.<br>Used Account of: " + Common.userID);

            Results.WriteDescription("Verify login is successful or not");
            //driver._Wait(5);
            //Assert.AreEqual(false, driver._IsElementPresent("class", "error") && driver._GetText("class", "error").Equals("Invalid email address or password"), driver._GetText("class", "error"));
            //if (isNotFromWishList)
            Assert.AreEqual(true, driver.WaitForElement("id", "signedin-user", 10), "Login Failed.");
            
            Results.WriteStatus("Pass", "Login  Successful.");                    

            return new Home(driver);
        }

        #endregion
    }
}
