using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using NUnit;
using System.Configuration;
using System.Web;
using System.Diagnostics;
//using System.Windows.Forms;

namespace KarmaLoop
{
    public static class Results
    {

        static String reporttime = System.DateTime.Now.ToString("yyyy-dd-MM.hh.mm.ss");
        static bool IsHTMLReportEnable = Convert.ToBoolean(ConfigurationSettings.AppSettings["EnableHTMLReporting"]);
        //static bool IsNUnitReportEnable = Convert.ToBoolean(ConfigurationSettings.AppSettings["EnableNUnitReporting"]);
        public static String HTMLResultsLocation = ConfigurationSettings.AppSettings["HTMLResultsLocation"];
        public static string folderName = "TestResults_" + reporttime;
        public static string ResultsDir = Path.Combine(Path.GetFullPath(HTMLResultsLocation), folderName);
        static DirectoryInfo x = System.IO.Directory.CreateDirectory(ResultsDir);
        public static int stepNo = 1;
        public static int scenarioPass = 0;
        public static int scenarioFail = 0;
        public static int failStep = 0;
        public static int passStep = 0;
        public static int warnStep = 0;

        public static String FileName = "TestResults_" + System.DateTime.Now.ToString("MMddyyy-hhmmss");

        public static String resultsFileName = ResultsDir + "\\" + FileName + ".html";


        public static void WriteTestSuiteHeading(string testsuite)
        {
            Common.currentReportLocation = ResultsDir;
            Common.currentTestSuite = testsuite;

            if (IsHTMLReportEnable)
            {
                System.IO.StreamWriter reportmsg = new System.IO.StreamWriter(resultsFileName, true);
                reportmsg.Close();
                System.IO.StreamReader reportread = new System.IO.StreamReader(resultsFileName);
                string arr = reportread.ReadToEnd();
                reportread.Close();
                System.IO.StreamWriter reportmsg1 = new System.IO.StreamWriter(resultsFileName, true);

                //Console.WriteLine("Array:"+ arr.Length);

                if (arr.Length == 0)
                {
                    File.Copy(Path.Combine(Path.Combine(HTMLResultsLocation, "images"), "logo.png"), Path.Combine(ResultsDir, "logo.png"));
                    reportmsg1.WriteLine("<h1><img src='.\\logo.png'</h1><br>");
                    reportmsg1.WriteLine("<script>function printpage(){window.print()}</script><input type='button' value='Print This Page?' onclick='printpage()' style='float: right;'/><br>");
                }

                reportmsg1.WriteLine("<font face='Calibri' size='3'><h2>");
                reportmsg1.WriteLine("Test Suite Name: " + testsuite);
                reportmsg1.WriteLine("</h2>");
                reportmsg1.WriteLine("</font>");
                reportmsg1.Close();
            }
            Console.WriteLine("Test Suite Name: " + testsuite);
        }

        public static void WriteScenarioHeading(string scenarioname)
        {
            Common.currentTestScenario = scenarioname;
            Common.scenarioNumberForSS = scenarioname.Split('-')[0].Trim();
            stepNo = 1;
            passStep = 0;
            failStep = 0;
            warnStep = 0;
            if (IsHTMLReportEnable)
            {
                System.IO.StreamWriter reportmsg = new System.IO.StreamWriter(resultsFileName, true);
                //coding for url, OS, browser
                reportmsg.WriteLine("<br><font face='Calibri' size='3' color='green'>");
                reportmsg.WriteLine("<table border='1'><tr>");
                reportmsg.WriteLine("<td align='center'><b>Operating System:</b></td>");
                reportmsg.WriteLine("<td>" + OSInfo.GetOSName() + "</b></td>");
                reportmsg.WriteLine("</tr><tr>");
                //reportmsg.WriteLine("<td align='center'><b>Browser:</b></td>");
                //reportmsg.WriteLine("<td>" + Request.Browser.Browser +"</b></td>");
                //reportmsg.WriteLine("</tr><tr>");
                reportmsg.WriteLine("<td align='center'><b>URL:</b></td>");
                reportmsg.WriteLine("<td>" + Common.KarmaloopTestSite + "</td>");
                reportmsg.WriteLine("</tr></table>");

                reportmsg.WriteLine("<br><font face='Calibri' size='3'>");
                reportmsg.WriteLine("<table border='1'><tr>");
                reportmsg.WriteLine("<th>Scenario Name: " + scenarioname + "</th>");
                reportmsg.WriteLine("</tr></table>");
                reportmsg.WriteLine("<br>");
                reportmsg.WriteLine("<table border='1'>");
                reportmsg.WriteLine("<tr>");
                reportmsg.WriteLine("<th width=4%>Step #</th>");
                reportmsg.WriteLine("<th width=30%>Description</th>");
                reportmsg.WriteLine("<th width=13%>DateTime</th>");
                reportmsg.WriteLine("<th width=5%>Status</th>");
                reportmsg.WriteLine("<th>Information/Warning</th>");
                reportmsg.WriteLine("</tr>");

                reportmsg.Close();
            }
            Console.WriteLine(scenarioname);
        }

