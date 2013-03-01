using System;
using System.Web;
using NUnit.Framework;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using KarmaLoop;
using System.Net;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Interactions;
using System.Diagnostics;
using OpenQA.Selenium.Support.UI;
using System.Text;
//using SHDocVw;

namespace OpenQA.Selenium
{
    public static class Controls
    {

        public static void clickSaveAndConfirm(this IWebDriver driver)
        {
            Results.WriteDescription("Verification of Confirmation message box and accept it.");
            driver._SelectFrameToDefaultContent();
            driver._SelectFrameFromDefaultContent("name", "main");
            driver.Select_Frame_withinFrame("id", "igTabStrip_frame0");
            driver._Click("id", "ctl00xnrToolbarMainxUltraWebToolbar1_Item_4_img");
            IAlert alert;
        backTo:
            try
            {
                alert = driver.SwitchTo().Alert();
            }
            catch (Exception e)
            {
                System.Threading.Thread.Sleep(1);
                goto backTo;
            }
            IAlert Alert = driver.SwitchTo().Alert();
            //Assert.AreEqual(true, Alert.Text.Equals("Record saved successfully!"));
            string Msg = driver.SwitchTo().Alert().Text;
            Alert.Accept();
            Results.WriteStatus("Pass", "Confirmation message - '" + Msg + "' appear and accepted.");
        }

        // public static void closeUnwantedBrowser()
        //{
        //    switch (OS.IsOS64Bit())
        //    {
        //        case true:
        //            Console.WriteLine("Unable to Close Existing open browser if any!");
        //            break;

        //        case false:
        //            foreach (InternetExplorer ie in new ShellWindows())
        //            {
        //                //Console.WriteLine(ie.HWND + "IE");
        //                //Console.WriteLine(ie.LocationName);
        //                //Console.WriteLine(ie.LocationURL);
        //                if (ie.LocationName.Contains("NavRisk Claims"))
        //                {
        //                    //MessageBox.Show(ie.LocationName);
        //                    ie.Quit();                            
        //                }
        //            }
        //            break;                    
        //    }            
        //}

        public static void goToHome(this IWebDriver driver)
        {
            //driver._Click("id", "linkHdrHome");
            //Controls.closeWindowPopUp();
        }

        public static string _GetUrl(this IWebDriver driver)
        {
            driver._Wait(2);
            return driver.Url;
        }

        public static void Select_Frame_withinFrame(this IWebDriver driver, string When, string How)
        {
            IWebElement ele = driver.Search_Element(When, How);
            driver.SwitchTo().Frame(ele);
        }

        public static void Select_Frame_FromDefaultContent(this IWebDriver driver, string When, string How)
        {
            IWebElement ele = driver.Search_Element(When, How);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(How);
        }

        public static IWebElement Search_Element(this IWebDriver driver, string When, string How)
        {
            IWebElement element = null;
            switch (When.ToLower())
            {
                case "id":
                    element = driver.FindElement(By.Id(How));
                    break;
                case "css":
                case "CssSelector":
                    element = driver.FindElement(By.CssSelector(How));
                    break;
                case "name":
                    element = driver.FindElement(By.Name(How));
                    break;
                default:
                    //element = null;
                    //Console.WriteLine("Incorrect By Selector!");
                    break;
            }
            return element;

        }

        public static string RandomString(int size) 
        {
            
            string input = "abcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder builder = new StringBuilder();
            
            for (int i = 0; i < size; i++)
                builder.Append(input[new Random().Next(0, input.Length)]);
                         
            return builder.ToString();
        }

        public static string GenerateDummyEmail()
        {
            string[] domains = new string[] {"aol.com","hotmail.com","live.com","gmail.com","yahoo.com","ymail.com"};
            StringBuilder email = new StringBuilder();

            email.Append("dummy" + RandomString(5) + "@" + domains[new Random().Next(0,domains.Length)]);

            return email.ToString();
        }

        public static bool _VerifyURL(this IWebDriver driver, string URLToVerify)
        {
            driver._Wait(5);
            if (driver.Url.Equals(URLToVerify))
                return true;
            else
                return false;
        }

