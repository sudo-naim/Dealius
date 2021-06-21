using BoDi;
using Dealius.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Tracing;

namespace Dealius.Hooks
{
    [Binding]
    public class Hooks 
    {
        private IWebDriver driver;
        private IObjectContainer objcontainer;
        private DealiusPage dealiusPage;
        ScenarioContext sct { get; set; }
        FeatureContext fct { get; set; }

        public Hooks(IObjectContainer _objcontainer, FeatureContext fct, ScenarioContext sct)
        {
            this.sct = sct;
            this.fct = fct;
            objcontainer = _objcontainer;
            OutputLogger.Initialize(objcontainer);
        }
        
        [BeforeScenario(Order = 0)]
        public void DriverSetup()
        {
            var URL = new Uri(ConfigurationManager.AppSettings.Get("UITestDriverURL"));
            var email = ConfigurationManager.AppSettings.Get("UserEmailOfficeAdmin");
            var password = ConfigurationManager.AppSettings.Get("UserPasswordOfficeAdmin");

            var chromeOptions = new ChromeOptions();
            //chromeOptions.AddArguments("headless");
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

        [AfterScenario(Order = 0)]
        public void AfterWebTest()
        {
            OutputLogger.Initialize(objcontainer);
            if (sct.TestError != null)
            {
                TakeScreenshot(driver, fct, sct);
            }
        }

        //[AfterTestRun]
        public static void DisposeData()
        {
            var db = new DbManager();

            db.DeleteAllData();
        }

        public static void TakeScreenshot(IWebDriver driver, FeatureContext _fct, ScenarioContext _sct)
        {
            try
            {
                string fileNameBase = string.Format("error_{0}_{1}_{2}",
                                                    _fct.FeatureInfo.Title.ToIdentifier(),
                                                    _sct.ScenarioInfo.Title.ToIdentifier(),
                                                    DateTime.Now.ToString("yyyyMMdd_HHmmss"));

                var artifactDirectory = Path.Combine(Directory.GetCurrentDirectory(), "testresults");
                if (!Directory.Exists(artifactDirectory))
                    Directory.CreateDirectory(artifactDirectory);

                string pageSource = driver.PageSource;
                string sourceFilePath = Path.Combine(artifactDirectory, fileNameBase + "_source.html");
                File.WriteAllText(sourceFilePath, pageSource, Encoding.UTF8);
                OutputLogger.Log($"Page source: {new Uri(sourceFilePath)}");

                ITakesScreenshot takesScreenshot = driver as ITakesScreenshot;

                if (takesScreenshot != null)
                {
                    var screenshot = takesScreenshot.GetScreenshot();

                    string screenshotFilePath = Path.Combine(artifactDirectory, fileNameBase + "_screenshot.png");

                    screenshot.SaveAsFile(screenshotFilePath, ScreenshotImageFormat.Jpeg);

                    OutputLogger.Log($"Screenshot: {new Uri(screenshotFilePath)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while taking screenshot: {0}", ex);
            }
        }

    }
}
