using System;
using System.IO;
using System.Threading;
using DemoTests.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace DemoTests.Pages
{
    public class MainEmailUkrNetPage : BasePage
    {
        [FindsBy(How = How.XPath,Using = "/html/body/div[2]/div[4]/div[4]/div/div[2]/ul/li[3]")]
        private IWebElement EmailLabelElement { get; set;}

        [FindsBy(How = How.Id,Using = "search-field")]
        private IWebElement SearchTextBoxElement { get; set;}
        [FindsBy(How = How.XPath,Using = "/html/body/div[2]/div[4]/div[4]/div/div[2]/div[1]/form/span/input")]
        private IWebElement SearchButtonElement { get; set;}

        public string EmailLabelText
        {
            get { return EmailLabelElement.Text; }
        }

        [FindsBy(How = How.XPath, Using = "/html/body/div[2]/div[4]/div[4]/div/div[2]/div[3]/div[2]/div[2]/div/span")]
        private IWebElement FoundEmailCountSpanElement { get; set;}

        [FindsBy(How = How.LinkText, Using = "Написати листа")]
        private IWebElement NewEmailAElement { get; set;}

        public int FoundEmailCountSpan
        {
            get
            {
                if (FoundEmailCountSpanElement == null) return -1;
                string count = FoundEmailCountSpanElement.Text.Split(' ')[1];
                return Int32.Parse(count);
            }
        }
        public MainEmailUkrNetPage()
        {
            PageFactory.InitElements(Driver.DriverInstance, this);

        }   
        
        public MainEmailUkrNetPage SearchEmails(string searchSubject, TimeSpan waitTime)
        {

            SetTextBox(SearchTextBoxElement, searchSubject);
            Thread.Sleep(500);
            SetTextBox(SearchTextBoxElement, Keys.Enter);
            Thread.Sleep(waitTime);
            return this;
        }

        public NewEmailUkrNetPage ClickNewEmailLabel()
        {
            NewEmailAElement.Click();
            ExecuteJavaScript(@"window.location.href = '#sendmsg'");
            Thread.Sleep(1000);
            return new NewEmailUkrNetPage();
        }
    }
}