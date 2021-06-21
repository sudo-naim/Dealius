using System;
using System.Configuration;
using BoDi;
using Dealius.Models;
using Dealius.Pages;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace Dealius.Steps
{
    [Binding]
    public class AccountingSteps
    {
        DealsProfilePage dealsProfilePage;
        RentCalculationPage rentCalculationPage;
        ClosingDealPage closingDealPage;
        DealiusPage dealiusPage;
        AccountingPage accountingPage;
        AddReceiptPage addReceiptPage;
        MakePaymentPage makePaymentPage;
        ScenarioContext sct { get; set; }
        FeatureContext fct { get; set; }
        private IObjectContainer objectContainer;

        public AccountingSteps(IObjectContainer objectContainer, IWebDriver driver, FeatureContext fct, ScenarioContext sct)
        {
            this.sct = sct;
            this.fct = fct;
            dealsProfilePage = new DealsProfilePage(driver);
            rentCalculationPage = new RentCalculationPage(driver);
            closingDealPage = new ClosingDealPage(driver);
            dealiusPage = new DealiusPage(driver);
            accountingPage = new AccountingPage(driver);
            addReceiptPage = new AddReceiptPage(driver);
            makePaymentPage = new MakePaymentPage(driver);
            this.objectContainer = objectContainer;
            OutputLogger.Initialize(objectContainer);
        }

        [Given(@"property information are entered")]
        public void GivenPropertyIsEntered()
        {
            dealsProfilePage.InputPropertyName("AProperty");
            dealsProfilePage.InputPropertyStreetAddress("Random Address");
        }
        
        [Given(@"transaction information are entered")]
        public void GivenTransactionInformationAreEntered(Table table)
        {
            var Deal = table.CreateInstance<CalculatorDealInfo>();
            sct.Add("Deal", Deal);

            dealsProfilePage.ClickHouseReferralToggle();
            dealsProfilePage.ClickCalculate();
            dealsProfilePage.WaitHalfASecond();
            dealsProfilePage.InputSpaceRequired(Deal.SpaceRequired);
            dealsProfilePage.InputCalculationStartDate(Deal.StartDate);
            dealsProfilePage.InputTerm(Deal.Term);
            dealsProfilePage.SelectLeaseType(Deal.LeaseType);
            dealsProfilePage.ClickContinue();
            rentCalculationPage.InputRatePerSf(Deal.RatePerSf);
            rentCalculationPage.InputRentClientRepFee(Deal.TenantRepFee);

            rentCalculationPage.ClickGenerateScheduleButton();
            rentCalculationPage.ClickSaveButton();
            dealsProfilePage.ClickExpandAll();

            sct.Add("dealID", closingDealPage.DealID());
        }
        
        [Given(@"landlord company name is entered")]
        public void GivenLandlordCompanyNameIsEntered()
        {
            dealsProfilePage.InputLandlordCompanyName("RandomCompany");
        }
        
        [Given(@"a house broker is added with commission percentage (.*)%")]
        public void GivenAHouseBrokerIsAdded(double percentage)
        {
            dealsProfilePage.ClickAddHouseBrokerButton();
            dealsProfilePage.SelectBroker(ConfigurationManager.AppSettings.Get("BrokerName"));  //pre-requisite used
            dealsProfilePage.InputCommissionPercentage(percentage);
        }

        [Given(@"(.*)% of fee on (.*)nd payment is added")]
        [Given(@"(.*)% of fee on (.*)st payment is added")]
        [Given(@"(.*)% of fee on (.*)d payment is added")]
        [Given(@"(.*)% of fee on (.*)th payment is added")]
        public void GivenSecondPaymentIsAdded(int percentage, int paymentNumber)
        {
            dealsProfilePage.ClickAddPaymentButton();
            dealsProfilePage.InputEstimatedPaymentDate(DateTime.Today, paymentNumber);
            dealsProfilePage.InputPaymentCommissionFee(percentage, paymentNumber);
        }

        [Given(@"payment is added")]
        public void GivenPaymentIsAdded()
        {
            dealsProfilePage.ClickAddPaymentButton();
            dealsProfilePage.InputEstimatedPaymentDate(DateTime.Today, 1);
            dealsProfilePage.InputPaymentCommissionFee(100, 1);
        }

        [Given(@"expense is added")]
        public void GivenExpenseIsAdded(Table table)
        {
            dynamic expense = table.CreateDynamicInstance();

            dealsProfilePage.ClickAddExpense();
            dealsProfilePage.SelectDealExpenseByText(expense.ExpenseType, 1);
            dealsProfilePage.InputExpenseVendorName(expense.VendorName, 1);
            dealsProfilePage.InputExpenseAmount(expense.ExpenseAmount, 1);
        }


        [Given(@"payment is added on (.*) days before todays date")]
        public void GivenPaymentIsAddedOnThePastFromTodays(int days)
        {
            dealsProfilePage.ClickAddPaymentButton();
            dealsProfilePage.InputEstimatedPaymentDate(DateTime.Today.AddDays(-days), 1);
            dealsProfilePage.InputPaymentCommissionFee(100, 1);
        }


        [Given(@"the deal is closed")]
        public void GivenTheDealIsClosed()
        {
            dealsProfilePage.ClickSaveButton();
            dealsProfilePage.ClickCloseDealButton();

            closingDealPage.ClickSubmitButton();
            closingDealPage.ClickPopupSubmitButton();
            closingDealPage.ClickPopUpOKButton();

            closingDealPage.ClickApproveButton();
            closingDealPage.WaitForProcessingButtonToDissappear();

            closingDealPage.ClickPopUpOKButton();
        }

        [Given(@"Accounting page is opened")]
        public void GivenAccountingPageIsOpened()
        {
            dealiusPage.ClickAccounting();
       }

        [Given(@"Receivables tab is opened")]
        public void GivenReceivablesTabIsOpened()
        {
            accountingPage.ClickReceivablesTab();
        }

        [When(@"the user opens the Payables tab")]
        public void WhenTheUserOpensThePayablesTab()
        {
            accountingPage.ClickPayablesTab();
        }

        [Given(@"Invoices tab is opened")]
        public void GivenInvoicesTabIsOpened()
        {
            accountingPage.ClickInvoicesTab();
        }

        [When(@"user find the deal row")]
        public void WhenUserFindTheDealRow()
        {
            var DealId = sct.Get<int>("dealID").ToString();
            accountingPage.InputSearchTermInvoices(DealId);
            accountingPage.ClickFilterDateRangeAllInvoices();
            accountingPage.WaitForLoadingImage();
        }

        [Then(@"invoice status of that deal is (.*)")]
        public void ThenInvoiceStatusOfThatDealIsPending(string status)
        {
            Assert.Equal(status, accountingPage.InvoiceStatusText());
        }

        [When(@"emails the invoice")]
        public void WhenEmailsTheInvoice()
        {
            accountingPage.ClickEmailInvoice();
            accountingPage.InputPopUpToEmail();
            accountingPage.ClickPopUpSendButton();
            accountingPage.WaitForLoadingImage();
        }

        [When(@"the user filters the closed Deal")]
        [Given(@"the closed Deal is filtered out")]
        public void WhenTheUserFiltersTheClosedDeal()
        {
            var DealId = sct.Get<int>("dealID").ToString();
            accountingPage.InputSearchTermReceivables(DealId);
            accountingPage.ClickFilterDateRangeAllReceivables();
        }

        [When(@"searches deal ID on payables tab")]
        public void WhenSearchesByDealID()
        {
            var DealId = sct.Get<int>("dealID").ToString();
            accountingPage.InputSearchTermPayables(DealId);
            accountingPage.SelectAllRelevancePayables();
            accountingPage.ClickFilterDateRangeAllPayables();
        }

        [When(@"opens make payment page")]
        public void WhenOpensMakePaymentPage()
        {
            accountingPage.ClickTheFirstMakePaymentButton();
        }


        [When(@"user navigates to Payables Summary form")]
        public void WhenUserNavigatesToPayablesSummaryForm()
        {
            var DealId = sct.Get<int>("dealID").ToString();

            accountingPage.ClickPayablesTab();
            accountingPage.InputSearchTermPayables(DealId);
            accountingPage.ClickFilterDateRangeAllPayables();
            accountingPage.ClickTheFirstMakePaymentButton();
        }

        [Then(@"total house net is \$(.*)")]
        public void ThenTotalHouseNetIs(double houseNet)
        {
            Assert.Equal(houseNet, makePaymentPage.TotalHouseNetAmount());
        }

        [Then(@"Amount Due for Payee '(.*)' is (.*)\$")]
        public void ThenAmountDueForPayeeIs(string payee, double amountDue)
        {
            Assert.Equal(amountDue, accountingPage.AmountDuePayables(payee));
        }

        [Then(@"the Total Due is (.*)\$")]
        public void ThenTheTotalDueIs(double totalDue)
        {
            Assert.Equal(totalDue, accountingPage.TotalDueReceivable());
        }

        [Then(@"Amount Due is (.*)\$")]
        public void ThenAmountDueIs(double amountDue)
        {
            Assert.Equal(amountDue, accountingPage.AmountDueReceivable());
        }

        [Then(@"open Balance is (.*)\$")]
        public void ThenOpenBalanceIs(double openBalance)
        {
            Assert.Equal(openBalance, accountingPage.OpenBalanceAmountReceivable());
        }

        [Then(@"Open Balance is (.*)")]
        public void ThenOpenBalanceIs(string openBalance)
        {
            Assert.Equal(openBalance, accountingPage.OpenBalanceAmountReceivableNegative());
        }


        [When(@"adds receipt for payment")]
        [Given(@"receipt for payment is added")]
        public void WhenAddsReceiptForPayment(Table table)
        {
            dynamic receipt = table.CreateDynamicInstance();

            accountingPage.SelectAllRelevance();
            accountingPage.WaitForLoadingImage();
            accountingPage.ClickAddReceiptButton();
            addReceiptPage.SelectPaymentMethod();
            addReceiptPage.InputReference();
            addReceiptPage.InputAmount(receipt.Amount);
            addReceiptPage.ClickSaveButton();
            accountingPage.WaitForLoadingImage();
        }

        [When(@"adds receipt for over payment")]
        [Given(@"receipt for over payment is added")]
        public void WhenAddsReceiptForOverPayment(Table table)
        {
            dynamic receipt = table.CreateDynamicInstance();

            accountingPage.SelectAllRelevance();
            accountingPage.WaitForLoadingImage();
            accountingPage.ClickAddReceiptButton();
            addReceiptPage.SelectPaymentMethod();
            addReceiptPage.InputReference();
            addReceiptPage.InputAmount(receipt.Amount);
            addReceiptPage.ClickSaveButton();
            addReceiptPage.ClickPopUpYesButton();
        }

        [When(@"confirms the value greater than Amount Due")]
        public void WhenConfirmsTheValueGreaterThanAmountDue()
        {
            addReceiptPage.ClickPopUpYesButton();
        }


        [Given(@"a receivable is writen off")]
        [When(@"a user marks receivable as write off")]
        [When(@"a user marks payables as write off")]
        public void WhenAUserMarksReceivableAsWriteOff()
        {
            accountingPage.ClickViewReceiptButton();
            addReceiptPage.InputAmount(0);
            addReceiptPage.ClickSaveButton();
            addReceiptPage.ClickPopUpYesButton();
            // additional waits
            accountingPage.WaitForLoadingImage();
            addReceiptPage.WaitForPopUpWriteOffPayablesDisplay();
            //
            addReceiptPage.ClickSecondPopUpSaveButton();
        }


        [Then(@"Amount Received is (.*)\$")]
        public void ThenAmountReceivedIs(double amountReceived)
        {
            Assert.Equal(amountReceived, accountingPage.AmountReceviedReceivable());
        }

        [Then(@"View Receipt button is displayed")]
        public void ThenViewReceiptButtonIsDisplayed()
        {
            Assert.True(accountingPage.ViewReceiptButton().Displayed);
        }

        [Then(@"Print Receipt button is displayed")]
        public void ThenPrintReceiptButtonIsDisplayed()
        {
            Assert.True(accountingPage.PrintReceiptButton().Displayed);
        }

        [Then(@"Delete Receipt button is displayed")]
        public void ThenDeleteReceiptButtonIsDisplayed()
        {
            Assert.True(accountingPage.DeletePaymentButton().Displayed);
        }

        [Then(@"Clear Open Balance button is displayed")]
        public void ThenClearOpenBalanceButtonIsDisplayed()
        {
            Assert.True(accountingPage.OpenClearBalanceButton().Displayed);
        }


        [When(@"a user deletes payment")]
        public void WhenAUserDeletesPayment()
        {
            accountingPage.ClickDeletePaymentButton();
            accountingPage.ClickYes();
        }

        [Then(@"Deal is shown under Open Receivables list")]
        public void ThenDealIsShownUnderOpenReceivablesList()
        {
            accountingPage.SelectOpenReceivablesRelevance();
            Assert.Equal(sct.Get<int>("dealID"), accountingPage.DealId());
        }

        [Then(@"Amount Received is empty")]
        public void ThenAmountReceivedIsEmpty()
        {
            Assert.True(String.IsNullOrEmpty(accountingPage.AmountReceviedReceivableText()), "Amount Received is expected to be null or empty");
        }

        [When(@"user clears open Balance for receivable")]
        public void WhenUserClearsOpenBalance()
        {
            accountingPage.ClickFirstClearOpenBalanceButton();
            accountingPage.ClickPopUpConfirm();
        }

        [When(@"user clears open Balance payable for Payee '(.*)'")]
        public void WhenUserClearsOpenBalanceForOverpayed(string payee)
        {
            accountingPage.ClickClearOpenBalanceButtonForPayable(payee);
            accountingPage.ClickYes();
        }

        [Given(@"receipt is added")]
        public void GivenReceiptIsAdded()
        {
            var DealId = sct.Get<int>("dealID").ToString();
            accountingPage.InputSearchTermReceivables(DealId);
            accountingPage.ClickFilterDateRangeAllReceivables();
            accountingPage.WaitForLoadingImage();
            accountingPage.ClickAddReceiptButton();
            addReceiptPage.SelectPaymentMethod();
            addReceiptPage.InputReference();
            addReceiptPage.ClickSavePayButton();
        }

        [When(@"a user makes the payment")]
        public void WhenAUserMakesThePayment()
        {
            var DealId = sct.Get<int>("dealID").ToString();

            makePaymentPage.SelectPaymentMethodForAllPayees();
            makePaymentPage.InputReferenceForAllPayees();
            makePaymentPage.ClickSaveButton();
            accountingPage.InputSearchTermPayables(DealId);
            accountingPage.SelectAllRelevancePayables();
            accountingPage.ClickFilterDateRangeAllPayables();
        }

        [When(@"a user clicks make payment")]
        public void WhenAUserClicksMakePayment()
        {
            accountingPage.ClickTheFirstMakePaymentButton();
        }

        [When(@"user click payment for payee '(.*)'")]
        public void WhenUserClickPaymentForPayee(string payee)
        {
            accountingPage.ClickPayableMakePayment(payee);
        }


        [When(@"a user adds payment details for the (.*)st payable")]
        [When(@"a user adds payment details for the (.*)nd payable")]
        [When(@"a user adds payment details for the (.*)d payable")]
        [When(@"a user adds payment details for the (.*)th payable")]
        public void WhenAUserAddsPaymentDetailsForTheStPayable(int payableNumber, Table table)
        {
            dynamic payment = table.CreateDynamicInstance();

            makePaymentPage.SelectPayableMethodForPayee(payableNumber, payment.PaymentMethod);
            makePaymentPage.InputReferenceForPayee(payableNumber, payment.Reference);
            makePaymentPage.InputAmountForPayee(payableNumber, payment.Amount);

        }

        [When(@"user shifts open balance")]
        public void WhenUserShiftsOpenBalance()
        {
            makePaymentPage.ClickSaveButton();
            makePaymentPage.ClickYesButton();

        }

        [When(@"user does not shift open balance")]
        public void WhenUserDoesNotShiftOpenBalance()
        {
            makePaymentPage.ClickSaveButton();
            makePaymentPage.ClickNoButton();
        }

        [When(@"a user enters payment details")]
        public void WhenAUserEntersPaymentDetails()
        {
            var DealId = sct.Get<int>("dealID").ToString();

            makePaymentPage.SelectPaymentMethodForAllPayees();
            makePaymentPage.InputReferenceForAllPayees();
        }

        [Then(@"Amount Paid for Payee '(.*)' of (.*) is (.*)\$")]
        public void ThenAmountPaidForBrokerIs(string brokerName, string paymentNumber, double amountPaid)
        {
            Assert.Equal(amountPaid, accountingPage.AmountPaidForPayeeAndPaymentNumber(brokerName, paymentNumber));
        }

        [Then(@"Amount Paid for Payee '(.*)' is (.*)\$")]
        public void ThenAmountPaidForPayeeIs(string brokerName, double amountPaid)
        {
            Assert.Equal(amountPaid, accountingPage.PayablesPayeeAmountPaid(brokerName));
        }

        [Then(@"Open Expense for Payee '(.*)' of (.*) is (.*)\$")]
        public void ThenOpenExpenseForBrokerIs(string brokerName, string paymentNumber, double OpenBalance)
        {
            Assert.Equal(OpenBalance, accountingPage.OpenExpenseForPayeeAndPaymentNumber(brokerName, paymentNumber));
        }

        [When(@"delets payment of payee '(.*)'")]
        public void WhenDeletsPaymentOfPayee(string payee)
        {
            accountingPage.ClickDeletePaymentButton(payee);
            accountingPage.ClickYes();

        }

        [Then(@"Amount Paid for payee '(.*)' is cleared")]
        public void ThenAmountPaidIsCleared(string payee)
        {
            Assert.True(String.IsNullOrEmpty(accountingPage.AmountPaidPayablesText(payee)),"Amount Paid is expected to be null or empty");
        }

        [Then(@"Payment Method for payee '(.*)' is cleared")]
        public void ThenPaymentMethodForPayeeIsCleared(string payee)
        {
            Assert.True(String.IsNullOrEmpty(accountingPage.PaymentMethodPayablesText(payee)), "Payment Method is expected to be null or empty");
        }

        [Then(@"Payment Reference for payee '(.*)' is cleared")]
        public void ThenPaymentReferenceForPayeeIsCleared(string payee)
        {
            Assert.True(String.IsNullOrEmpty(accountingPage.PaymentReferencePayablesText(payee)), "Payment Reference is expected to be null or empty");
        }

        [Then(@"Payment Date for payee '(.*)' is cleared")]
        public void ThenPaymentDateForPayeeIsCleared(string payee)
        {
            Assert.True(String.IsNullOrEmpty(accountingPage.PaidDatePayablesText(payee)), "Payment Date is expected to be null or empty");
        }

        [When(@"refreshes the page")]
        public void WhenRefreshesThePage()
        {
            accountingPage.refreshPage();
        }

        [When(@"saves payment for first payable with amount (.*)\$")]
        public void WhenUserInputsAmountForFirstPayment(double amount)
        {
            var DealId = sct.Get<int>("dealID").ToString();

            makePaymentPage.InputFirstPaymentAmount(amount);
            makePaymentPage.ClickSaveButton();
            accountingPage.InputSearchTermPayables(DealId);
            accountingPage.SelectAllRelevancePayables();
            accountingPage.ClickFilterDateRangeAllPayables();
        }

        [When(@"inputs amount (.*)\$ for second payment")]
        public void WhenUserInputsAmountForSecondPayment(double amount)
        {
            var DealId = sct.Get<int>("dealID").ToString();

            makePaymentPage.InputSecondPaymentAmount(amount);
            makePaymentPage.ClickSaveButton();
            accountingPage.InputSearchTermPayables(DealId);
            accountingPage.SelectAllRelevancePayables();
            accountingPage.ClickFilterDateRangeAllPayables();
        }

        [Then(@"open expense for Payee '(.*)' is (.*)")]
        public void ThenOpenExpenseForPayeeIs(string payee, string openBalance)
        {
            Assert.Equal(openBalance, accountingPage.OpenBalanceAmountPayablesText(payee));
        }

    }
}
