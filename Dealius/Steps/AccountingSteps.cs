using System;
using Dealius.Models;
using Dealius.Pages;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

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
            dealsProfilePage.InputLeaseType(Deal.LeaseType);
            dealsProfilePage.InputCalculationStartDate(Deal.StartDate);
            dealsProfilePage.InputTerm(Deal.Term);
            dealsProfilePage.InputSpaceRequired(Deal.SpaceRequired);
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
            dealsProfilePage.SelectBroker("User Broker");  //pre-requisite used
            dealsProfilePage.InputCommissionPercentage("100");
        }
        
        [Given(@"payment is added")]
        public void GivenPaymentIsAdded()
        {
            var Deal = sct.Get<CalculatorDealInfo>("Deal");
            
            dealsProfilePage.ClickAddPaymentButton();
            dealsProfilePage.InputEstimatedPaymentDate(Deal.StartDate.AddMonths(18));
            dealsProfilePage.InputPaymentCommissionFee("100");
        }
        
        [Given(@"the deal is closed")]
        public void GivenTheDealIsClosed()
        {
            dealsProfilePage.ClickSaveButton();
            dealsProfilePage.ClickCloseDealButton();
            closingDealPage.ClickSubmitButton();
            closingDealPage.ClickPopupSubmitButton();
            closingDealPage.ClickApproveButton();
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
            var DealId = sct.Get<int>("dealID").ToString();
            accountingPage.ClickReceivablesTab();
            accountingPage.InputSearchTerm(DealId);
            accountingPage.ClickFilterDateRangeAll();
        }


    }
}
