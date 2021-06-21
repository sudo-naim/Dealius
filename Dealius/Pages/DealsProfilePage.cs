using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;
using Dealius.Utils;
using System.Globalization;
using OpenQA.Selenium.Interactions;
namespace Dealius.Pages
{
    class DealsProfilePage : BasePage
    {
        #region Locators
        private By DealNameInput = By.Name("DealName");
        private By ClientName = By.Id("ClientName");
        private By OppositeSideName = By.Id("OppositeSideName");
        private By AddNew = By.XPath("//a[text()='Add new']");
        private By NameInput = By.Id("Name");
        private By EstimatedCloseDate = By.Name("EstimatedCloseDate");
        private By SaveButton = By.XPath("//form[@id='form-main']/descendant::button[@data-loading-text='CREATING DEAL...'][@type='submit']");
        private By SaveFormButton = By.XPath("//button/following-sibling::button[text()='SAVE']");
        private By CloseDealButton = By.XPath("//button[text()='CLOSE DEAL']");
        private By CalculateButton = By.XPath("//button[contains(text(),'Calculate')]");
        private By ExpandAllButton = By.CssSelector("a[class='expand-all-link']");
        private By StartDateInput = By.Id("PopupStartDate");
        private By PopUpCalcution = By.Id("popup-calculation");
        private By PopupLeaseTypeSelect = By.Id("PopupLeaseType");
        private By SpaceRequiredInput = By.Name("RequestedSpace");
        private By TermInput = By.XPath("//input[@name='Term']");
        private By ContinueButton = By.XPath("//button[contains(text(),'CONTINUE')]");
        private By PropertyStreetAddres = By.Name("Property[StreetAddress]");
        private By PropertyName = By.Name("Property[PropertyName]");
        private By LandlordCompanyName = By.Id("OppositeSideName");
        private By AddHouseBrokerButton = By.XPath("//span[contains(text(),'Add House Broker')]");
        private By AddOutsideBrokerButton = By.XPath("//span[contains(text(),'Add Outside Broker')]");
        private By PurchasePriceInput = By.CssSelector("input[name='PurchasePrice']");
        private By BuyerRepFeeInput = By.CssSelector("input[name='ClientRepFee']");
        private By AddPaymentButton = By.XPath("//span[contains(text(),'Add Payment')]");
        private By BrokerName = By.Name("DealBrokers[0][BrokerName]");
        private By SecondBrokerName = By.Name("DealBrokers[1][BrokerName]");
        private By HouseReferralToggle = By.Name("IsHouseReferral");
        private By CompanyName = By.Name("Company[Name]");
        private By PopUpYesButton = By.CssSelector("button.btn.btn-primary.js-yes");
        private By PopUpNoButton = By.CssSelector("button.btn.btn-default.js-no");
        private By AddNewContactSaveButton = By.XPath("//form[@id='form-contact-details']/descendant::button[contains(text(),'SAVE')]");
        private By NewCompanyName = By.Id("Name");
        private By EditButton = By.XPath("//div[@class='page-header']/descendant::button[contains(text(),'EDIT DEAL')]");
        private By SetSplitsButton = By.CssSelector("button[class='btn btn-primary btn-sm btn-add-split']");
        private By AddExpenseButton = By.XPath("//button/span[contains(text(),'Add Expense')]/..");
        private By GreenPaidLabel = By.XPath("//div[@id='popup-deal_split']/descendant::span[contains(text(),'Paid')]");
        private By ExpensesVendorName(int index) => By.Name($"DealExpenses[{index}][VendorName]");
        private By ExpensesAmount(int index) => By.Name($"DealExpenses[{index}][Amount]");
        private By ExpenseTypeSelect(int index) => By.Name($"DealExpenses[{index}][ExpenseTypeID]");

        private By EstimatedPaymentDate(int index) => By.Name($"DealPayments[{index}][EstimatedPaymentDate]");
        private By DealPaymentCommissionFee(int index) => By.Name($"DealPayments[{index}][CommissionUi]");

        private By BrokerNameDropdownLi(string brokerName) => By.CssSelector($"li[data-value='{brokerName}']");
        private By CompanyNameDropdownLi(string companyName) => By.CssSelector($"li[data-value='{companyName}']");
        private By BrokerPercentage = By.Name("DealBrokers[0][Commission]");
        private By SecondBrokerPercentage = By.Name("DealBrokers[1][Commission]");
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
            WaitElementClick(AddNew);
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
            ScrollToElement(CalculateButton);
            WaitElementClick(CalculateButton);
        }

