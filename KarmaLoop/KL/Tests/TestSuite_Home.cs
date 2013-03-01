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
    public class TestSuite_Home : Base
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
            Results.WriteTestSuiteHeading(typeof(TestSuite_Home).Name);   
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            //((IJavaScriptExecutor)driver).ExecuteScript("window.onbeforeunload = function(e){};");
            //driver.Quit();
        }

        [Test]
        public void Scen01_LaunchKarmaLoopSite()
        {
            try
            {
                Results.WriteScenarioHeading("Home_01 - Launch Karmaloop Site and Navigate to Home Page");
                
                home = home.NavigateToApplication();
                                
                Results.EndReport();
            }
            catch(Exception e)
            {
                Logging.LogStop(e);
                throw;
            }
        }

        [Test]
        public void Scen02_ViewNewStylesBrandsProducts()
        {
            try
            {
                Results.WriteScenarioHeading("Home_02 - Click a Brand link from New Styles Panel and View Products");

                home = home.NavigateToApplication();
                Products products = home.ClickNewStyle();

                Results.EndReport();
            }
            catch (Exception e)
            {
                Logging.LogStop(e);
                throw;
            }
        }

        [Test]
        public void Scen03_SubscribeToEListFromHomePage()
        {
            try
            {
                Results.WriteScenarioHeading("Home_03 - Subscribe to E-List from Home Page");

                home = home.NavigateToApplication();
                home = home.JoinOurEList();

                Results.EndReport();
            }
            catch (Exception e)
            {
                Logging.LogStop(e);
                throw;
            }
        }

        [Test]
        public void Scen04_DisplayAllProductsInfoFromHome()
        {
            try
            {
                Results.WriteScenarioHeading("Home_04 - Display All Products information from Home Page");

                home = home.NavigateToApplication();
                home = home.displayInfoAllTheProducts();

                Results.EndReport();
            }
            catch (Exception e)
            {
                Logging.LogStop(e);
                throw;
            }
        }

        [Test]
        public void Scen05_ClickToViewProductInfoFromHome()
        {
            try
            {
                Results.WriteScenarioHeading("Home_05 - View an random Product from Home Page");

                home = home.NavigateToApplication();
                ViewProduct prod = home.ViewProduct();

                Results.EndReport();
            }
            catch (Exception e)
            {
                Logging.LogStop(e);
                throw;
            }
        }

        [Test]
        public void Scen06_ViewProductByInputingProductIDInUrl()
        {
            try
            {
                Results.WriteScenarioHeading("Home_06 - View a Specific Product - ProductID: " + Common.productID + " (by updating URL");
                home = home.NavigateToApplication();
                ViewProduct prod = home.ViewSpecificProduct(Common.productID);

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