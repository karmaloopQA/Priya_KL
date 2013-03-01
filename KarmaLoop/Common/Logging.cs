using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using System.IO;

namespace KarmaLoop
{
    public static class Logging
    {
        public static void LogStop(Exception e)
        {
            //string errorTime = System.DateTime.Now.ToString("MMddyyy-hhmmss");
            string screenshotName = Common.scenarioNumberForSS + ".jpg";
            string screenshotPath = Path.Combine(Path.GetFullPath(Common.currentReportLocation), screenshotName);
            Common.currentDriver._TakeScreenshot(screenshotPath);
            Results.WriteStatus("Fail", "<br><b>ERROR Message:</b><br>" + e.Message + "<br><br><b>StackTrace:</b><br> " + e.StackTrace + "<br><br>See <a href= .\\" + screenshotName + ">Screenshot<a> of the Page where the execution Stopped.");
            Results.MessageSkipRemainingSteps();
            Results.EndReport();
        }
    }
}
