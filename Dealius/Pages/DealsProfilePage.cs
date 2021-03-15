using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;
using Dealius.Utils;
using System.Globalization;

namespace Dealius.Pages
{
    class DealsProfilePage
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
        IWebDriver driver;
        WebDriverWait wait;

        public static int SpaceInSF { get; private set; }
        public static DateTime StartDate { get; private set; }
        public static int TermInMonths { get; private set; }

        public DealsProfilePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public void InputDealName(string DealName)
        {
            Method.Input(wait, DealNameInput, DealName);
        }

        public void InputCompanyName(string CompanyName)
        {
            Method.WaitElementToBeClickable(wait, ClientName).Click();
            Method.WaitElementToBeClickable(wait, AddNew).Click();
            Method.Input(wait, NameInput, CompanyName);
            Method.WaitElementToBeClickable(wait, SaveFormButton).Click();
        }

        public void InputEstimatedCloseDate(string Date)
        {
            Method.Input(wait, EstimatedCloseDate, Date + Keys.Enter);
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

        public void InputCalculationStartDate(DateTime StartDate)
        {
            //StartDate = DateTime.ParseExact(Date, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            //Method.Input(wait, StartDateInput, StartDate.ToString("MM/dd/yyyy") + Keys.Enter);
            Method.Input(wait, StartDateInput, StartDate.ToString("MM/dd/yyyy") + Keys.Enter);
        }

        public void InputLeaseType(string LeaseType)
        {
            Method.WaitElementToBeClickable(wait, LeasTypeSelect).Click();
            Method.WaitElementToBeClickable(wait, By.XPath($"//button/following::span[contains(text(),'{LeaseType}')][2]")).Click();
        }
        
        public void InputTerm(int months)
        {
            TermInMonths = months;
            driver.FindElements(TermInput)[1].SendKeys(months.ToString()+Keys.Enter);
            //Method.Input(wait, TermInput, "12");
        }

        public void InputSpaceRequired(int SpaceinSF)
        {
            SpaceInSF = SpaceinSF;
            Method.Input(wait, SpaceRequiredInput, SpaceInSF.ToString());
        }

        public void ClickContinue()
        {
            Method.WaitElementToBeClickable(wait, ContinueButton).Click();
        }
    }
}