        public static bool _VerifyPartialURL(this IWebDriver driver, string URLToVerify)
        {
            driver._Wait(5);
            if (driver.Url.Contains(URLToVerify))
                return true;
            else
                return false;
        }
        public static bool _VerifyPartialTitle(this IWebDriver driver, string TextToVerify)
        {
            driver._Wait(2);

            if (driver.Title.ToLower().Contains(TextToVerify.ToLower()))
                return true;
            else
                return false;
        }

        public static bool _VerifyTitle(this IWebDriver driver, string TextToVerify)
        {
            driver._Wait(2);

            if (driver.Title.ToLower().Equals(TextToVerify.ToLower()))
                return true;
            else
                return false;
        }

        public static void _switchToBrowserTab(this IWebDriver driver, bool isNew = true)
        {
            string currentWindowHandle = driver.WindowHandles[0];
            if (!driver.WindowHandles.Count.Equals(1))
            {
                if (isNew)
                {
                    string newHandle = string.Empty;
                    ReadOnlyCollection<string> windowHandles = driver.WindowHandles;
                    //int i = 1;
                    foreach (string handle in windowHandles)
                    {
                        if (!handle.Equals(currentWindowHandle))
                        {
                            //Console.WriteLine("Handle #" + i++ + " : " + handle);
                            newHandle = handle;
                            break;
                        }
                    }

                    driver.SwitchTo().Window(newHandle);
                }
                else
                {
                    if (!driver.WindowHandles.Count.Equals(1))
                    {
                        for (int w = 1; w < driver.WindowHandles.Count; w++)
                        {
                            IWebDriver windowDriver = driver.SwitchTo().Window(driver.WindowHandles[w]);
                            windowDriver.Close();
                        }
                    }
                    driver.SwitchTo().Window(currentWindowHandle);
                }
                driver._Wait(3);
            }
            else
                Console.WriteLine("Only 1 window is available in current driver.");
        }

        public static void _browserBack(this IWebDriver driver)
        {
            driver.Navigate().Back();
            driver._Wait(1);
        }

        public static void _browserForward(this IWebDriver driver)
        {
            driver.Navigate().Forward();
            driver._Wait(1);
        }

        public static void _browserRefresh(this IWebDriver driver)
        {
            driver.Navigate().Refresh();
            driver._Wait(1);
        }

        public static bool _WaitForElementToBeHidden(this IWebDriver driver, string When, string How, int timeout = 30)
        {
            int i;
            bool isHidden = false;
            for (i = 0; i < timeout; i++)
            {
                if (driver._IsElementPresent(When, How))
                    Thread.Sleep(1000);
                else
                {
                    isHidden = true;
                    break;
                }
            }
            Assert.AreNotEqual(timeout, i, "Waiting Time exceed.");

            return isHidden;
        }

        public static void WaitForElementToBePopulated(this IWebDriver driver, string When, string How, int timeout = 10)
        {
            for (int i = 0; i < timeout; i++)
            {
                //Console.WriteLine("Iteration " + (i + 1));
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
                if (driver._IsElementPresent(When, How))
                {
                    //Console.WriteLine("Found");
                    break;
                }
            }
        }

        public static bool WaitForElement(this IWebDriver driver, string When, string How, int timeout = 60)
        {
            bool isDisplayed = false;
            for (int i = 0; i < timeout; i++)
            {
                //Console.WriteLine(i + 1);
                System.Threading.Thread.Sleep(1000);
                //driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
                if (driver._IsElementPresent(When, How))
                {
                    isDisplayed = true;
                    break;
                }
            }
            return isDisplayed;
        }

        public static bool _IsElementPresent(this IWebDriver driver, string When, string How)
        {
            bool isDisplayed = false;

            try
            {
                switch (When.ToLower())
                {
                    case "id":
                        isDisplayed = driver.FindElement(By.Id(How)).Displayed;
                        break;
                    case "css":
                    case "CssSelector":
                        isDisplayed = driver.FindElement(By.CssSelector(How)).Displayed;
                        break;
                    case "name":
                        isDisplayed = driver.FindElement(By.Name(How)).Displayed;
                        break;
                    case "xpath":
                        isDisplayed = driver.FindElement(By.XPath(How)).Displayed;
                        break;
                    case "linktext":
                        isDisplayed = driver.FindElement(By.LinkText(How)).Displayed;
                        break;
                    case "class":
                    case "classname":
                        isDisplayed = driver.FindElement(By.ClassName(How)).Displayed;
                        break;
                    default:
                        Console.WriteLine("Incorrect By Selector!>>>>>Chosen Selector: " + When);
                        break;
                }
            }
            catch (NoSuchElementException)
            {
                isDisplayed = false;
            }

            return isDisplayed;

        }

