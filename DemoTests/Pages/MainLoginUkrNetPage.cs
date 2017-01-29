using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DemoTests.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace DemoTests.Pages
{
    class MainLoginUkrNetPage : BasePage
    {

        [FindsBy(How = How.Id,Using = "login")]
        private IWebElement LoginTextBox { get; set;}
        [FindsBy(How = How.Id,Using = "password")]
        private IWebElement PasswordTextBox { get; set;}
        [FindsBy(How = How.XPath,Using = "/html/body/div/div/div/form/div[3]/button")]
        private IWebElement LoginButton { get; set;}

        public bool IsInvalidUserOrPwdMessageVisible
        {
            get { return IsInvalidUserPwdMessageVisible(); }
        }

        public void LogIn(string username, string password)
        {
            SetTextBox(LoginTextBox, username);
            SetTextBox(PasswordTextBox, password);
            LoginButton.Click();
            Thread.Sleep(2000);
        }
        public MainLoginUkrNetPage()
        {
            PageFactory.InitElements(Driver.DriverInstance, this);
        }

        private bool IsInvalidUserPwdMessageVisible()
        {
            List<IWebElement> list = Driver.DriverInstance.FindElements(By.XPath("/html/body/div/div/div/form/div[1]")).ToList();
            return (list.Count > 0) && (list[0].Text.Contains("Логін або пароль вказано невірно.") || list[0].Text.Contains("Login or password"));
        }

        
    }
}
