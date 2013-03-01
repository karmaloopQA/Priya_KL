using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using KarmaLoop;
using OpenQA.Selenium.Remote;
using NUnit.Framework;
 
namespace KarmaLoop
{
    public class Base
    {
        #region Private Variables

        private FirefoxProfile _ffp;
        private InternetExplorerOptions _ie;
        private ChromeOptions _chrome;
        private DesiredCapabilities _cap;
        private IWebDriver _driver;

        #endregion

        #region Public Methods

        public Base(IWebDriver driver)
        {
            this._driver = driver;
        }

        public Base() { }

        public IWebDriver driver
        {
            get { return this._driver; }
            set { this._driver = value; }
        }

        public IWebDriver StartBrowser()
        {
            
            Common.WebBrowser = System.Configuration.ConfigurationSettings.AppSettings["Browser"].ToLower();
            string driverDir = System.Configuration.ConfigurationSettings.AppSettings["DriverDir"];
            string driverPath = "";
            switch (Common.WebBrowser.ToLower())
            {
                case "firefox":
                    _cap = DesiredCapabilities.Firefox();
                    _cap.SetCapability(CapabilityType.AcceptSslCertificates, true);
                    _ffp = new FirefoxProfile();
                    _ffp.AcceptUntrustedCertificates = true;
                    _driver = new FirefoxDriver(_ffp);
                    break;
                case "iexplore":
                    _cap = DesiredCapabilities.InternetExplorer();
                    _cap.SetCapability(CapabilityType.AcceptSslCertificates, true);
                    _ie = new InternetExplorerOptions();
                    _ie.IgnoreZoomLevel = true;
                    _ie.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    switch (OS.IsOS64Bit())
                    {
                        case true:
                            Console.WriteLine("Running the test on 64 bit Operating System.");
                            driverPath = driverDir + "\\x64_IE_driver";
                            Console.WriteLine("test");
                            Common.OSbit = 64;
                            break;
                        case false:
                            Console.WriteLine("Running the test on 32 bit Operating System.");
                            driverPath = driverDir + "\\Win32_IE_driver";
                            Common.OSbit = 32;
                            break;
                    }
                    _driver = new InternetExplorerDriver(driverPath, _ie, Common.DriverTimeout);
                    break;
                case "chrome":
                    driverPath = driverDir + "\\Chrome";
                    _cap = DesiredCapabilities.Chrome();
                    _cap.SetCapability(CapabilityType.AcceptSslCertificates, true);
                    _chrome = new ChromeOptions();
                    //_chrome.AddArgument("--new-window");
                    //_chrome.AddArguments("-user-data-dir=c:\\chrome --new-window %1");
                    _driver = new ChromeDriver(driverPath, _chrome, Common.DriverTimeout);

                    break;
            }

            _driver.Manage().Cookies.DeleteAllCookies();
            _driver.Manage().Window.Maximize();

            //navigateToApplication();

            return _driver;
        }

        #endregion

        #region Private Methods

        private void navigateToApplication()
        {
            Results.WriteDescription("Launching a application");
            driver.Navigate().GoToUrl(Common.KarmaloopTestSite);
            driver._Wait(1);
            Assert.AreEqual(Common.KarmaloopTestSite, driver._GetUrl());
            try
            {
                if (driver.WaitForElement("id", "monetate_emailSocialLbx", 10))
                    driver._sendKeys("id", "monetate_emailSocialLbx", "esc");
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("xxx");
            }
            Results.WriteStatus("Pass", "Application Launched successfully");

        }

        #endregion
    }
}