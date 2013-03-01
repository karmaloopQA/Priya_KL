using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using NUnit.Framework;
using KarmaLoop;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Reflection;

namespace KarmaLoop.KL.Tests
{
    [TestFixture]
    public class TestSuite_CheckOut : Base
    {
        TopHeader header;
        Home home;
        
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            IWebDriver driver = StartBrowser();
            Common.currentDriver = driver;
            header = new TopHeader(driver);
            home = new Home(driver);
            Results.WriteTestSuiteHeading(typeof(TestSuite_CheckOut).Name);   
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            driver.Quit();
        }

        [Test]
        public void Scen01_CheckOutAsGuestUser()
        {
            try
            {
                Results.WriteScenarioHeading("CheckOut_01 - Buy a Product As Guest User.");

                home = home.NavigateToApplication().VerifyHomePage();
                ViewProduct view = home.ViewProduct().VerifyViewProductPage();
                ViewBag bag = view.SelectSizeAndClickAddToShoppingBag().ClickViewBag().VerifyShoppingBagPage();
                AccountSignIn acct = bag.ClickCheckOutWithCreditCard_AsGuestUser().VerifyAcctSignInPage();
                Shipping ship = acct.CheckOutAsGuestUser().VerifyShippingTab();
                Payment pay = ship.InputShippingInfo().VerifyPaymentTab();
                ReviewYourOrder rev = pay.InputBillingInfo().VerifyReviewOrderTab();
                OrderReceipt receipt = rev.ClickPlaceYourOrderButton();
                receipt.GetOrderNumber();

                Results.EndReport();
            }
            catch(Exception e)
            {
                Logging.LogStop(e);
                throw;
            }
        }

        [Test]
        public void Scen02_CheckOutAsExistingUser_LoginBeforeCheckout()
        {
            try
            {
                Results.WriteScenarioHeading("CheckOut_02 - Buy a Product As Existing User(Logs in before adding Prodcuct in Bag).");

                home = home.NavigateToApplication().VerifyHomePage();
                home = home.ClickSignIn().LoginAs();
                ViewProduct view = home.ViewProduct().VerifyViewProductPage();
                ViewBag bag = view.SelectSizeAndClickAddToShoppingBag().ClickViewBag().VerifyShoppingBagPage();
                Shipping ship = bag.ClickCheckOutWithCreditCard().VerifyShippingTab();
                Payment pay = ship.InputShippingInfo().VerifyPaymentTab();
                ReviewYourOrder rev = pay.InputBillingInfo().VerifyReviewOrderTab();
                OrderReceipt receipt = rev.ClickPlaceYourOrderButton();
                receipt.GetOrderNumber();

                Results.EndReport();
            }
            catch (Exception e)
            {
                Logging.LogStop(e);
                throw;
            }
        }

        [Test]
        public void Scen03_CheckOutAsExistingUser_LoginDuringCheckOut()
        {
            try
            {
                Results.WriteScenarioHeading("CheckOut_03 - Buy a Product As Existing User(Logs in during checkout).");

                home = home.NavigateToApplication().VerifyHomePage();
                ViewProduct view = home.ViewProduct().VerifyViewProductPage();
                ViewBag bag = view.SelectSizeAndClickAddToShoppingBag().ClickViewBag().VerifyShoppingBagPage();
                AccountSignIn acct = bag.ClickCheckOutWithCreditCard_AsGuestUser().VerifyAcctSignInPage();
                Shipping ship = acct.SignInAsExistingUser().VerifyShippingTab();
                Payment pay = ship.InputShippingInfo().VerifyPaymentTab();
                ReviewYourOrder rev = pay.InputBillingInfo().VerifyReviewOrderTab();
                OrderReceipt receipt = rev.ClickPlaceYourOrderButton();
                receipt.GetOrderNumber();

                Results.EndReport();
            }
            catch (Exception e)
            {
                Logging.LogStop(e);
                throw;
            }
        }

    }
}