        public static void _UploadFile(this IWebDriver driver, string When, string How, string File)
        {
            IWebElement element = driver._FindElement(When, How);
            element.SendKeys(File);
        }

        public static string _getHref(this IWebDriver driver, string When, string How)
        {
            IWebElement element = driver._FindElement(When, How);
            return element.GetAttribute("href").ToString();
        }

        public static string _GetSrc(this IWebDriver driver, string When, string How)
        {
            IWebElement element = driver._FindElement(When, How);
            return element.GetAttribute("src").ToString();
        }

        public static void _Wait(this IWebDriver driver, int x)
        {
            for (int i = 0; i <= x; i++)
            {
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
            }
        }

        public static bool _VerifyText(this IWebDriver driver, string When, string How, string TextToVerify)
        {
            IWebElement element = driver._FindElement(When, How);
            Console.WriteLine("Verifing Text: " + element.Text);
            if (element.Text.ToLower().Equals(TextToVerify.ToLower()))
                return true;
            else
                return false;
        }

        public static bool _VerifyPartialText(this IWebDriver driver, string When, string How, string PartialTextToVerify)
        {
            IWebElement element = driver._FindElement(When, How);
            if (element.Text.Contains(PartialTextToVerify))
                return true;
            else
                return false;
        }

        public static bool _VerifyValue(this IWebDriver driver, string When, string How, string ValueToVerify)
        {
            string ActualText = driver._GetValue(When, How);
            if (ValueToVerify.Equals(ActualText))
                return true;
            else
                return false;
        }

        //Drop Down List
        public static bool _IsOptionSelected(this IWebDriver driver, string When, string How, string TextToVerify)
        {
            bool IsAvail = false;
            IWebElement Element = driver._FindElement(When, How);
            IList<IWebElement> options = Element.FindElements(By.TagName("option"));
            foreach (IWebElement option in options)
            {
                if (option.Text.ToLower().Equals(TextToVerify.ToLower()))
                {
                    IsAvail = option.Selected;
                    break;
                }
                else
                {
                    if (option.Text.ToLower().StartsWith(TextToVerify.ToLower()))
                    {
                        IsAvail = option.Selected;
                        break;
                    }
                    else
                        IsAvail = false;
                }
            }
            return IsAvail;

        }

        public static string _GetSelectedText(this IWebDriver driver, string When, string How)
        {
            string DDText = "";
            IWebElement Element = driver._FindElement(When, How);
            IList<IWebElement> options = Element.FindElements(By.TagName("option"));
            foreach (IWebElement option in options)
            {
                if (option.Selected)
                {
                    DDText = option.Text;
                    break;
                }
            }
            return DDText;
        }

        public static bool _IsRadioEnabled(this IWebDriver driver, string When, string How)
        {
            IWebElement element = driver._FindElement(When, How);

            return element.Enabled;
        }

        public static void _SelectOption(this IWebDriver driver, string When, string How, string Value)
        {
            bool IsAvail = false;
            IWebElement Element = driver._FindElement(When, How);
            IList<IWebElement> options = Element.FindElements(By.TagName("option"));
            foreach (IWebElement option in options)
            {
                if (option.Text.ToLower().Equals(Value.ToLower()))
                {
                    //Console.WriteLine(option.Text + " With equal");
                    option.Click();
                    IsAvail = true;
                    break;
                }
                else
                {
                    if (option.Text.ToLower().Contains(Value.ToLower()))
                    {
                        //Console.WriteLine(option.Text + " With Contains");
                        option.Click();
                        IsAvail = true;
                        break;
                    }
                    else
                        IsAvail = false;
                }
            }
            //Console.WriteLine(IsAvail);
            if (!IsAvail)
            {
                //Console.WriteLine(!IsAvail + " In If");
                Console.WriteLine("ERROR: Option is not Available!");
                Assert.Fail("Expected value is not available" + Value);
            }
        }

