using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using Dealius.Utils;
using Xunit;

namespace Dealius.Pages
{
    class LeaseRateCalculatorPage
    {
        #region Locators
        By RatePerSFInput = By.Name("BaseRatePerSf");
        By GenerateScheduleButton = By.XPath("//a[text()='Generate Schedule']");
        By BaseRentMonths = By.Name("Rents[0][Months]");
        #endregion
        IWebDriver driver;
        WebDriverWait wait;
        public LeaseRateCalculatorPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

        public void InputRatePerSF(int RatePerSF)
        {
            Method.Input(wait, RatePerSFInput, RatePerSF + Keys.Enter);
        }

        public void ClickGenerateScheduleButton()
        {
            Method.WaitElementToBeClickable(wait, GenerateScheduleButton).Click();
        }

        public void CheckBaseRentMonthsResult(string BRMonths)
        {
            string s = getVal(driver.FindElement(BaseRentMonths));
            Assert.Equal(BRMonths, s);
        }

        public void Check




        public String getVal(IWebElement webElement)
        {
            IJavaScriptExecutor e = (IJavaScriptExecutor)driver;
            return (String)e.ExecuteScript(String.Format("return $(\"input[name = 'Rents[0][Months]']\").val();"));
            //return (String)e.ExecuteScript(String.Format("return $(\"input[name = 'Rents[0][Months]']\").val();", webElement.GetAttribute("name")));

        }
    }
}
