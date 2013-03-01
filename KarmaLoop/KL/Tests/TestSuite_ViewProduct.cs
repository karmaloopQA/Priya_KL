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
    public class TestSuite_ViewProduct : Base
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
            Results.WriteTestSuiteHeading(typeof(TestSuite_ViewProduct).Name);   
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            //((IJavaScriptExecutor)driver).ExecuteScript("window.onbeforeunload = function(e){};");
            driver.Quit();
        }

        [Test]
        public void Scen01_ViewProductInfo_RandomProduct()
        {
            try
            {
                Results.WriteScenarioHeading("ViewProduct_01 - View Product Information(Brand, Title, Size, etc) of any Random Product.");

                home = home.NavigateToApplication().VerifyHomePage();
                ViewProduct view = home.ViewProduct().VerifyViewProductPage();
                
                Results.EndReport();
            }
            catch(Exception e)
            {
                Logging.LogStop(e);
                throw;
            }
        }

        [Test]
        public void Scen02_ViewProductInfo_SpecificProduct()
        {
            try
            {
                
                Results.WriteScenarioHeading("ViewProduct_02 - View Product Information(Brand, Title, Size, etc). of ProductID: " + Common.productID);

                home = home.NavigateToApplication().VerifyHomePage();
                ViewProduct view = home.ViewSpecificProduct(Common.productID).VerifyViewProductPage();


                Results.EndReport();
            }
            catch (Exception e)
            {
                Logging.LogStop(e);
                throw;
            }
        }

        [Test]
        public void Scen03_AddProductToShoppingBag()
        {
            try
            {

                Results.WriteScenarioHeading("ViewProduct_03 - Add a Product to shopping bag");

                home = home.NavigateToApplication().VerifyHomePage();
                ViewProduct view = home.ViewProduct().VerifyViewProductPage();
                view = view.SelectSizeAndClickAddToShoppingBag();
                ViewBag bag = view.OpenShoppingBag().VerifyShoppingBagPage();
                
                Results.EndReport();
            }
            catch (Exception e)
            {
                Logging.LogStop(e);
                throw;
            }
        }

        [Test]
        public void Scen04_SendEmailToFriend()
        {
            try
            {

                Results.WriteScenarioHeading("ViewProduct_04 - Share Product Info with the friend via Email");

                home = home.NavigateToApplication().VerifyHomePage();
                ViewProduct view = home.ViewProduct().VerifyViewProductPage();
                EmailAFriend email = view.ClickEmailAFriendButton();
                view = email.EnterInfoAndSendEmail();

                Results.EndReport();
            }
            catch (Exception e)
            {
                Logging.LogStop(e);
                throw;
            }
        }

        [Test]
        public void Scen05_AddToWishList_AlreadyLoggedUser()
        {
            try
            {

                Results.WriteScenarioHeading("ViewProduct_05 - Add a Product to Wish List(when user already logged)");

                home = home.NavigateToApplication().VerifyHomePage();
                home = home.ClickSignIn().LoginAs();
                ViewProduct view = home.ViewProduct().VerifyViewProductPage();
                view = view.ClickAddToWishList_WhenAlreadyLoggedIn();
                
                Results.EndReport();
            }
            catch (Exception e)
            {
                Logging.LogStop(e);
                throw;
            }
        }

        //[Test]
        public void Scen06_AddToWishList_LoginDuringProcess()
        {
            try
            {

                Results.WriteScenarioHeading("ViewProduct_06 - Add a Product to Wish List(Exisitng User logs in during process)");

                home = home.NavigateToApplication().VerifyHomePage();
                ViewProduct view = home.ViewProduct().VerifyViewProductPage();
                view.ClickAddToWishList_WhenLoginNeeded();
                                
                Results.EndReport();
            }
            catch (Exception e)
            {
                Logging.LogStop(e);
                throw;
            }
        }

        //[Test]
        public void Scen07_AddToWishList_CreateNewAccountDuringProcess()
        {
            try
            {

                Results.WriteScenarioHeading("ViewProduct_07 - Add a Product to Wish List(creating new account during process");

                home = home.NavigateToApplication().VerifyHomePage();
                ViewProduct view = home.ViewProduct().VerifyViewProductPage();
                view.ClickAddToWishList_WhenNewAccountNeeded();
                                
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