using BoDi;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Dealius.Hooks
{
    [Binding]
    class Hooks
    {
        private IWebDriver driver;
        private IObjectContainer objcontainer;
        private DealiusPage dealiusPage;
        protected string URL = "https://dealius-dev-tests.azurewebsites.net/";
        public Hooks(IObjectContainer objcontainer)
        {
            this.objcontainer = objcontainer;
        }
        
        [BeforeScenario(Order = 0)]
        public void DriverSetup()
        {
            //var chromeOptions = new ChromeOptions();
            //driver = new ChromeDriver(chromeOptions);
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            objcontainer.RegisterInstanceAs(driver);
            driver.Navigate().GoToUrl(URL);
            dealiusPage = new DealiusPage(driver);
        }

        
        [AfterScenario]
        public void DisposeDriverAfterScenario()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
