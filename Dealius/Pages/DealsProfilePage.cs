using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;
using Dealius.Utils;
using System.Globalization;

namespace Dealius.Pages
{
    class DealsProfilePage
    {
        IWebDriver driver;
        WebDriverWait wait;
        #region Locators
        By DealNameInput = By.Name("DealName");
        By ClientName = By.Id("ClientName");
        By AddNew = By.XPath("//a[text()='Add new']");
        By NameInput = By.Id("Name");
        By EstimatedCloseDate = By.Name("EstimatedCloseDate");
        By SaveButton = By.XPath("//ul/following-sibling::button[text() = 'SAVE']");
        By SaveFormButton = By.XPath("//button/following-sibling::button[text()='SAVE']");
        By CalculateButton = By.XPath("//button[contains(text(),'Calculate')]");
        By ExpandAllButton = By.CssSelector("a[class='expand-all-link']");
        By StartDateInput = By.Id("PopupStartDate");
        By LeasTypeSelect = By.CssSelector("button[data-id='PopupLeaseType']");
        By TermInput = By.XPath("//input[@name='Term']");
        By SpaceRequiredInput = By.Name("RequestedSpace");
        By ContinueButton = By.XPath("//button[contains(text(),'CONTINUE')]");
        #endregion
        public DealsProfilePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

        public void InputDealName()
        {
            Method.Input(wait, DealNameInput, "AutomationTest");
        }

        public void InputCompanyName()
        {
            Method.WaitElementToBeClickable(wait, ClientName).Click();
            Method.WaitElementToBeClickable(wait, AddNew).Click();
            Method.Input(wait, NameInput, "ANewCompany");
            Method.WaitElementToBeClickable(wait, SaveFormButton).Click();
        }

        public void InputEstimatedCloseDate()
        {
            Method.Input(wait, EstimatedCloseDate, "03.20.2021" + Keys.Enter);
        }

        public void ClickSave()
        {
            Method.WaitElementToBeClickable(wait, SaveButton).Click();
        }

        public void ClickExpandAll()
        {
            Method.WaitElementToBeClickable(wait, ExpandAllButton).Click();
        }
        public void ClickCalculate()
        {
            Method.WaitElementToBeClickable(wait, CalculateButton).Click();
        }

        public void InputCalculationStartDate()
        {
            DateTime StartDate = DateTime.ParseExact("01.01.2021", "dd.MM.yyyy", CultureInfo.InvariantCulture);
            
            Method.Input(wait, StartDateInput, StartDate.ToString("MM/dd/yyyy") + Keys.Enter);
        }

        public void InputLeaseType()
        {
            Method.WaitElementToBeClickable(wait, LeasTypeSelect).Click();
            Method.WaitElementToBeClickable(wait, By.XPath("//button/following::span[contains(text(),'Assignment')][2]")).Click();
        }
        
        public void InputTerm()
        {
            driver.FindElements(By.XPath("//input[@name='Term']"))[1].SendKeys("12"+Keys.Enter);
            //Method.Input(wait, TermInput, "12");
        }

        public void InputSpaceRequired()
        {
            Method.Input(wait, SpaceRequiredInput, "500");
        }

        public void ClickContinue()
        {
            Method.WaitElementToBeClickable(wait, ContinueButton).Click();
        }
    }
}
