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
        private By DealNameInput = By.Name("DealName");
        private By ClientName = By.Id("ClientName");
        private By AddNew = By.XPath("//a[text()='Add new']");
        private By NameInput = By.Id("Name");
        private By EstimatedCloseDate = By.Name("EstimatedCloseDate");
        private By SaveButton = By.XPath("//ul/following-sibling::button[text() = 'SAVE']");
        private By SaveFormButton = By.XPath("//button/following-sibling::button[text()='SAVE']");
        private By CloseDealButton = By.XPath("//button[text()='CLOSE DEAL']");
        private By CalculateButton = By.XPath("//button[contains(text(),'Calculate')]");
        private By ExpandAllButton = By.CssSelector("a[class='expand-all-link']");
        private By StartDateInput = By.Id("PopupStartDate");
        private By LeasTypeSelect = By.CssSelector("button[data-id='PopupLeaseType']");
        private By SpaceRequiredInput = By.Name("RequestedSpace");
        private By TermInput = By.XPath("//input[@name='Term']");
        private By ContinueButton = By.XPath("//button[contains(text(),'CONTINUE')]");
        private By PropertyStreetAddres = By.Name("Property[StreetAddress]");
        private By PropertyName = By.Name("Property[PropertyName]");
        private By LandlordCompanyName = By.Id("OppositeSideName");
        private By AddHouseBrokerButton = By.XPath("//span[contains(text(),'Add House Broker')]");
        private By AddPaymentButton = By.XPath("//span[contains(text(),'Add Payment')]");
        private By BrokerName = By.Name("DealBrokers[0][BrokerName]");
        private By HouseReferralToggle = By.Name("IsHouseReferral");
        private By EstimatedPaymentDate(int index) => By.Name($"DealPayments[{index}][EstimatedPaymentDate]");
        private By DealPaymentCommissionFee(int index) => By.Name($"DealPayments[{index}][CommissionUi]");

        private By BrokerNameDropdownLi(string dataValue) => By.CssSelector("li[data-value='User Broker']");
        private By BrokerPercentage = By.Name("DealBrokers[0][Commission]");
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
            Input(ClientName, companyName);
            WaitElementToBeClickable(AddNew).Click();
            WaitElementToBeClickable(SaveFormButton).Click();
            WaitForElement(Find(By.CssSelector("a[title='Clear']")));
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
            var date = startDate.ToString("MM/dd/yyyy");
            Input(StartDateInput, date + Keys.Enter);
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

        public void InputPropertyName(string propertyName)
        {
            Input(PropertyName, propertyName);
        }
        public void InputPropertyStreetAddress(string streetAddress)
        {
            Input(PropertyStreetAddres, streetAddress);
        }

        public void InputLandlordCompanyName(string companyName)
        {
            Input(LandlordCompanyName, companyName);
            WaitElementToBeClickable(AddNew).Click();
            WaitElementToBeClickable(SaveFormButton).Click();
        }

        public void ClickAddHouseBrokerButton()
        {
            click(AddHouseBrokerButton);
        }

        public void SelectBroker(string brokerName)
        {
            Input(BrokerName,brokerName);
            click(BrokerNameDropdownLi(brokerName));
        }

        public void InputCommissionPercentage(string percentage)
        {
            Input(BrokerPercentage, percentage);
        }

        public void ClickAddPaymentButton()
        {
            click(AddPaymentButton);
        }

        public void InputEstimatedPaymentDate(DateTime paymentDate)
        {
            Input(EstimatedPaymentDate(0), paymentDate.ToString("MM/dd/yyyy"));
        }

        public void InputPaymentCommissionFee(string percentage)
        {
            Input(DealPaymentCommissionFee(0), percentage);
        }

        public void ClickSaveButton()
        {
            click(By.XPath("//button[contains(@class,'btn-success')][text()='SAVE']"));
            WaitElementDisplayed(By.XPath("//button[text()='SAVING...']"));
            WaitElementDisappears(By.XPath("//button[text()='SAVING...']"));
            WaitElementDisappears(By.XPath("//span[contains(text(),'Deal updated')]"));
        }

        public void ClickCloseDealButton()
        {
            click(CloseDealButton);
        }

        public void ClickHouseReferralToggle()
        {
            Find(HouseReferralToggle, By.XPath("./following-sibling::span")).Click();
        }
    }
}