        public static void WriteStatus(string PassOrFail, string Message = "")
        {
            string color = string.Empty;
            string infoORwarn = string.Empty;
            if (IsHTMLReportEnable)
            {
                System.IO.StreamWriter reportmsg = new System.IO.StreamWriter(resultsFileName, true);
                if (PassOrFail.ToLower().Equals("pass"))
                {
                    color = "green";
                    infoORwarn = "INFO: ";
                }
                else if (PassOrFail.ToLower().Equals("fail"))
                {
                    color = "red";
                    infoORwarn = "ERROR: ";
                }
                if (PassOrFail.ToLower().Trim().Contains("warn"))
                {
                    color = "orange";
                    infoORwarn = "WARN: ";
                }

                reportmsg.WriteLine("<td align='center'><b><font color='" + color + "'>" + PassOrFail + "</font></b></td>");
                reportmsg.WriteLine("<td><font color='" + color + "'><b>" + infoORwarn + "</b>" + Message + "</font></td>");
                reportmsg.WriteLine("</tr>");
                reportmsg.Close();
            }
            if (PassOrFail.ToLower().Equals("pass"))
                passStep++;
            else if (PassOrFail.ToLower().Equals("fail"))
                failStep++;
            else
                warnStep++;

            stepNo++;

            Console.WriteLine("Status: " + PassOrFail);

            Message = Message.Replace("<b>", "");
            Message = Message.Replace("</b>", "");
            Message = Message.Replace("<br>", "\n");

            if (!Message.Equals(""))
            {
                if (Message.StartsWith("ERROR:"))
                    Console.WriteLine(Message);
                else
                    Console.WriteLine(infoORwarn + Message);
            }
        }

        public static void WriteStatusWithTable(string PassOrFail, string[,] ResultsArray, int rows)
        {
            int Height = rows;
            int Width = (ResultsArray.Length / Height);
            string color = string.Empty;
            if (IsHTMLReportEnable)
            {
                System.IO.StreamWriter reportmsg = new System.IO.StreamWriter(resultsFileName, true);

                if (PassOrFail.ToLower().Equals("pass"))
                    color = "green";
                else if (PassOrFail.ToLower().Equals("fail"))
                    color = "red";
                else if (PassOrFail.ToLower().Trim().Contains("warn"))
                    color = "orange";

                reportmsg.WriteLine("<td align='center'><b><font color='" + color + "'>" + PassOrFail + "</font></b></td>");
                reportmsg.WriteLine("<td><font face='Calibri' size='3'><table border = '1'>");

                for (int i = 0; i < Height; i++)
                {
                    reportmsg.WriteLine("<tr>");
                    for (int j = 0; j < Width; j++)
                    {
                        reportmsg.WriteLine("<td>");
                        if (ResultsArray[i, j] == "")
                            reportmsg.WriteLine("Null");
                        else
                            reportmsg.WriteLine(ResultsArray[i, j]);

                        reportmsg.WriteLine("</td>");
                    }
                    reportmsg.WriteLine("</tr>");
                }
                reportmsg.WriteLine("</table></font></td></tr>");
                reportmsg.Close();
            }
            if (PassOrFail.ToLower().Equals("pass"))
                passStep++;
            else if (PassOrFail.ToLower().Equals("fail"))
                failStep++;
            else
                warnStep++;
            stepNo++;

            Console.WriteLine("Status: " + PassOrFail);

            int[] LengthCounter = new int[Width];
            int TotalLength = 0;

            for (int a = 0; a < Width; a++)
            {
                int Chars = 0;
                for (int b = 0; b < Height; b++)
                {
                    string Text = ResultsArray[b, a];
                    int NewChars = Text.Length;
                    if (Chars < NewChars)
                        Chars = NewChars;
                }
                LengthCounter[a] = Chars;
                TotalLength = TotalLength + Chars + 3;
            }

            Console.WriteLine("Data in Grid:");

            for (int x = 0; x < (TotalLength + 2); x++)
                Console.Write("-");
            Console.Write("\n");

            for (int i = 0; i < Height; i++)
            {
                Console.Write("| ");
                for (int j = 0; j < Width; j++)
                {
                    string spaces = "";
                    int c = LengthCounter[j] - ResultsArray[i, j].Length;
                    for (int k = 0; k < c; k++)
                        spaces = spaces + " ";
                    Console.Write(ResultsArray[i, j] + spaces + " | ");
                }
                Console.Write("\n");
                for (int x = 0; x < (TotalLength + 1); x++)
                    Console.Write("-");
                Console.Write("\n");
            }
        }

