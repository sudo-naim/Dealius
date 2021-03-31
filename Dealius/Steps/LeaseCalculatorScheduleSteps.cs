using Dealius.Models;
using Dealius.Pages;
using OpenQA.Selenium;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace Dealius.Steps
{
    [Binding]
    public class LeaseCalculatorScheduleSteps
    {
        DealiusPage dealiusPage;
        DealsPage dealsPage;
        DealsProfilePage dealsProfilePage;
        LeaseRateCalculatorPage leaseRateCalculatorPage;
        CalculatorDealInfo deal;
        LeaseVariables leaseVariables;
        ScenarioContext sct { get; set; }
        FeatureContext fct { get; set; }
        public LeaseCalculatorScheduleSteps(IWebDriver driver, FeatureContext fct , ScenarioContext sct)
        {
            this.sct = sct;
            this.fct = fct;
            dealiusPage = new DealiusPage(driver);
            dealsPage = new DealsPage(driver);
            dealsProfilePage = new DealsProfilePage(driver);
            leaseRateCalculatorPage = new LeaseRateCalculatorPage(driver);

            //initialize an object that contains requisite input data for creating a deal
            deal = new CalculatorDealInfo
            {
                Date = DateTime.Now,
                LeaseType = "Assignment",
                Months = new Random().Next(999),
                SpaceRequired = new Random().Next(99999)
            };

            //initialize an object that contains requisite input data for the calculator feature (lease variables)
            leaseVariables = new LeaseVariables
            {
                RatePerSF = new Random().Next(999)
            };
        }

        [Given(@"a (.*) Rep Deal is created")]
        public void GivenAUserAddsALeaseDeal(string repType)
        {
            dealiusPage.Login();
            dealiusPage.ClickDeals();
            dealsPage.ClickAddDeal();
            dealsPage.ClickRepType(repType);
            dealsProfilePage.InputDealName("AutomationTest");
            dealsProfilePage.InputCompanyName("ANewCompany");
            dealsProfilePage.InputEstimatedCloseDate("03.20.2021");
            dealsProfilePage.ClickSave();
            dealsProfilePage.ClickExpandAll();
        }

        [Given(@"the Lease calculator is opened")]
        public void GivenOpensTheLeaseRateCalculatorForThatDeal()
        {
            dealsProfilePage.ClickCalculate();
            dealsProfilePage.InputCalculationStartDate(deal.Date);
            dealsProfilePage.InputLeaseType(deal.LeaseType);
            dealsProfilePage.InputTerm(deal.Months);
            dealsProfilePage.InputSpaceRequired(deal.SpaceRequired);
            dealsProfilePage.ClickContinue();
        }
        [Given(@"generates schedule")]
        [When(@"the user generates schedule")]
        public void WhenGeneratesSchedule()
        {
            leaseRateCalculatorPage.ClickGenerateScheduleButton();
        }

        [Given(@"Rate per SF is entered")]
        public void WhenTheUserEntersTheRatePerSF()
        {
            leaseRateCalculatorPage.InputRatePerSf(leaseVariables.RatePerSF);
        }
        
        [Then(@"all the row input fields are displayed correctly")]
        public void ThenAllTheRowInputFieldsAreDisplayedCorrectly()
        {
            leaseRateCalculatorPage.CheckInputFieldsForRow(
                deal.Date,
                deal.Months,
                deal.SpaceRequired,
                leaseVariables.RatePerSF
                );
        }

        [Then(@"all rows for the schedule generated are displayed")]
        public void ThenAllRowsForTheScheduleGeneratedAreDisplayed()
        {
            leaseRateCalculatorPage.CheckAllRowsAreDisplayed(deal.Months);
        }


        [When(@"the user clicks calculate on Transaction Information section")]
        [Given(@"the user clicks calculate on Transaction Information section")]
        public void GivenClicksCalculateOnTransactionInformationSection()
        {
            dealsProfilePage.ClickCalculate();
        }

        [When(@"enters lease deal information")]
        [Given(@"enters lease deal information")]
        public void GivenEntersLeaseDealInformation()
        {
            dealsProfilePage.InputCalculationStartDate(deal.Date);
            dealsProfilePage.InputLeaseType(deal.LeaseType);
            dealsProfilePage.InputTerm(deal.Months);
            dealsProfilePage.InputSpaceRequired(deal.SpaceRequired);
        }

        [When(@"clicks continue")]
        public void WhenTheUserClicksContinue()
        {
            dealsProfilePage.ClickContinue();
        }

        [Then(@"the lease rate calculator page is opened")]
        public void ThenTheLeaseRateCalculatorIsOpened()
        {
            leaseRateCalculatorPage.CheckLeaseCalculatorPageLanded();
        }

        [Then(@"all Deal Information is displayed correctly")]
        public void ThenAllDealInformationIsDisplayed()
        {
            leaseRateCalculatorPage.CheckLeaseType(deal.LeaseType);
            leaseRateCalculatorPage.CheckLeaseCommencement(deal.Date);
            leaseRateCalculatorPage.CheckLeaseExpiration(deal.Date, deal.Months);
            leaseRateCalculatorPage.CheckMonths(deal.Months);
            leaseRateCalculatorPage.CheckSquareFootage(deal.SpaceRequired);
        }

        [Given(@"Rate Type option '(.*)' is selected")]
        public void GivenRateTypeOptionIsSelected(string RateType)
        {
            leaseRateCalculatorPage.SelectRateType(RateType);
        }

        [Given(@"Base Rate column header displays '(.*)'")]
        [Then(@"Base Rate column header displays '(.*)'")]
        public void ThenBaseRateColumnHeaderDisplays(string HeaderUnderBaseRateTitle)
        {
            leaseRateCalculatorPage.CheckHeaderUnderBaseRateIsDisplayed(HeaderUnderBaseRateTitle);
        }

        [Then(@"Base Rate column header '(.*)' is not displayed")]
        public void ThenBaseRateColumnHeaderIsNotDisplayed(string HeaderUnderBaseRateTitle)
        {
            leaseRateCalculatorPage.CheckHeaderUnderBaseRateIsNotDisplayed(HeaderUnderBaseRateTitle);
        }

    }
}