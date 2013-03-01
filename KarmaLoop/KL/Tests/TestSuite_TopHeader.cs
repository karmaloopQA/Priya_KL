using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using NUnit.Framework;
using KarmaLoop;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace KarmaLoop.KL.Tests
{
    [TestFixture]
    public class TestSuite_TopHeader : Base
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
            Results.WriteTestSuiteHeading(typeof(TestSuite_TopHeader).Name);   
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            //((IJavaScriptExecutor)driver).ExecuteScript("window.onbeforeunload = function(e){};");
            driver.Quit();
        }

        [Test]
        public void Scen01_LoginAsMember()
        {
            try
            {
                Results.WriteScenarioHeading("TopHeader_01 - Login As existing Member in Karmaloop Site");

                home = home.NavigateToApplication().VerifyHomePage();
                Login log = home.ClickSignIn();
                log.LoginAs();

                Results.EndReport();
            }
            catch (Exception e)
            {
                Logging.LogStop(e);
                throw;
            }
        }

        [Test]
        public void Scen02_SubscribeToEListFromTopNav()
        {
            try
            {
                Results.WriteScenarioHeading("TopHeader_02 - Subscribe To E-List From Top Nav");

                home = home.NavigateToApplication().VerifyHomePage();
                SubscribeElist s = header.ClickSubscribeToElist().VerifySubscribePopup();
                s.EntervalidDataAndClickSubscribe();

                Results.EndReport();
            }
            catch (Exception e)
            {
                Logging.LogStop(e);
                throw;
            }
        }

        [Test]
        public void Scen03_CreateAccount()
        {
            try
            {
                Results.WriteScenarioHeading("TopHeader_03 - Create an Account");

                home = home.NavigateToApplication().VerifyHomePage();
                CreateAccount c = header.ClickSignInAndCreateAccount();
                c.EnterValidAccountInfo();

                Results.EndReport();
            }
            catch (Exception e)
            {
                Logging.LogStop(e);
                throw;
            }
        }

        [Test]
        public void Scen04_PerformKeywordSearchForProduct()
        {
            try
            {
                Results.WriteScenarioHeading("TopHeader_04 - Perform Keyword search from Top header");

                home = home.NavigateToApplication().VerifyHomePage();
                Products prod = header.SearchProduct();
                prod.VerifyProductsPage_afterSearch();

                Results.EndReport();
            }
            catch (Exception e)
            {
                Logging.LogStop(e);
                throw;
            }
        }

        [Test]
        public void Scen05_SelectAnyBrandForMen()
        {
            try
            {
                Results.WriteScenarioHeading("TopHeader_05 - Select any Men's Brand from Top header");

                home = home.NavigateToApplication().VerifyHomePage();
                Products prod = header.SearchProduct();
                prod.VerifyProductsPage();

                Results.EndReport();
            }
            catch (Exception e)
            {
                Logging.LogStop(e);
                throw;
            }
        }

        [Test]
        public void Scen06_SelectAnyBrandForWomen()
        {
            try
            {
                Results.WriteScenarioHeading("TopHeader_06 - Select any Women's Brand from Top header");

                home = home.NavigateToApplication().VerifyHomePage();
                Products prod = header.SearchProduct();
                prod.VerifyProductsPage_afterSearch();

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