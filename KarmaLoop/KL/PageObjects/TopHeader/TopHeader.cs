using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;

namespace KarmaLoop.KL
{
    class TopHeader : Base
    {
        #region Public Methods

        public TopHeader(IWebDriver driver) : base(driver) {  }

        public Login ClickSignIn()
        {
            Results.WriteDescription("Click on SIGN IN link from Home Page.");
            driver._Click("class", "signin-account");
            Results.WriteStatus("Pass", "Action performed successfully.");

            return new Login(driver);
        }

        public MyAccount ClickMyAccount()
        {
            Results.WriteDescription("Click on MY ACCOUNT link from Home Page.");
            driver._Click("linktext", "MY ACCOUNT");
            Results.WriteStatus("Pass", "Action performed successfully.");

            return new MyAccount(driver);
        }

        public CreateAccount ClickSignInAndCreateAccount()
        {
            
            Results.WriteDescription("Click SIGN IN link and verify the login popup");
            driver._Click("class", "signin-account");
            Results.WriteStatus("Pass", "Login popup displayed succsessfully");

            Results.WriteDescription("Click Create Account link and verify the create account popup");
            driver._Click("xpath", "//*[@class='account-menu-create-account']/a");
            Results.WriteStatus("Pass", "Create Account popup displayed succsessfully");

            return new CreateAccount(driver);
        }

        public SubscribeElist ClickSubscribeToElist(string subscribeFrom = "top")
        {
            
            if (subscribeFrom.ToLower().Equals("top"))
            {
                Results.WriteDescription("Click on subscribe to Elist");
                driver._Click("id", "subscribe");

                System.Threading.Thread.Sleep(6000);

                driver.SwitchTo().Frame("fancybox-frame");

                Results.WriteStatus("Pass", "Subscribe To Elist button clicked successfully");
            }
            else
            { }

            return new SubscribeElist(driver);
        }

        public Products SearchProduct()
        {
            Results.WriteDescription("Input any keyword in Search Panel and click on GO");
            Common.searchedKeyword = "cool";
            driver._Type("id", "search-term", Common.searchedKeyword);
            driver._Click("id", "search-go");
            Results.WriteStatus("Pass", "Performed search using keyword - '" + Common.searchedKeyword + "'.");
            
            return new Products(driver);
        }

        public ViewBag ClickViewBag()
        {
            Results.WriteDescription("Click on 'View Bag' link.");
            driver._Click("id", "minicart-link",true);
            Results.WriteStatus("Pass", "'View Bag' link Clicked.");

            return new ViewBag(driver);
        }

        public Men NavBarForMen()
        {
            return new Men(driver);
        }

        public Women NavBarForWomen()
        {
            return new Women(driver);
        }

        #endregion
    }
}