        public static IList<IWebElement> _GetDropDownOptions(this IWebDriver driver, string When, string How)
        {
            IWebElement Element = driver._FindElement(When, How);
            IList<IWebElement> options = Element.FindElements(By.TagName("option"));

            return options;
        }

        public static bool _IsOptionAvailable(this IWebDriver driver, string When, string How, string OptionValue)
        {
            bool IsAvailable = false;
            IWebElement Element = driver._FindElement(When, How);
            IList<IWebElement> options = Element.FindElements(By.TagName("option"));
            foreach (IWebElement option in options)
            {
                if (option.Text.ToLower().Equals(OptionValue.ToLower()))
                {
                    IsAvailable = true;
                    break;
                }
                else
                {
                    if (option.Text.ToLower().StartsWith(OptionValue.ToLower()))
                    {
                        IsAvailable = true;
                        break;
                    }
                }
            }
            return IsAvailable;

        }

        //Text field
        public static void _Type(this IWebDriver driver, string When, string How, String TextToInput)
        {
            driver.WaitForElementToBePopulated(When, How);
            IWebElement ele = driver._FindElement(When, How);
            ele.Clear();
            ele.SendKeys(TextToInput);
        }

        public static void _sendKeys(this IWebDriver driver, string When, string How, String key)
        {
            IWebElement ele = driver._FindElement(When, How);
            switch (key.ToLower().Trim())
            {
                case "tab":
                    ele.SendKeys(Keys.Tab);
                    break;
                case "alt":
                    ele.SendKeys(Keys.Alt);
                    break;
                case "esc":
                    ele.SendKeys(Keys.Escape);
                    break;
                default:
                    Console.WriteLine("Key is not added in the case of _sendKeys function.");
                    break;
            }
        }


        public static void _TypeAndHitEnter(this IWebDriver driver, string When, string How, String TextToInput)
        {
            IWebElement ele = driver._FindElement(When, How);
            ele.Clear();
            ele.SendKeys(TextToInput);
            ele.SendKeys(Keys.Enter);
        }

        //link, text, button
        public static void _Click(this IWebDriver driver, string When, string How, bool waitForURLChange = false)
        {
            driver.WaitForElementToBePopulated(When, How);
            IWebElement ele = driver._FindElement(When, How);
            string currURL = driver.Url;
            ele.Click();

            if (waitForURLChange)
            {
                for (int i = 0; i < 60; i++)
                {
                    if (driver.Url.Equals(currURL))
                        break;
                    else
                        driver._Wait(1);
                }
                if (currURL.Equals(driver.Url))
                    Assert.Fail("URL isn't Changed.");
            }

        }

        public static void _Click(this IWebDriver driver, string When, string How, int index, bool waitForURLChange = false)
        {
            //driver.WaitForElementToBePopulated(When, How);
            IList<IWebElement> list = driver._FindElements(When, How);
            string currURL = driver.Url;
            list[index-1].Click();

            if (waitForURLChange)
            {
                for (int i = 0; i < 60; i++)
                {
                    if (driver.Url.Equals(currURL))
                        break;
                    else
                        driver._Wait(1);
                }
                if (currURL.Equals(driver.Url))
                    Assert.Fail("URL isn't Changed.");
            }

        }

        public static void doubleClick(this IWebDriver driver, string When, string How)
        {
            IWebElement ele = driver._FindElement(When, How);
            Actions action = new Actions(driver);
            action.DoubleClick();
            action.Perform();
        }
        //Frame
        public static void _SelectFrameWithinFrame(this IWebDriver driver, string When, string How)
        {
            IWebElement ele = driver._FindElement(When, How);
            driver.SwitchTo().Frame(ele);
        }

        //Frame
        public static void _SelectFrameFromDefaultContent(this IWebDriver driver, string When, string How)
        {
            IWebElement ele = driver._FindElement(When, How);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(How);
        }

        public static void _switchToMainFrame(this IWebDriver driver)
        {
            while (!driver._IsElementPresent("name", "main"))
            {
                //Console.WriteLine("In while loop sodho sodho");
                driver._SelectFrameToDefaultContent();
            }
        }

        public static void _SelectFrameToDefaultContent(this IWebDriver driver)
        {
            driver.SwitchTo().DefaultContent();
        }
        //find longest text in an array

