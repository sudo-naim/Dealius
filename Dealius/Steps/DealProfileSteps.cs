using BoDi;
using Dealius.Models;
using Dealius.Pages;
using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace Dealius.Steps
{
    [Binding]
    public class DealProfileSteps
    {
        DealiusPage dealiusPage;
        DealsPage dealsPage;
        DealsProfilePage dealsProfilePage;
        RentCalculationPage _rentCalculationPage;
        AccountingPage accountingPage;
        private CalculatorDealInfo Deal { get; set; }
        private LeaseVariables leaseVariables { get; set; } = new LeaseVariables();
        ScenarioContext sct { get; set; }
        FeatureContext fct { get; set; }
        private IObjectContainer objectContainer;
        public DealProfileSteps(IObjectContainer objectContainer,IWebDriver driver, FeatureContext fct, ScenarioContext sct)
        {
            this.sct = sct;
            this.fct = fct;
            dealiusPage = new DealiusPage(driver);
            dealsPage = new DealsPage(driver);
            dealsProfilePage = new DealsProfilePage(driver);
            _rentCalculationPage = new RentCalculationPage(driver);
            accountingPage = new AccountingPage(driver);
            this.objectContainer = objectContainer;
            OutputLogger.Initialize(objectContainer);
        }

        [Given(@"financial details are added")]
        public void GivenFinancialDetailsAreAdded(Table table)
        {
            dynamic finance = table.CreateDynamicInstance();

            dealsProfilePage.InputPurchasePrice(finance.PurchasePrice);
            dealsProfilePage.InputBuyerRepFee(finance.BuyerRepFee);
        }

        [Given(@"seller commpany name is entered")]
        public void GivenSellerCommpanyNameIsEntered()
        {
            dealsProfilePage.InputSellerInformation("AnyCompany");
        }

        [Given(@"buyer rep deal payment is added")]
        public void GivenBuyerRepDealPaymentIsAdded()
        {
            dealsProfilePage.ClickAddPaymentButton();
            dealsProfilePage.InputEstimatedPaymentDate(DateTime.Parse("02.20.2021"), 1);
            dealsProfilePage.InputPaymentCommissionFee(100, 1);
        }

        [When(@"opens deals profile")]
        public void WhenOpensDealsProfile()
        {
            accountingPage.ClickDealIDCell();
        }

        [When(@"opens set split pop up")]
        public void WhenOpensSetSplitPopUp()
        {
            dealsProfilePage.ClickExpandAll();
            dealsProfilePage.ClickEditButton();
            dealsProfilePage.ClickSetSplitsButton();
        }

        [Then(@"gree Paid Label is displayed")]
        public void ThenGreePaidLabelIsDisplayed()
        {
            Assert.True(dealsProfilePage.PaidLabel().Displayed);
        }


    }
}
