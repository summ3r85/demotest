using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace DemoTests.Framework
{
    public enum Browser
    {
        firefox = 1,
        chrome = 2,
        ie = 3,
        safari = 4
    }

    public class Driver
    {
        private static IWebDriver m_DriverInstance;

        public Driver(Browser browser)
        {
            switch (browser)
            {
                case Browser.firefox:
                    m_DriverInstance = FirefoxDriverInstance;
                    break;
                case Browser.chrome:
                    m_DriverInstance = ChromeDriverInstance;
                    break;
                case Browser.ie:
                    m_DriverInstance = IEDriverInstance;
                    break;
                default:
                    m_DriverInstance = FirefoxDriverInstance;
                    break;
            }
        }

        private static IWebDriver ChromeDriverInstance
        {
            get
            {
                if (m_DriverInstance == null)
                {
                    return new ChromeDriver(
                        @"C:\project\drivers\");
                }
                return null;
            }
        }

        public static IWebDriver DriverInstance
        {
            get { return m_DriverInstance; }
        }

        private static IWebDriver FirefoxDriverInstance
        {
            get
            {
                if (m_DriverInstance == null)
                {
                    m_DriverInstance =
                        new FirefoxDriver(FirefoxDriverService.CreateDefaultService(@"C:\project\drivers\"), new FirefoxOptions(), TimeSpan.FromSeconds(120));
                    //new FirefoxDriver());
                }
                return m_DriverInstance;
            }
        }

        private static IWebDriver IEDriverInstance
        {
            get
            {
                if (m_DriverInstance == null)
                {
                    var options = new InternetExplorerOptions();
                    options.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    //Clean the session before launching the browser
                    options.EnsureCleanSession = true;
                    m_DriverInstance =
                        new InternetExplorerDriver(
                            InternetExplorerDriverService.CreateDefaultService(
                                @"C:\project\drivers\"), options);
                }
                return m_DriverInstance;
            }
        }

        public static void Close()
        {
            if (m_DriverInstance != null)
            {
                m_DriverInstance.Quit();
                m_DriverInstance = null;
            }
        }
    }
}