        public static int[] _GetLongestTextInColumn(string[,] TableData, int rows)
        {
            int Height = rows;
            int Width = TableData.Length / rows;
            int[] LengthCounter = new int[Width];
            //int TotalLength = 0;

            for (int a = 0; a < Width; a++)
            {
                int Chars = 0;
                for (int b = 0; b < Height; b++)
                {
                    string Text = TableData[b, a];
                    int NewChars = Text.Length;
                    if (Chars < NewChars)
                        Chars = NewChars;
                }
                LengthCounter[a] = Chars;
                //TotalLength = TotalLength + Chars + 3;
            }

            return LengthCounter;
        }

        //Table
        public static int _GetNoOfRows(this IWebDriver driver, string When, string How)
        {
            IWebElement ele = driver._FindElement(When, How);
            int TR_count = ele.FindElements(By.TagName("tr")).Count;
            int rows = 0;
            /*if (ele.FindElement(By.TagName("th")).Displayed)
                rows = TR_count - 1;
            else*/

            rows = TR_count;

            return rows;
        }

        //Get Table data that doesn't have <th> tag in the table
        public static string[,] _GetGridDataWithoutTHTag(this IWebDriver driver, string When, string How)
        {
            IWebElement element = driver._FindElement(When, How);

            IList<IWebElement> RowElements = element.FindElements(By.TagName("tr"));
            IList<IWebElement> ColElements = element.FindElements(By.TagName("td"));

            int RowCount = RowElements.Count;
            int ColCount = (ColElements.Count / RowElements.Count);

            //Console.WriteLine("Rows: " + RowCount);
            //Console.WriteLine("Cols: " + ColCount);

            string[,] GridData = new string[RowCount, ColCount];

            for (int i = 0; i < RowCount; i++)
            {
                IList<IWebElement> iRow = RowElements[i].FindElements(By.TagName("td"));
                for (int j = 0; j < ColCount; j++)
                {
                    //Console.WriteLine("[{0},{1}] - {2}", i, j, iRow[j].Text);
                    GridData[i, j] = iRow[j].Text;
                }
            }

            return GridData;
        }

        //Get Table data that have <th> tag in the table
        public static string[,] _GetGridData(this IWebDriver driver, string When, string How)
        {
            IWebElement element = driver._FindElement(When, How);

            IList<IWebElement> HeaderElements = element.FindElements(By.TagName("th"));
            IList<IWebElement> RowElements = element.FindElements(By.TagName("tr"));

            string[,] GridData = new string[RowElements.Count, HeaderElements.Count];

            //Console.WriteLine(GridData.Length);
            for (int i = 0; i < RowElements.Count; i++)
            {
                if (i == 0)
                {
                    IList<IWebElement> Headers = RowElements[i].FindElements(By.TagName("th"));
                    for (int k = 0; k < Headers.Count; k++)
                    {
                        //Console.WriteLine(Headers[k].Text);
                        GridData[i, k] = Headers[k].Text;
                    }
                }
                else
                {
                    IList<IWebElement> Cols = RowElements[i].FindElements(By.TagName("td"));
                    for (int j = 0; j < Cols.Count; j++)
                    {
                        //Console.WriteLine(Cols[j].Text);
                        GridData[i, j] = Cols[j].Text;
                    }
                }
            }
            return GridData;
        }

        //Returns Text
        public static string _GetText(this IWebDriver driver, string When, string How)
        {
            IWebElement ele = driver._FindElement(When, How);
            //Console.WriteLine(ele.Text);
            return ele.Text;
        }

        public static string _GetText(this IWebDriver driver, string When1, string How1, string When2, string How2)
        {
            IWebElement ele = driver._FindElement(When1, How1)._FindElement(When2,How2);
            
            return ele.Text;
        }

        public static string _GetValue(this IWebDriver driver, string When, string How)
        {
            IWebElement ele = driver._FindElement(When, How);

            return ele.GetAttribute("Value");
        }

        public static string _GetValue(this IWebDriver driver, string When1, string How1, string When2, string How2)
        {
            IWebElement ele = driver._FindElement(When1, How1)._FindElement(When2, How2);

            return ele.GetAttribute("Value");
        }

        //Radio button
        public static bool _IsRadioSelected(this IWebDriver driver, string When, string How)
        {
            IWebElement ele = driver._FindElement(When, How);

            return ele.Selected;
        }

