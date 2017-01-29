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
    public abstract class BasePage
    {
        public string Title
        {
            get { return Driver.DriverInstance.Title; }
        }

        public void ExecuteJavaScript(string jsCode)
        {
            if (Driver.DriverInstance is IJavaScriptExecutor)
            {
                var js = Driver.DriverInstance as IJavaScriptExecutor;
                js.ExecuteScript(jsCode);
            }
        }

        protected void SetTextBox(IWebElement webElement, string text)
        {
            webElement.SendKeys(text);
        }

        public MainEmailUkrNetPage NavigateInbox()
        {
            ExecuteJavaScript(@"window.location.href = '#msglist,folder=0&page=1'");
            Thread.Sleep(1000);
            return new MainEmailUkrNetPage();
        }
    }

    public interface IBasePage
    {
    }
}
