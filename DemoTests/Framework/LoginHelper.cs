using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DemoTests.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace DemoTests.Framework
{
    public static class LoginHelper
    {
        public static MainEmailUkrNetPage LogInUkrNet(Credential credential)
        {
            MainLoginUkrNetPage loginPage = new MainLoginUkrNetPage();
            loginPage.LogIn(credential.username,
                Encoding.ASCII.GetString(Convert.FromBase64String(credential.password)));
            Thread.Sleep(1000);
            return new MainEmailUkrNetPage();
        }

        public static void LogOutUkrNet()
        {
            Driver.DriverInstance.FindElement(By.XPath(@"/html/body/div[2]/div[4]/div[4]/div/div[2]/ul/li[5]/a")).Click();
        }
    }
}
