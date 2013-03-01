using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace KarmaLoop
{
    public static class Common
    {
        private static TimeSpan _defaultTimeSpan = new TimeSpan(0, 0, 30);
 
        public static string WebBrowser { get; set; }
        public static TimeSpan DriverTimeout
        {
            get { return _defaultTimeSpan; }
            set { _defaultTimeSpan = value; }
        }
        public static IWebDriver currentDriver { get; set; }
        public static int OSbit { get; set; }
        public static string userID { get; set; }
        public static string passwd { get; set; }
        public static string currentReportLocation { get; set; }
        public static string currentTestSuite { get; set; }
        public static string KarmaloopTestSite { get; set; }
        public static string currentTestScenario { get; set; }
        public static string scenarioNumberForSS { get; set; }
        public static bool comingBackToPreviousStep { get; set; }
        public static string searchedKeyword { get; set; }
        public static string productID { get; set; }
    }
    
}