using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Configuration;
//using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net.Mail;
//using Ionic.Zip;
using System.IO;

namespace KarmaLoop.KL
{
    [SetUpFixture]
    public class SetupFixture
    {
        // Runs before all tests

        [SetUp]
        public void SetUp()
        {
            string environment = ConfigurationSettings.AppSettings["KarmaloopTestEnvironment"];            
            Common.KarmaloopTestSite = environment;            
            Common.userID = System.Configuration.ConfigurationSettings.AppSettings["UserID"];
            Common.passwd = System.Configuration.ConfigurationSettings.AppSettings["Password"];
            Common.comingBackToPreviousStep = false;
        }

        [TearDown]
        public void TearDown()
        {
            Common.KarmaloopTestSite = null;
            Common.DriverTimeout = new TimeSpan(0, 0, 60);
            Common.WebBrowser = null;
            Results.WriteStatusOfAllTheTests();
        //    DialogResult Dres = MessageBox.Show("Test(s) Execution Complete! \n" + "No Of Tests: " + (Results.scenarioPass + Results.scenarioFail) + "\nPass: " + Results.scenarioPass + "\nFail: " + Results.scenarioFail + "\nDo you want to send Report via Email?" , "NavRisk Claim Automation" , MessageBoxButtons.YesNo, MessageBoxIcon.Information );

        //    if (Dres == DialogResult.Yes)
        //    {
        //        Form prompt = new Form();
        //        prompt.Width = 500;
        //        prompt.Height = 200;
        //        prompt.Text = "Email";
        //        Label textLabel = new Label() { Left = 50, Top = 20, Width = 200, Text = "Enter Email Address!" };
        //        TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 200 };
        //        Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70 };
        //        confirmation.Click += (sender, e) => { prompt.Close(); };
        //        prompt.Controls.Add(confirmation);
        //        prompt.Controls.Add(textLabel);
        //        prompt.Controls.Add(textBox);
        //        prompt.ShowDialog();                

        //        test1:

        //        if(IsEmail(textBox.Text))
        //        {
        //            MessageBox.Show("Send Email to: " + textBox.Text);

        //            MailMessage mail = new MailMessage();
        //            SmtpClient SmtpServer = new SmtpClient("smtp.1and1.com");
        //            mail.From = new MailAddress("karmaloop-qa@priyanet.com");
        //            mail.To.Add(textBox.Text);
        //            mail.Subject = "NavRisk Automation " + Results.FileName;
        //            mail.Body = "Find Attached Report for Automation test execution";                               

        //            Attachment attach = new Attachment(CompressFolder());
        //            mail.Attachments.Add(attach);
                
        //            SmtpServer.Port = 25;
        //            SmtpServer.Credentials = new System.Net.NetworkCredential("karmaloop-qa@priyanet.com", "success");
        //            SmtpServer.EnableSsl = true;
                
        //            SmtpServer.Send(mail);
        //            MessageBox.Show("Email Send");                    

        //        }
        //        else
        //        {
        //            prompt.ShowDialog();
        //            goto test1;
        //        }             
                           
                
        //    }           
            
        //}

        //public static bool IsEmail(string email)
        //{
        //    string MatchEmailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@" + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\." + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|" + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
        //    if (email != null) return Regex.IsMatch(email, MatchEmailPattern);
        //    else return false;
        //}

        //public static string CompressFolder()
        //{
        //    using (ZipFile zip = new ZipFile())
        //    {
        //        //zip.UseUnicodeAsNecessary = true;
        //        zip.AddDirectory(Results.ResultsDir);
        //        zip.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
        //        zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");
        //        string completePath = Path.Combine(Path.GetFullPath(Results.HTMLResultsLocation), Results.folderName + ".zip");
        //        zip.Save(string.Format(completePath));

        //        return completePath;
        //    }
        }


    }
    
}