        public static void WriteDescription(string StepDescription)
        {
            if (IsHTMLReportEnable)
            {
                System.IO.StreamWriter reportmsg = new System.IO.StreamWriter(resultsFileName, true);
                reportmsg.WriteLine("<tr>");
                reportmsg.WriteLine("<td>" + stepNo + "</td>");
                reportmsg.WriteLine("<td>" + " " + StepDescription + "</td>");
                reportmsg.WriteLine("<td>" + "[" + System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToLongTimeString() + "] " + "</td>");
                reportmsg.Close();
            }
            StepDescription = StepDescription.Replace("<br>", "\n");
            StepDescription = StepDescription.Replace("<b>", "");
            StepDescription = StepDescription.Replace("</b>", "");
            Console.WriteLine("\nStep #" + stepNo + "). " + StepDescription);
        }

        public static void EndReport()
        {
            if (IsHTMLReportEnable)
            {
                System.IO.StreamWriter reportmsg = new System.IO.StreamWriter(resultsFileName, true);
                reportmsg.WriteLine("</table><br><table border = '1'>");
                reportmsg.WriteLine("<tr><th>Status</th><th># of Steps</th></tr>");
                reportmsg.WriteLine("<tr><td><font color='green'>Pass</font></td><td align='center'><font color='green'>" + passStep + "</font></td></tr>");
                reportmsg.WriteLine("<tr><td><font color='orange'>Pass with Warning</font></td><td align='center'><font color='orange'>" + warnStep + "</font></td></tr>");
                reportmsg.WriteLine("<tr><td><font color='red'>Fail</font></td><td align='center'><font color='red'>" + failStep + "</font></td></tr>");
                reportmsg.WriteLine("</table></font>");
                reportmsg.Close();
            }
            Console.WriteLine("-------------------------");
            Console.WriteLine("No. of Steps are Pass: " + passStep);
            Console.WriteLine("No. of Steps are Pass with Warning: " + warnStep);
            Console.WriteLine("No. of Steps are Fail: " + failStep);
            Console.WriteLine("-------------------------");
            if (failStep == 0)
                scenarioPass++;
            else
                scenarioFail++;
        }

        public static void MessageSkipRemainingSteps()
        {
            if (IsHTMLReportEnable)
            {
                System.IO.StreamWriter reportmsg = new System.IO.StreamWriter(resultsFileName, true);
                reportmsg.WriteLine("<tr>");
                reportmsg.WriteLine("<TH colspan='5'>");
                reportmsg.WriteLine("<b><font color='blue'>");
                reportmsg.WriteLine("Skipping remaining steps due to above error.");
                reportmsg.WriteLine("</font></b>");
                reportmsg.WriteLine("</th>");
                reportmsg.WriteLine("</tr>");
                reportmsg.Close();
            }
            Console.WriteLine("\nSkipping remaining steps due to above error.\n");

        }

        public static void WriteStatusOfAllTheTests()
        {
            if (IsHTMLReportEnable)
            {
                System.IO.StreamWriter reportmsg = new System.IO.StreamWriter(resultsFileName, true);
                reportmsg.WriteLine("<br><font face='Calibri' size='3'><h3>************************************<br>");
                reportmsg.WriteLine("Final status of all the tests</h3><table border = '1'>");
                reportmsg.WriteLine("<tr><th><font color='blue'>Scenario Status</font></th>");
                reportmsg.WriteLine("<th><font color='blue'># of Scenarios</font></th></tr>");
                reportmsg.WriteLine("<tr><td><font color='green'>Pass/Warning</font></td><td align='center'>" + scenarioPass + "</td></tr>");
                reportmsg.WriteLine("<tr><td><font color='red'>Fail</font></td><td align='center'>" + scenarioFail + "</td></tr>");
                reportmsg.WriteLine("</table></font>");
                reportmsg.Close();
            }

            Console.WriteLine("-------------------------");
            Console.WriteLine("No. of Scenarios are Pass/Warning: " + scenarioPass);
            Console.WriteLine("No. of Scenarios are Fail: " + scenarioFail);
            Console.WriteLine("-------------------------");

            //Creation of NUnit HTML Report using Batch File:
            /*if (IsNUnitReportEnable)
            {

                try
                {
                    string str = ConfigurationSettings.AppSettings["NUnitBatchFile"];
                    System.Diagnostics.Process.Start(str).WaitForExit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
                }
            }*/
        }

        public static void RunGenerateSummaryBatchFile()
        {
            try
            {
                string str = ConfigurationSettings.AppSettings["NUnitBatchFile"];
                System.Diagnostics.Process.Start(str).WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
            }
        }



    }
}