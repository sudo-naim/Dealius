using System;
using System.Configuration;
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

        public AccountingSteps(IWebDriver driver, FeatureContext fct, ScenarioContext sct)
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
        
        [Given(@"a house broker is added")]
        public void GivenAHouseBrokerIsAdded()
        {
            dealsProfilePage.ClickAddHouseBrokerButton();
            dealsProfilePage.SelectBroker(ConfigurationManager.AppSettings.Get("BrokerName"));  //pre-requisite used
            dealsProfilePage.InputCommissionPercentage("100");
        }
        
        [Given(@"payment is added")]
        public void GivenPaymentIsAdded()
        {
            dealsProfilePage.ClickAddPaymentButton();
            dealsProfilePage.InputEstimatedPaymentDate(DateTime.Today);
            dealsProfilePage.InputPaymentCommissionFee("100");
        }

        [Given(@"payment is added on a day before todays date")]
        public void GivenPaymentIsAddedOnThePastFromTodays()
        {
            dealsProfilePage.ClickAddPaymentButton();
            dealsProfilePage.InputEstimatedPaymentDate(DateTime.Today.AddDays(-1));
            dealsProfilePage.InputPaymentCommissionFee("100");
        }


        [Given(@"the deal is closed")]
        public void GivenTheDealIsClosed()
        {
            dealsProfilePage.ClickSaveButton();
            dealsProfilePage.ClickCloseDealButton();

            //if(dealsProfilePage.PopUpYesButtonIsDisplayed())
            //dealsProfilePage.ClickPopUpYesButton();

            closingDealPage.ClickSubmitButton();
            //waitforCloseRequestDeatilsPopUp Proccessingbutton to dissappear
            closingDealPage.ClickPopupSubmitButton();
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
            accountingPage.WaitForLoadingImageToDissapear();
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
            accountingPage.WaitForLoadingImageToDissapear();
        }

        [When(@"the user filters the closed Deal")]
        [Given(@"the closed Deal is filtered out")]
        public void WhenTheUserFiltersTheClosedDeal()
        {
            var DealId = sct.Get<int>("dealID").ToString();
            accountingPage.InputSearchTermReceivables(DealId);
            accountingPage.ClickFilterDateRangeAllReceivables();
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
        public void WhenAUserMarksReceivableAsWriteOff()
        {
            accountingPage.ClickViewReceiptButton();
            addReceiptPage.InputAmount(0);
            addReceiptPage.ClickSaveButton();
            addReceiptPage.ClickPopUpYesButton();
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
            Assert.True(String.IsNullOrEmpty(accountingPage.AmountReceviedReceivableText()));
        }

        [When(@"user clears open Balance")]
        public void WhenUserClearsOpenBalance()
        {
            accountingPage.ClickClearOpenBalance();
            accountingPage.ClickPopUpConfirm();
        }

        [When(@"user clears open Balance for payable")]
        public void WhenUserClearsOpenBalanceForOverpayed()
        {
            accountingPage.ClickClearOpenBalance();
            accountingPage.ClickYes();
        }

        [Given(@"receipt is added")]
        public void GivenReceiptIsAdded()
        {
            var DealId = sct.Get<int>("dealID").ToString();
            accountingPage.InputSearchTermReceivables(DealId);
            accountingPage.ClickFilterDateRangeAllReceivables();
            accountingPage.ClickAddReceiptButton();
            addReceiptPage.SelectPaymentMethod();
            addReceiptPage.InputReference();
            addReceiptPage.ClickSavePayButton();
        }

        [When(@"a user makes the payment")]
        public void WhenAUserMakesThePayment()
        {
            var DealId = sct.Get<int>("dealID").ToString();

            makePaymentPage.SelectPaymentMethod();
            makePaymentPage.InputReference();
            makePaymentPage.ClickSaveButton();
            accountingPage.InputSearchTermPayables(DealId);
            accountingPage.SelectAllRelevancePayables();
            accountingPage.ClickFilterDateRangeAllPayables();
        }

        [When(@"a user enters payment details")]
        public void WhenAUserEntersPaymentDetails()
        {
            var DealId = sct.Get<int>("dealID").ToString();

            makePaymentPage.SelectPaymentMethod();
            makePaymentPage.InputReference();
        }

        [Then(@"amount paid for Payee '(.*)' is (.*)\$")]
        public void ThenAmountPaidForBrokerIs(string brokerName, int amountPaid)
        {
            Assert.Equal(amountPaid, accountingPage.PayablesPayeeAmountPaid(brokerName));
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
            Assert.True(String.IsNullOrEmpty(accountingPage.AmountPaidPayablesText(payee)));
        }

        [Then(@"Payment Method for payee '(.*)' is cleared")]
        public void ThenPaymentMethodForPayeeIsCleared(string payee)
        {
            Assert.True(String.IsNullOrEmpty(accountingPage.PaymentMethodPayablesText(payee)));
        }

        [Then(@"Payment Reference for payee '(.*)' is cleared")]
        public void ThenPaymentReferenceForPayeeIsCleared(string payee)
        {
            Assert.True(String.IsNullOrEmpty(accountingPage.PaymentReferencePayablesText(payee)));
        }

        [Then(@"Payment Date for payee '(.*)' is cleared")]
        public void ThenPaymentDateForPayeeIsCleared(string payee)
        {
            Assert.True(String.IsNullOrEmpty(accountingPage.PaidDatePayablesText(payee)));
        }

        [When(@"refreshes the page")]
        public void WhenRefreshesThePage()
        {
            accountingPage.refreshPage();
        }

        [When(@"inputs amount (.*)\$ for first payment")]
        public void WhenUserInputsAmountForFirstPayment(double amount)
        {
            var DealId = sct.Get<int>("dealID").ToString();

            makePaymentPage.InputFirstPaymentAmount(amount);
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