        public void WaitHalfASecond()
        {
            System.Threading.Thread.Sleep(500);
        }
        public void InputCalculationStartDate(DateTime startDate)
        {
            var date = startDate.ToString("MM/dd/yyyy");
            Input(StartDateInput, date + Keys.Enter);
        }
        
        public void SelectLeaseType(string leaseType)
        {
            //WaitForElement(Find(PopUpCalcution, ContinueButton));
            //WaitForElement(Find(PopupLeaseTypeSelect).FindElement(By.XPath("./..")));
            var select = new SelectElement(WaitElementEnabled(PopupLeaseTypeSelect));
            select.SelectByText(leaseType);
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
            ScrollToElement(LandlordCompanyName);
            click(LandlordCompanyName);
            WaitElementClick(AddNew);
            Input(NewCompanyName, companyName);
            WaitElementToBeClickable(SaveFormButton).Click();
        }

        public void ClickAddHouseBrokerButton()
        {
            WaitForElement(AddOutsideBrokerButton);
            click(AddHouseBrokerButton);
        }
        
        public void ClickAddOutsideBrokerButton()
        {
            click(AddOutsideBrokerButton);
        }

        public void SelectBroker(string brokerName)
        {
            Input(BrokerName,brokerName);
            click(BrokerNameDropdownLi(brokerName));
        }
        
        public void InputSecondBrokerName(string brokerName)
        {
            Input(SecondBrokerName, brokerName);
        }

        public void SelectCompanyName(string companyName)
        {
            Input(CompanyName, companyName+Keys.Enter);
            //click(CompanyNameDropdownLi(companyName));
            //pre-requisite used (need to create a company of type other to be used on outside broker)
        }

        public void ClickAddNew()
        {
            click(AddNew);
        }

        public void ClickFormContactDetailsSaveButton()
        {
            click(AddNewContactSaveButton);
        }

        public void InputCommissionPercentage(double percentage)
        {
            Input(BrokerPercentage, percentage.ToString());
        }
        
        public void InputCommissionPercentageForSecondBroker(string percentage)
        {
            Input(SecondBrokerPercentage, percentage);
        }

        public void ClickAddPaymentButton()
        {
            click(AddPaymentButton);
        }

        public void InputEstimatedPaymentDate(DateTime paymentDate, int paymentNumber)
        {
            Input(EstimatedPaymentDate(paymentNumber-1), paymentDate.ToString("MM/dd/yyyy")+ Keys.Enter);
        }

        public void InputPaymentCommissionFee(int percentage, int paymentNumber)
        {
            Input(DealPaymentCommissionFee(paymentNumber-1), percentage.ToString());
        }

        public void ClickSaveButton()
        {
            click(By.XPath("//button[contains(@class,'btn-success')][text()='SAVE']"));
            WaitElementDisplayed(By.XPath("//span[contains(text(),'Deal updated')]"));
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

        public void ClickPopUpYesButton()
        {
            click(PopUpYesButton);
        }
        
        public void ClickPopUpNoButton()
        {
            click(PopUpNoButton);
        }

        public bool PopUpYesButtonIsDisplayed()
        {
            var el = wait.Until(drv => driver.FindElement(PopUpYesButton).Displayed);
            return el;
        }

        public void InputPurchasePrice(double price)
        {
            Input(PurchasePriceInput, price.ToString());
        }

        public void InputBuyerRepFee(double price)
        {
            Input(BuyerRepFeeInput, price.ToString());
        }

        public void ClickEditButton()
        {
            click(EditButton);
        }
        
        public void ClickSetSplitsButton()
        {
            click(SetSplitsButton);
        }

        public void ClickAddExpense()
        {
            click(AddExpenseButton);
        }

        public IWebElement PaidLabel()
        {
           WaitElementDisplayed(By.Id("tab0"));
           return Find(GreenPaidLabel);
        }

        public void SelectDealExpenseByText(string text, int rowNumber)
        {
            var select = new SelectElement(WaitElementEnabled(ExpenseTypeSelect(rowNumber - 1)));
            select.SelectByText(text);
        }

        public void InputExpenseVendorName(string name, int rowNumber)
        {
            Input(ExpensesVendorName(rowNumber-1), name);
        }
        
        public void InputExpenseAmount(double amount, int rowNumber)
        {
            Input(ExpensesAmount(rowNumber-1), amount.ToString());
        }
        public void InputSellerInformation(string companyName)
        {
            Input(OppositeSideName, companyName);
            WaitElementClick(AddNew);
            WaitElementToBeClickable(SaveFormButton).Click();
            //WaitForElement(Find(By.CssSelector("a[title='Clear']")));
        }
    }
}
