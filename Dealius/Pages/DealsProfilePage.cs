using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;
using Dealius.Utils;
using System.Globalization;

namespace Dealius.Pages
{
    class DealsProfilePage : BasePage
    {
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

        public static int SpaceInSf { get; private set; }
        public static DateTime StartDate { get; private set; }
        public static int TermInMonths { get; private set; }

        public DealsProfilePage(IWebDriver driver) : base(driver) { }

        public void InputDealName(string dealName)
        {
            WaitDocumentReadyState();
            Input(DealNameInput, dealName);
        }

        public void InputCompanyName(string companyName)
        {
            WaitElementToBeClickable(ClientName).Click();
            WaitElementToBeClickable(AddNew).Click();
            Input(NameInput, companyName);
            WaitElementToBeClickable(SaveFormButton).Click();
        }

        public void InputEstimatedCloseDate(string date)
        {
            Input(EstimatedCloseDate, date + Keys.Enter);
        }

        public void ClickSave()
        {
            WaitElementToBeClickable(SaveButton).Click();
        }

        public void ClickExpandAll()
        {
            WaitElementToBeClickable(ExpandAllButton).Click();
        }
        public void ClickCalculate()
        {
            WaitElementToBeClickable(CalculateButton).Click();
        }

        public void InputCalculationStartDate(DateTime startDate)
        {
            Input(StartDateInput, startDate.ToString("MM/dd/yyyy") + Keys.Enter);
        }

        public void InputLeaseType(string leaseType)
        {
            WaitElementToBeClickable(LeasTypeSelect).Click();
            WaitElementToBeClickable(By.XPath($"//button/following::span[contains(text(),'{leaseType}')][2]")).Click();
        }
        
        public void InputTerm(int months)
        {
            TermInMonths = months;
            driver.FindElements(TermInput)[1].SendKeys(months.ToString()+Keys.Enter);
        }

        public void InputSpaceRequired(int spaceInSf)
        {
            Input(SpaceRequiredInput, spaceInSf.ToString());
        }

        public void ClickContinue()
        {
            WaitElementToBeClickable(ContinueButton).Click();
        }
    }
}
