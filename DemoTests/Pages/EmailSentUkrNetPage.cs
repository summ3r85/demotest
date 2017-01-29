using System.Collections.Generic;
using System.Linq;
using DemoTests.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace DemoTests.Pages
{
    public class EmailSentUkrNetPage : BasePage
    {
        public EmailSentUkrNetPage()
        {
            PageFactory.InitElements(Driver.DriverInstance, this);

        }
        public bool IsSuccessfullMessageVisible
        {
            get
            {
                string xPath = @"/html/body/div[2]/div[4]/div[4]/div/div[2]/div[3]/div[7]/div[2]/div[1]/strong";
                List<IWebElement> listWebElements = Driver.DriverInstance.FindElements(By.XPath(xPath)).ToList();
                return listWebElements.Count > 0 && listWebElements.FirstOrDefault().Text.Contains("Вашого листа відправлено");
            }
            
        }
        public string PopUpMessageText()
        {
            List<IWebElement> listWebElements = Driver.DriverInstance.FindElements(By.Id("info-popup-text")).ToList();
            return listWebElements.Count > 0  ? listWebElements.FirstOrDefault().Text : string.Empty;
        }

    }
}