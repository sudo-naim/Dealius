using Dealius.Models;
using Dealius.Pages;
using OpenQA.Selenium;
using System;
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

            deal = new CalculatorDealInfo
            {
                Date = DateTime.Now,
                LeaseType = "Assignment",
                Months = new Random().Next(999),
                SpaceRequired = new Random().Next(99999)
            };

            leaseVariables = new LeaseVariables
            {
                RatePerSF = new Random().Next(999)
            };
        }

        [Given(@"a user adds a new lease deal")]
        public void GivenAUserAddsALeaseDeal()
        {
            dealiusPage.Login();
            dealiusPage.ClickDeals();
            dealsPage.ClickAddDeal();
            dealsPage.ClickTenantRep();
            dealsProfilePage.InputCompanyName("ANewCompany");
            dealsProfilePage.InputDealName("AutomationTest");
            dealsProfilePage.InputEstimatedCloseDate("03.20.2021");
            dealsProfilePage.ClickSave();
            dealsProfilePage.ClickExpandAll();
        }

        [Given(@"a user opens the Lease Rate Calculator for that deal")]
        public void GivenOpensTheLeaseRateCalculatorForThatDeal()
        {
            dealsProfilePage.ClickCalculate();
            dealsProfilePage.InputCalculationStartDate(deal.Date);
            dealsProfilePage.InputLeaseType("Direct-New");
            dealsProfilePage.InputTerm(10);
            dealsProfilePage.InputSpaceRequired(900);
            dealsProfilePage.ClickContinue();
        }
        [Given(@"generates schedule")]
        [When(@"generates schedule")]
        public void WhenGeneratesSchedule()
        {
            leaseRateCalculatorPage.ClickGenerateScheduleButton();
        }

        [When(@"the user enters (.*)\$ for the Rate per SF")]
        public void WhenTheUserEntersForTheRatePerSF(double RatePerSF)
        {
            leaseRateCalculatorPage.InputRatePerSF(RatePerSF);
        }

        [When(@"the user enters the Rate per SF")]
        public void WhenTheUserEntersTheRatePerSF()
        {
            leaseRateCalculatorPage.InputRatePerSF(leaseVariables.RatePerSF);
        }
        
        [Then(@"all columns of the first row are correct")]
        public void ThenTheFirstRowResultsAreCorrect()
        {
            int numberOfRows = (int)Math.Ceiling((double)DealsProfilePage.TermInMonths / 12);
            for (int rowIndex = 0; rowIndex <numberOfRows; rowIndex++)
            {
                leaseRateCalculatorPage.CheckRentMonths(rowIndex);
                leaseRateCalculatorPage.CheckMonthlyRatePerSf(rowIndex);
                leaseRateCalculatorPage.CheckFreeRentMonths(rowIndex);
                leaseRateCalculatorPage.CheckRentPerMonthAmount(rowIndex);
                leaseRateCalculatorPage.CheckTotalLeaseAmount(rowIndex);
                //leaseRateCalculatorPage.CheckDateRange(rowIndex);
            }
        }
        //===============================================================
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

        [When(@"the user clicks continue")]
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
            leaseRateCalculatorPage.CheckRateType(RateType);
        }

        [Given(@"Base Rate column header displays '(.*)'")]
        [Then(@"Base Rate column header displays '(.*)'")]
        public void ThenBaseRateColumnHeaderDisplays(string BaseRateTitle)
        {
            leaseRateCalculatorPage.CheckBaseRateTitle(BaseRateTitle);
        }

        [When(@"the user selects Rate Type option '(.*)'")]
        public void WhenTheUserSelectsRateTypeOption(string RateType)
        {
            leaseRateCalculatorPage.SelectRateType(RateType);
        }

    }
}