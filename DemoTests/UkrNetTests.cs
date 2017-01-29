using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DemoTests.Framework;
using DemoTests.Pages;
using NUnit.Framework;
using OpenQA.Selenium;

namespace DemoTests
{
    [Category("login")]
    [TestFixture]
    public class UkrNetLoginTests : BaseTest
    {
        [Test]
        public void LoginToUkrNetInvalidPassword_returnsTrue_Test()
        {
            MainLoginUkrNetPage page = new MainLoginUkrNetPage();
            page.LogIn("summ3r85", "");
            Assert.IsTrue(page.IsInvalidUserOrPwdMessageVisible);
            
        }
        [TestCaseSource(typeof(CredentialsData), "testData")]
        public void LoginToUkrNetValidPassword_AreEqueal_Test(Credential credential)
        {
            MainEmailUkrNetPage page = LoginHelper.LogInUkrNet(credential);
            Assert.AreEqual(page.EmailLabelText, string.Concat(credential.username,"@ukr.net"));
        }
    }

    [Category("search")]
    [TestFixture]
    public class UkrNetEmailSearchTests : BaseTest
    {
        [TestCase("datalife", 2)]
        [TestCase("Хостинг",6)]
        public void FindEmailBySubject_AreEqual_Test(string searchSubject, int resultCount)
        {
            MainEmailUkrNetPage page = LoginHelper.LogInUkrNet(CredentialsData.ValidCredentials).SearchEmails(searchSubject, TimeSpan.FromSeconds(2));
            Assert.AreEqual(resultCount, page.FoundEmailCountSpan);
            LoginHelper.LogOutUkrNet();
        }
    }

    [Category("newemail")]
    [TestFixture]
    public class UkrNetEmailNewEmailTests : BaseTest
    {
        [Test]
        public void NewMessageWindowToFieldEmpty_returnTrue_Test()
        {
            EmailSentUkrNetPage page =
                LoginHelper.LogInUkrNet(CredentialsData.ValidCredentials)
                    .ClickNewEmailLabel()
                    .FullEmailFields("", "test subject", "message11")
                    .SendButtonClick();
            Thread.Sleep(1000);
            Assert.AreEqual("Невірно заповнене поле Кому", page.PopUpMessageText());
            page.ExecuteJavaScript(@"iface.info_popup.hide();");
            LoginHelper.LogOutUkrNet();
        }

        [Test]
        public void SendMessageToMySelfWait10sec_returnTrue_Test()
        {
            Random rnd = new Random();
            UnicodeEncoding unc = new UnicodeEncoding();
            string subject = Convert.ToBase64String(unc.GetBytes(rnd.Next(1000000, 2000000).ToString()));

            EmailSentUkrNetPage page =
                LoginHelper.LogInUkrNet(CredentialsData.ValidCredentials)
                    .ClickNewEmailLabel()
                    .FullEmailFields("summ3r@ukr.net", subject, "message")
                    .SendButtonClick();
            Thread.Sleep(5000);
            Assert.True(page.Title.Contains("Відправлено"), page.Title);
            int count = page.NavigateInbox().SearchEmails(subject, TimeSpan.FromSeconds(5)).FoundEmailCountSpan;
            Assert.AreEqual(2, count, "page.FoundEmailCountSpan");
            LoginHelper.LogOutUkrNet();
        }
    }
}