        public static string _getValueOfSelectedRadio(this IWebDriver driver, string When, string How)
        {
            IList<IWebElement> radios = driver._FindElements(When, How);
            string radioText = "none";
            foreach (IWebElement radio in radios)
            {
                if (radio.Selected)
                {
                    radioText = radio.GetAttribute("Value");
                    break;
                }
            }
            return radioText;
        }

        public static bool _isChecked(this IWebDriver driver, string When, string How)
        {
            IWebElement element = driver._FindElement(When, How);
            return element.Selected;
        }

        public static void _selectCheckBox(this IWebDriver driver, string When, string How)
        {
            IWebElement element = driver._FindElement(When, How);

            if (!element.Selected)
                element.Click();
        }

        public static void _deselectCheckBox(this IWebDriver driver, string When, string How)
        {
            IWebElement element = driver._FindElement(When, How);

            if (element.Selected)
                element.Click();
        }

        public static void _SelectRadioOption(this IWebDriver driver, string When, string How)
        {
            IWebElement ele = driver._FindElement(When, How);
            if (!ele.Selected)
            {
                ele.Click();
            }
        }

        public static string _SelectRandomRadioOption(this IWebDriver driver, string When1, string How1, string When2, string How2)
        {
            IList<IWebElement> options = driver._FindElement(When1, How1)._FindElementsWithinElement(When2,How2);
            Assert.AreNotEqual(0, options.Count, "Options are not found.");
            int rand = new Random().Next(0, options.Count-1);
            string text = options[rand].Text;
            if (!options[rand].Selected)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", options[rand]);
                options[rand].Click();
            }
            return text;
        }

        //List Box
        public static string[] _GetListBoxText(this IWebDriver driver, string When, string How)
        {
            IWebElement element = driver._FindElement(When, How);
            IList<IWebElement> options = element.FindElements(By.TagName("option"));

            string[] ListBoxData = new string[options.Count];
            if (options.Count != 0)
            {
                int i = 0;
                foreach (IWebElement option in options)
                {
                    ListBoxData[i++] = option.Text;
                }
            }
            return ListBoxData;
        }

        private static IWebElement _FindElement(this IWebDriver driver, string When, string How)
        {
            IWebElement element = null;
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    switch (When.ToLower().Trim())
                    {
                        case "id":
                            driver.WaitForElementToBePopulated(When, How);
                            element = driver.FindElement(By.Id(How));
                            break;
                        case "css":
                        case "cssselector":
                            driver.WaitForElementToBePopulated(When, How);
                            element = driver.FindElement(By.CssSelector(How));
                            break;
                        case "name":
                            driver.WaitForElementToBePopulated(When, How);
                            element = driver.FindElement(By.Name(How));
                            break;
                        case "xpath":
                            driver.WaitForElementToBePopulated(When, How);
                            element = driver.FindElement(By.XPath(How));
                            break;
                        case "class":
                        case "classname":
                            driver.WaitForElementToBePopulated(When, How);
                            element = driver.FindElement(By.ClassName(How));
                            break;
                        case "linktext":
                            driver.WaitForElementToBePopulated(When, How);
                            element = driver.FindElement(By.LinkText(How));
                            break;
                        case "tag":
                        case "tagname":
                            driver.WaitForElementToBePopulated(When, How);
                            element = driver.FindElement(By.TagName(How));
                            break;
                        default:
                            //element = null;
                            Console.WriteLine("Incorrect By Selector!>>>>>Chosen Selector: " + When);
                            break;
                    }
                }
                catch (NoSuchElementException)
                {
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
                    continue;
                }
                catch (ElementNotVisibleException)
                {
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
                    continue;
                }

