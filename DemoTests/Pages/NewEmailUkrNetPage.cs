using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DemoTests.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace DemoTests.Pages
{
    public class NewEmailUkrNetPage : BasePage
    {
        [FindsBy(How = How.Id,Using = "toField")]
        private IWebElement ToFieldInputElement { get; set;}
        [FindsBy(How = How.XPath,Using = "/html/body/div[2]/div[4]/div[4]/div/div[2]/div[3]/div[5]/div[4]/div/form[1]/label[5]/span[2]/input")]
        private IWebElement SubjectInputElement { get; set;}
        [FindsBy(How = How.XPath,Using = "/html/body/div[2]/div[4]/div[4]/div/div[2]/div[3]/div[5]/div[3]/span[1]/input")]
        private IWebElement SendButtonElement { get; set;}

        [FindsBy(How = How.XPath,Using = "/html/body/div[2]/div[4]/div[4]/div/div[2]/div[3]/div[5]/div[4]/div/form[2]/div[1]/div[2]")]
        private IWebElement MessageBodyInputElement { get; set;}

        [FindsBy(How = How.Id,Using = "text")]
        private IWebElement MessageBodyTextAreaElement{ get; set;}
        [FindsBy(How = How.Id,Using = "info-popup-text")]
        private IWebElement PopUpMessageDivElement { get; set;}
       
        public NewEmailUkrNetPage()
        {
            PageFactory.InitElements(Driver.DriverInstance, this);

        }

        public NewEmailUkrNetPage FillEmailFields(string toField, string subject, string messageBody)
        {
            SetTextBox(ToFieldInputElement, toField);
            SetTextBox(SubjectInputElement, subject);
            Thread.Sleep(500);
            SetTextBox(MessageBodyTextAreaElement, messageBody);           
            return this;
        }

        public EmailSentUkrNetPage SendButtonClick()
        {
            SendButtonElement.Click();
            Thread.Sleep(1000);
            return new EmailSentUkrNetPage();
        }

        public string PopUpMessageText()
        {
            List<IWebElement> listWebElements = Driver.DriverInstance.FindElements(By.Id("info-popup-text")).ToList();
            return listWebElements.Count > 0  ? listWebElements.FirstOrDefault().Text : string.Empty;
        }

      
        
        
    }


}