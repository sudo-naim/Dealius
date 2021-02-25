using Dealius.Pages;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Dealius.Steps
{
    [Binding]
    public class DealsSteps
    {
        DealiusPage dealiusPage;
        DealsPage dealsPage;
        DealsProfilePage dealsProfilePage;
        LeaseRateCalculatorPage leaseRateCalculatorPage;
        public DealsSteps(IWebDriver driver)
        {
            dealiusPage = new DealiusPage(driver);
            dealsPage = new DealsPage(driver);
            dealsProfilePage = new DealsProfilePage(driver);
            leaseRateCalculatorPage = new LeaseRateCalculatorPage(driver);
        }

        [Given(@"a user adds a new lease deal")]
        public void GivenAUserAddsALeaseDeal()
        {
            dealiusPage.Login();
            dealiusPage.ClickDeals();
            dealsPage.ClickAddDeal();
            dealsPage.ClickTenantRep();
            dealsProfilePage.InputDealName();
            dealsProfilePage.InputCompanyName();
            dealsProfilePage.InputEstimatedCloseDate();
            dealsProfilePage.ClickSave();
            dealsProfilePage.ClickExpandAll();
        }

        [Given(@"a user opens the Lease Rate Calculator for that deal")]
        public void GivenOpensTheLeaseRateCalculatorForThatDeal()
        {
            dealsProfilePage.ClickCalculate();
            dealsProfilePage.InputCalculationStartDate();
            dealsProfilePage.InputLeaseType();
            dealsProfilePage.InputTerm();
            dealsProfilePage.InputSpaceRequired();
            dealsProfilePage.ClickContinue();
        }

        [When(@"generates schedule")]
        public void WhenGeneratesSchedule()
        {
            leaseRateCalculatorPage.ClickGenerateScheduleButton();
        }


        [When(@"the user enters (.*)\$ for the Rate per SF")]
        public void WhenTheUserEntersForTheRatePerSF(int RatePerSF)
        {
            leaseRateCalculatorPage.InputRatePerSF(RatePerSF);
        }

        [Then(@"the the first row results are (.*)")]
        public void ThenTheTheFirstRowResultsAre(string BaseRentMonths)
        {
            leaseRateCalculatorPage.CheckBaseRentMonthsResult(BaseRentMonths);
        }


    }
}
