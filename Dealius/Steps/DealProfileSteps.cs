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
    class DealProfileSteps
    {
        DealiusPage dealiusPage;
        DealsPage dealsPage;
        DealsProfilePage dealsProfilePage;
        RentCalculationPage _rentCalculationPage;
        private CalculatorDealInfo Deal { get; set; }
        private LeaseVariables leaseVariables { get; set; } = new LeaseVariables();
        ScenarioContext sct { get; set; }
        FeatureContext fct { get; set; }

        public DealProfileSteps(IWebDriver driver, FeatureContext fct, ScenarioContext sct)
        {
            this.sct = sct;
            this.fct = fct;
            dealiusPage = new DealiusPage(driver);
            dealsPage = new DealsPage(driver);
            dealsProfilePage = new DealsProfilePage(driver);
            _rentCalculationPage = new RentCalculationPage(driver);
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
            dealsProfilePage.InputEstimatedPaymentDate(DateTime.Parse("02.20.2021"));
            dealsProfilePage.InputPaymentCommissionFee("100");
        }

    }
}