                if (element.Displayed)
                {
                    break;
                }
                else
                {
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
                }

            }

            return element;

        }

        public static IWebElement _FindElement(this IWebElement ele, string When, string How)
        {
            IWebElement element = null;
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    switch (When.ToLower().Trim())
                    {
                        case "id":
                            //ele.WaitForElementToBePopulated(When, How);
                            element = ele.FindElement(By.Id(How));
                            break;
                        case "css":
                        case "cssselector":
                            //driver.WaitForElementToBePopulated(When, How);
                            element = ele.FindElement(By.CssSelector(How));
                            break;
                        case "name":
                            //ele.WaitForElementToBePopulated(When, How);
                            element = ele.FindElement(By.Name(How));
                            break;
                        case "xpath":
                            //driver.WaitForElementToBePopulated(When, How);
                            element = ele.FindElement(By.XPath(How));
                            break;
                        case "class":
                        case "classname":
                            //driver.WaitForElementToBePopulated(When, How);
                            element = ele.FindElement(By.ClassName(How));
                            break;
                        case "linktext":
                            //driver.WaitForElementToBePopulated(When, How);
                            element = ele.FindElement(By.LinkText(How));
                            break;
                        case "tag":
                        case "tagname":
                            //driver.WaitForElementToBePopulated(When, How);
                            element = ele.FindElement(By.TagName(How));
                            break;
                        default:
                            //element = null;
                            Console.WriteLine("Incorrect By Selector!>>>>>Chosen Selector: " + When);
                            break;
                    }
                }
                catch (NoSuchElementException)
                {
                    System.Threading.Thread.Sleep(1000);
                    continue;
                }
                catch (ElementNotVisibleException)
                {
                    System.Threading.Thread.Sleep(1000);
                    continue;
                }

                if (element.Displayed)
                {
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(1000);
                }

            }

            return element;

        }

        public static IList<IWebElement> _FindElements(this IWebDriver driver, string When, string How)
        {
            IList<IWebElement> elements = null;
            for (int i = 0; i <= 30; i++)
            {
                try
                {
                    switch (When.ToLower())
                    {
                        case "id":
                            elements = driver.FindElements(By.Id(How));
                            break;
                        case "css":
                        case "cssselector":
                            elements = driver.FindElements(By.CssSelector(How));
                            break;
                        case "name":
                            elements = driver.FindElements(By.Name(How));
                            break;
                        case "xpath":
                            elements = driver.FindElements(By.XPath(How));
                            break;
                        case "class":
                        case "classname":
                            elements = driver.FindElements(By.ClassName(How));
                            break;
                        case "tagname":
                        case "tag":
                            elements = driver.FindElements(By.TagName(How));
                            break;
                        default:
                            //element = null;
                            Console.WriteLine("Incorrect By Selector!");
                            break;
                    }
                }
                catch (NoSuchElementException)
                {
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
                    continue;
                }
                catch (ElementNotVisibleException)
                {
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
                    continue;
                }
            }

            return elements;

        }

        public static IList<IWebElement> _FindElementsWithinElement(this IWebElement element, string When, string How)
        {
            IList<IWebElement> elements = null;

            switch (When.ToLower())
            {
                case "id":
                    elements = element.FindElements(By.Id(How));
                    break;
                case "css":
                case "cssselector":
                    elements = element.FindElements(By.CssSelector(How));
                    break;
                case "name":
                    elements = element.FindElements(By.Name(How));
                    break;
                case "xpath":
                    elements = element.FindElements(By.XPath(How));
                    break;
                case "class":
                case "classname":
                    elements = element.FindElements(By.ClassName(How));
                    break;
                case "tag":
                case "tagname":
                    elements = element.FindElements(By.TagName(How));
                    break;
                default:
                    //element = null;
                    Console.WriteLine("Incorrect By Selector!");
                    break;
            }
            return elements;
        }

        public static string _SelectRandomElementFromElements(this IWebDriver driver, string When1, string How1, string When2, string How2)
        {
            IList<IWebElement> list = driver._FindElementsFromElement(When1, How1, When2, How2);
            int rand = new Random().Next(0, list.Count);
            string text = list[rand].Text;
            list[rand].Click();

            return text;
        }

        public static string _SelectSpecificElementFromElements(this IWebDriver driver, string When1, string How1, string When2, string How2, string textToClick)
        {
            IList<IWebElement> list = driver._FindElementsFromElement(When1, How1, When2, How2);
            bool isAvail = false;
            string text = string.Empty;
            foreach (IWebElement e in list)
            {
                if (e.Text.ToLower().Equals(textToClick.ToLower()))
                {
                    e.Click();
                    text = e.Text;
                    isAvail = true;
                }
            }

            if (!isAvail)
                Assert.Fail("Option isn't available.");

            return text;
        }

        public static IList<IWebElement> _FindElementsFromElement(this IWebDriver driver, string When1, string How1, string When2, string How2)
        {
            return driver._FindElement(When1, How1)._FindElementsWithinElement(When2, How2);
        }

        public static void _TakeScreenshot(this IWebDriver driver, string saveLocation)
        {
            ITakesScreenshot screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();
            //Console.WriteLine("loc: " + saveLocation);
            screenshot.SaveAsFile(saveLocation, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        /*
        public static IJavaScriptExecutor Scripts(this IWebDriver driver)
        {
            return (IJavaScriptExecutor)driver;
        }
        public static void checkForBrokenImages(this IWebDriver driver)
        {
            
            IList<IWebElement> allImages = driver.FindElements(By.TagName("img"));
            //Console.WriteLine(driver.Scripts().ExecuteScript("return arguments[0].complete","https://aa.xx.com").ToString());
            foreach (IWebElement image in allImages)
            {
                Console.Write(">> " + image.GetAttribute("src"));
                bool loaded = Convert.ToBoolean(driver.Scripts().ExecuteScript("return arguments[0].complete", image));
                
                if (!loaded)
                {
                    Console.Write(">>>>>>>>>>>>>>>>>>>> Broken");
                }
                Console.Write("\n");
            }
            allImages = null;
        }

        public static void ProcessRequest(this IWebDriver driver)
        {
            
            //request.Credentials = MyCredentialCache;

            IList<IWebElement> allImages = driver.FindElements(By.TagName("img"));
            Console.WriteLine(allImages.Count);
            foreach (IWebElement image in allImages)
            {
                try
                {
                    Console.WriteLine(">> " + image.GetAttribute("src"));
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(image.GetAttribute("src"));
                    Console.WriteLine(request.Timeout);
                    //request.Timeout = 10000;
                    request.Method = "HEAD";
                    request.GetResponse();
                }
                catch (WebException ex)
                {
                    if (ex.Status != WebExceptionStatus.ProtocolError)
                    {
                        Console.Write(">>>>>>>>>>>>>>>>>>>> Broken");
                        //throw ex;
                    }
                }
            }            
        }*/

        public static string[] checkForBrokenImages(this IWebDriver driver, string When = "", string How = "")
        {
            IList<IWebElement> allImages;
            string[] resWithStatus = { "", "" };
            if (When.Equals("") && How.Equals(""))
            {
                allImages = driver.FindElements(By.TagName("img"));
            }
            else
            {
                IWebElement element = driver._FindElement(When, How);
                allImages = element.FindElements(By.TagName("img"));
            }
            resWithStatus[0] = resWithStatus[0] + "Total Images found for this Product are: " + allImages.Count + "<br>";
            int vendorcount = 0;
            int failCount = 0;
            foreach (IWebElement image in allImages)
            {
                string url = image.GetAttribute("src");
                if (url.ToLower().Contains("vendor"))
                {
                    vendorcount++;
                    if (RemoteFileExists(url) == "false")
                    {
                        resWithStatus[0] = resWithStatus[0] + "Broken URL: " + url + "<br>";
                        failCount++;
                    }
                }
                //Console.WriteLine(url);
            }
            if (failCount.Equals(0))
            {
                resWithStatus[0] = resWithStatus[0] + "all the images are visible.<br>";
                resWithStatus[1] = "Pass";
            }
            else
                resWithStatus[1] = "Fail";
            //Console.WriteLine("Vendor wali images: " + vendorcount);
            return resWithStatus;
        }

        private static string RemoteFileExists(string url)
        {
            try
            {
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                request.Timeout = 10000;
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                response.Close();
                //Returns TURE if the Status code == 200
                return response.StatusCode.ToString();
            }
            catch
            {
                //Any exception will returns false.
                return "false";
            }
        }

        //public static void closeWindowPopUp()
        //{
        //    AutoItX3 autoit = new AutoItX3();
        //    autoit.WinWaitActive("Windows Internet Explorer", "", 15);

        //    if (autoit.WinExists("Windows Internet Explorer", "Are you sure you want to leave this page?") == 1 )
        //    {
        //        autoit.WinActive("Windows Internet Explorer");
        //        autoit.ControlClick("Windows Internet Explorer", "Are you sure you want to leave this page?","", "LEFT", 1);
        //        autoit.MouseClick();
        //        //autoit.Send("{ENTER}");
        //    }
        //}


    }

}