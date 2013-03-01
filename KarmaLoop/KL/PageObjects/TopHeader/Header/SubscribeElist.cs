using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using NUnit.Framework;
using System.Collections.ObjectModel;

namespace KarmaLoop.KL
{
    class SubscribeElist : TopHeader
    {
        #region Public Methods

        public SubscribeElist(IWebDriver driver): base(driver) {  }

        public SubscribeElist VerifySubscribePopup()
        {
            Results.WriteDescription("Verify Subscribe to Elist page");
            System.Threading.Thread.Sleep(2000);          

            string text = driver._GetText("class", "popup-header");
            //Console.WriteLine(text);
            Assert.AreEqual(text, "Subscribe to our Newsletter");
            Results.WriteStatus("Pass", "Subscribe to Elist displayed successfully!");

            return new SubscribeElist(driver);
        }

        public SubscribeElist EntervalidDataAndClickSubscribe()
        {
            Results.WriteDescription("Enter valid Email address, phone and click subscribe");
            driver._Type("id", "Email", "testing527.test@gmail.com");
            driver._Type("id", "Phone", "1231231234");
            driver._selectCheckBox("id", "SendSMS");
            driver._Click("id", "subscribe-submit");

            if (driver._IsElementPresent("class", "error"))
            {
                string text3 = driver._GetText("class", "error");
                //Console.WriteLine(text3);
                Assert.AreEqual(text3, "This field is required.");
                Results.WriteStatus("Pass", "Message Displayed: " + text3);
            }
            else
            {
                Results.WriteStatus("Pass", "Subscribe Button Clicked Successfully");
            }

            Results.WriteDescription("Enter valid Email address, valid phone and click subscribe");
            driver._Type("id", "Phone", "1231231234");           

            driver._SelectRadioOption("id", "Gender");
            driver._Click("id", "subscribe-submit");
            System.Threading.Thread.Sleep(2000);
            if (driver._IsElementPresent("class", "validation-summary-errors"))
            {
                IList<IWebElement> ele = driver.FindElements(By.ClassName("validation-summary-errors"));
                //Console.WriteLine(text4);
                string text4 = null;
                int i = 0;
                foreach (IWebElement e in ele)
                {

                    text4 = e.Text;
                    //Console.WriteLine(text4);
                    i = i + 1;
                    if (i == 2) break;
                }

                Assert.AreEqual(text4, "You have already subscribed.");
                driver._Type("id", "Email", "testing533.test@gmail.com");  // Make sure you put here new email address after each successfull run.
                driver._Click("id", "subscribe-submit");
            }

            System.Threading.Thread.Sleep(5000);
            string text1 = driver._GetText("class", "yui3-u-1");
            //Console.WriteLine(text1);
            Assert.AreEqual(text1, "Thank you for signing up for the Karmaloop Newsletter!");

            driver.SwitchTo().DefaultContent();
            driver._Click("id", "fancybox-close");
            Results.WriteStatus("Pass", "Subscribe button clicked successfully!");

            return new SubscribeElist(driver);
        }

        #endregion
    }
}
