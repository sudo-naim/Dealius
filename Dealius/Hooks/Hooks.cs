using BoDi;
using Dealius.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Configuration;
using System.Net;
using TechTalk.SpecFlow;

namespace Dealius.Hooks
{
    [Binding]
    class Hooks 
    {
        private IWebDriver driver;
        private IObjectContainer objcontainer;
        private DealiusPage dealiusPage;
        
        public Hooks(IObjectContainer objcontainer)
        {
            this.objcontainer = objcontainer;
        }
        
        [BeforeScenario(Order = 0)]
        public void DriverSetup()
        {
            var URL = new Uri(ConfigurationManager.AppSettings.Get("UITestDriverURL"));
            var email = ConfigurationManager.AppSettings.Get("UserEmailOfficeAdmin");
            var password = ConfigurationManager.AppSettings.Get("UserPasswordOfficeAdmin");
            var chromeOptions = new ChromeOptions();

            chromeOptions.AddArguments("headless");
            chromeOptions.AddArguments("--disable-gpu");
            chromeOptions.AddArguments("--window-size=1920,1080");
            chromeOptions.AddArguments("start-maximized");
            driver = new ChromeDriver(chromeOptions);
            objcontainer.RegisterInstanceAs(driver);
            driver.Navigate().GoToUrl(URL);
            dealiusPage = new DealiusPage(driver);
            dealiusPage.Login(email, password);
        }

        [AfterScenario(Order = 1)]
        public void DisposeDriverAfterScenario()
        {
            driver.Quit();
        }

        [AfterScenario(Order = 2)]
        public void DisposeData()
        {
            var db = new DbManager();

            db.DeleteAllData();
        }
    }
}
