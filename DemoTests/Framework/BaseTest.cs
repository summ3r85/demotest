using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace DemoTests.Framework
{
    public abstract class BaseTest
    {
        private readonly string url = @"https://mail.ukr.net/classic/login";

        [OneTimeSetUp]
        public static void Init()
        {
            string browser = GetBrowserEnvironmentVariable();
            Browser browserEnum;
            switch (browser)
            {
                case "firefox":
                    browserEnum = Browser.firefox;
                    break;
                case "chrome":
                    browserEnum = Browser.chrome;
                    break;
                case "ie":
                    browserEnum = Browser.ie;
                    break;
                default:
                    browserEnum = Browser.chrome;
                    break;

            }

            Driver drv = new Driver(browserEnum);
            Driver.DriverInstance.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(10));
            Driver.DriverInstance.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
            Driver.DriverInstance.Manage().Window.Maximize();
        }

        private static string GetBrowserEnvironmentVariable()
        {
            return Environment.GetEnvironmentVariable("DEMOTEST_BROWSER");
             
        }

        [OneTimeTearDown]
        public static void Close()
        {
            
            Driver.Close();
        }

        [SetUp]
        public void Open()
        {
            Driver.DriverInstance.Navigate().GoToUrl(url);
        }

        [TearDown]
        public void CleanUp()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                string datetime = DateTime.Now.ToString("ddMMyyhhmmss");

                var outputFileName = "out/scr/" + datetime + '_' + TestContext.CurrentContext.Test.MethodName + ".jpg";
                using (var fs = new FileStream(outputFileName, FileMode.CreateNew, FileAccess.ReadWrite))
                {
                    var screenshot = ((ITakesScreenshot) Driver.DriverInstance).GetScreenshot();
                    fs.Write(screenshot.AsByteArray, 0, screenshot.AsByteArray.Length);
                }
                Console.WriteLine("##teamcity[testFailed name='{0}' message='<a href='file://C:/project/{1}' details='sreenshot'", TestContext.CurrentContext.Test.MethodName, outputFileName );

            }
            //LoginHelper.LogOutUkrNet();
            Driver.DriverInstance.Manage().Cookies.DeleteAllCookies();
        }
    }
}
