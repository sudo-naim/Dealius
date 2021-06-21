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
    public class LeaseCalculatorScheduleSteps
    {
        DealiusPage dealiusPage;
        DealsPage dealsPage;
        DealsProfilePage dealsProfilePage;
        RentCalculationPage _rentCalculationPage;
        private CalculatorDealInfo Deal { get; set; }
        private LeaseVariables leaseVariables { get; set; } = new LeaseVariables();
        ScenarioContext sct { get; set; }
        FeatureContext fct { get; set; }
        // ---- TableCells
        private double MonthsCell { get; set; }
        // ---- Table Cells
        private IObjectContainer objectContainer;
        public LeaseCalculatorScheduleSteps(IObjectContainer objectContainer,IWebDriver driver, FeatureContext fct , ScenarioContext sct)
        {
            this.sct = sct;
            this.fct = fct;
            dealiusPage = new DealiusPage(driver);
            dealsPage = new DealsPage(driver);
            dealsProfilePage = new DealsProfilePage(driver);
            _rentCalculationPage = new RentCalculationPage(driver);
            this.objectContainer = objectContainer;
            OutputLogger.Initialize(objectContainer);
        }

        [Given(@"deal transaction information is entered")]
        [When(@"deal transaction information is entered")]
        public void GivenDealInformationIsEntered(Table table)
        {
            Deal = table.CreateInstance<CalculatorDealInfo>();
            sct.Add("Deal", Deal);

            dealsProfilePage.ClickCalculate();
            dealsProfilePage.WaitHalfASecond();
            dealsProfilePage.InputCalculationStartDate(Deal.StartDate);
            dealsProfilePage.SelectLeaseType(Deal.LeaseType);
            dealsProfilePage.InputTerm(Deal.Term);
            dealsProfilePage.InputSpaceRequired(Deal.SpaceRequired);
            dealsProfilePage.ClickContinue();
        }

        [Given(@"deal info (.*) (.*) (.*) (.*) is entered")]
        public void GivenDealInformationIsEntered(string startDate, string leaseType, int term, int spaceRequired)
        {
            
            dealsProfilePage.ClickCalculate();
            dealsProfilePage.WaitHalfASecond();
            dealsProfilePage.SelectLeaseType(leaseType);
            dealsProfilePage.InputCalculationStartDate(DateTime.Parse(startDate));
            dealsProfilePage.InputTerm(term);
            dealsProfilePage.InputSpaceRequired(spaceRequired);
            dealsProfilePage.ClickContinue();
        }
        
        [When(@"the user enters Rate: (.*) and presses tab")]
        public void WhenTheUserEntersRate(int ratePerSf)
        {
            _rentCalculationPage.InputRate(ratePerSf);
        }

        [Then(@"Rate per Sf shows (.*)")]
        public void ThenTheIs(int ratePerSf)
        {
            Assert.Equal(ratePerSf, _rentCalculationPage.RatePerSf());
        }

        [Given(@"user enters (.*) for Rates Per Sf")]
        [When(@"the user enters Rates Per Sf: (.*) and presses tab")]
        public void WhenTheUserEnters(int ratePerSf)
        {
            _rentCalculationPage.InputRatePerSf(ratePerSf);
        }

        [Then(@"Rate shows (.*)")]
        public void ThenTheIsCalculatedAutomatically(int Rate)
        {
            Assert.Equal(Rate,_rentCalculationPage.Rate());
        }


        [Given(@"a (.*) Rep Deal is created")]
        public void GivenAUserAddsALeaseDeal(string repType, Table table)
        {
            dynamic Deal = table.CreateDynamicInstance();

            //dealiusPage.Login();
            dealiusPage.ClickDeals();
            dealsPage.ClickAddDeal();
            dealsPage.ClickRepType(repType);
            dealsProfilePage.InputCompanyName(Deal.CompanyName);
            dealsProfilePage.InputDealName(Deal.DealName);
            dealsProfilePage.InputEstimatedCloseDate(Deal.EstCloseDate.ToString("MM/dd/yyyy"));
            dealsProfilePage.ClickSave();
            dealsProfilePage.ClickExpandAll();
        }

        [Given(@"generates schedule")]
        [When(@"the user generates schedule")]
        public void WhenGeneratesSchedule()
        {
            _rentCalculationPage.ClickGenerateScheduleButton();
        }


        [Given(@"Rate per SF is entered")]
        public void WhenRatePerdFIsEntered()
        {
            leaseVariables.RatePerSf = new Random().Next(999);
            _rentCalculationPage.InputRatePerSf(leaseVariables.RatePerSf);
        }

        [Given(@"Rate per SF of (.*)\$ is entered")]
        public void WhenRatePerSFIsEntered(double RatePerSf)
        {
            leaseVariables.RatePerSf = RatePerSf;
            _rentCalculationPage.InputRatePerSf(RatePerSf);
        }

        [Then(@"all the rows data are displayed correctly")]
        public void ThenAllTheRowInputFieldsAreDisplayedCorrectly()
        {
            var rentsGridBodyRows = _rentCalculationPage.GetRentsGridTableRows();
            var firstRow = rentsGridBodyRows[0]; 
            //we will first assert the first row since it can differ from the number of months
            //below you can see that the expected results of the table cells are calculated
            var MonthsCell = 12 - (rentsGridBodyRows.Count * 12 - Deal.Term);
            var MonthlyRateCell = leaseVariables.RatePerSf * Deal.SpaceRequired;

            int AnnualRateCell = (int)((leaseVariables.RatePerSf * Deal.SpaceRequired/12) * MonthsCell);
            double TotalLeaseRowCell;

            //if the rate type is Per Month then we assert based on per month calculations and cells (first row assertions)
            if (leaseVariables.RateType.ToLower().Equals("per month"))
            {
                TotalLeaseRowCell = (leaseVariables.RatePerSf * Deal.SpaceRequired) * MonthsCell;

                Assert.Equal(leaseVariables.RatePerSf, _rentCalculationPage.tdMonthlyRatePerSf(firstRow));
                Assert.Equal(MonthlyRateCell, _rentCalculationPage.RentPerMonthAmount(firstRow));
                Assert.Equal(TotalLeaseRowCell, _rentCalculationPage.TotalLeaseAmount(firstRow));
            }
            //else if the rate type is Per Year then we assert based on per year calculations and cells (first row assertions)
            else
            {
                Assert.Equal(leaseVariables.RatePerSf, _rentCalculationPage.tdRatePerSf(firstRow));
                Assert.Equal(AnnualRateCell, (int)_rentCalculationPage.TdBaseRentAmountInput(firstRow));
                Assert.Equal(AnnualRateCell, (int)_rentCalculationPage.TotalLeaseAmount(firstRow));
            }
            Assert.Equal(MonthsCell, _rentCalculationPage.RentMonths(firstRow));
            Assert.Equal(0, _rentCalculationPage.FreeRentMonths(firstRow));
            
            // here we are asserting the other rows 
            foreach (var row in rentsGridBodyRows.Skip(1))
            {
                if (leaseVariables.RateType.ToLower().Equals("per month"))
                {
                    TotalLeaseRowCell = (leaseVariables.RatePerSf * Deal.SpaceRequired) * 12;

                    Assert.Equal(leaseVariables.RatePerSf, _rentCalculationPage.tdMonthlyRatePerSf(row));
                    Assert.Equal(MonthlyRateCell, _rentCalculationPage.RentPerMonthAmount(row));
                    Assert.Equal(TotalLeaseRowCell, _rentCalculationPage.TotalLeaseAmount(row));
                }
                else
                {
                    AnnualRateCell = (int)(leaseVariables.RatePerSf * Deal.SpaceRequired);
                    TotalLeaseRowCell = AnnualRateCell;
                    Assert.Equal(leaseVariables.RatePerSf, _rentCalculationPage.tdRatePerSf(row));
                    Assert.Equal(AnnualRateCell, (int)_rentCalculationPage.TdBaseRentAmountInput(row));
                    Assert.Equal(TotalLeaseRowCell, (int)_rentCalculationPage.TotalLeaseAmount(row));
                }
                Assert.Equal(12, _rentCalculationPage.RentMonths(row));
                Assert.Equal(0, _rentCalculationPage.FreeRentMonths(row));
            }
        }
        
        [Then(@"footer Total Lease Amount cell is \$(.*)")]
        public void ThenTotalLeaseFooterCellDisplaysCorrectValue(double totalLeaseAmount)
        {
            Assert.Equal(totalLeaseAmount, _rentCalculationPage.FooterTotalLeaseAmount());
        }

        [Then(@"all rows for the schedule generated are displayed")]
        public void ThenAllRowsForTheScheduleGeneratedAreDisplayed()
        {
            Deal = sct.Get<CalculatorDealInfo>("Deal");
            _rentCalculationPage.CheckAllRowsAreDisplayed(Deal.Term);
        }

        [When(@"enters lease deal information")]
        [Given(@"enters lease deal information")]
        public void GivenEntersLeaseDealInformation()
        {
            dealsProfilePage.InputCalculationStartDate(Deal.StartDate);
            dealsProfilePage.SelectLeaseType(Deal.LeaseType);
            dealsProfilePage.InputTerm(Deal.Term);
            dealsProfilePage.InputSpaceRequired(Deal.SpaceRequired);
        }

        [When(@"clicks continue")]
        public void WhenTheUserClicksContinue()
        {
            dealsProfilePage.ClickContinue();
        }

        [Given(@"lease rate calculator page is opened")]
        [Then(@"the lease rate calculator page is opened")]
        public void ThenTheLeaseRateCalculatorIsOpened()
        {
            //_rentCalculationPage.CheckLeaseCalculatorPageLanded();
        }

        [Then(@"all Deal Information is displayed correctly")]
        public void ThenAllDealInformationIsDisplayed()
        {
            var Deal = sct.Get<CalculatorDealInfo>("Deal");

            _rentCalculationPage.CheckLeaseType(Deal.LeaseType); //testing screenshot
            _rentCalculationPage.CheckLeaseCommencement(Deal.StartDate);
            _rentCalculationPage.CheckLeaseExpiration(Deal.StartDate, Deal.Term);
            _rentCalculationPage.CheckMonths(Deal.Term);
            _rentCalculationPage.CheckSquareFootage(Deal.SpaceRequired);
        }

        [Given(@"Rate Type option '(.*)' is selected")]
        public void GivenRateTypeOptionIsSelected(string RateType)
        {
            leaseVariables.RateType = RateType;
            _rentCalculationPage.SelectRateType(RateType);
        }

        [Given(@"Base Rate column header displays '(.*)'")]
        [Then(@"Base Rate column header displays '(.*)'")]
        public void ThenBaseRateColumnHeaderDisplays(string HeaderUnderBaseRateTitle)
        {
            _rentCalculationPage.CheckHeaderUnderBaseRateIsDisplayed(HeaderUnderBaseRateTitle);
        }

        [Then(@"Base Rate column header '(.*)' is not displayed")]
        public void ThenBaseRateColumnHeaderIsNotDisplayed(string HeaderUnderBaseRateTitle)
        {
            _rentCalculationPage.
                CheckHeaderUnderBaseRateIsNotDisplayed(HeaderUnderBaseRateTitle);
        }

        [Then(@"the Rental Rate is \$(.*) per month")]
        public void ThenTheRentalRateIsRecalculatedAccordingly(double rentalRate)
        {
            Assert.Equal(rentalRate, _rentCalculationPage.RentalRateValue());
        }

        [Given(@"(.*) Tenant Rep fee type is selected")]
        [When(@"the user selects (.*) Tenant Rep fee type")]
        public void WhenTheUserSelectsSFTenantRepFeeType(string RentClientRepFeeType)
        {
            _rentCalculationPage.SelectRentClientRepFeeType(RentClientRepFeeType);
        }

        [Given(@"(.*) Landlord Rep fee type is selected")]
        [When(@"the user selects (.*) Landlord Rep fee type")]
        public void WhenTheUserSelectsSFLandlordRepFeeType(string RentClientRepFeeType)
        {
            _rentCalculationPage.SelectRentOpositeSideRepFee(RentClientRepFeeType);
        }

        [Then(@"(.*) Column hides away")]
        public void ThenTenantRepColumnHidesAway(string columnName)
        {
                _rentCalculationPage.
                    CheckTenantRepColumnIsNotDisplayed(columnName);
        }

        [Given(@"the (.*) toggle is clicked")]
        [When(@"the user clicks the (.*) toggle")]
        public void WhenTheUserClicksTheLeaseExpansionToggle(string toggleTitle)
        {
            _rentCalculationPage.ClickToggleByName(toggleTitle);
        }

        [Then(@"(.*) Column shows on RentsGrid table")]
        public void ThenSquareFootageColumnShowsOnRentsGridTable(string title)
        {
            Assert.True(_rentCalculationPage.ColumnHeader(title).Displayed);
        }


        [When(@"the user selects (.*) for Rent Abatement type")]
        public void WhenTheUserSelectsAmountForRentAbatement(string optionToSelect)
        {
            _rentCalculationPage.SelectRentAbatement(optionToSelect);
        }

        [Given(@"Annual % Increase (.*) is entered")]
        [When(@"the user enters Annual % Increase(.*)")]
        public void WhenTheUserEntersAnnualIncrease(double annualIncreasePercentage)
        {

            _rentCalculationPage.InputAnnualIncrease(annualIncreasePercentage);
        }

        [Then(@"the '(.*)'th Annual Year \(row\) Rate Per Sf is (.*)")]
        [Then(@"the '(.*)'d Annual Year \(row\) Rate Per Sf is (.*)")]
        [Then(@"the '(.*)'nd Annual Year \(row\) Rate Per Sf is (.*)")]
        public void ThenTheNdAnnualYearRowRatePerSfIs(int row, double RatePerSfRow)
        {
            var rentsGridBodyRows = _rentCalculationPage.GetRentsGridTableRows();

            Assert.Equal(RatePerSfRow, _rentCalculationPage.tdRatePerSf(rentsGridBodyRows[row-1]));
        }

        [Given(@"Rent Abatement Months (.*) is entered")]
        [When(@"the user enters Rent Abatement Months (.*)")]
        public void WhenEntersRentAbatementMonths(int freeMonths)
        {
            _rentCalculationPage.InputRentAbatementMonths(freeMonths);
        }

        [Then(@"footer rent grid Total Free Months is (.*)")]
        public void ThenFooterRentGridTotalFreeMonthsIs(double totalFreeMonths)
        {
            Assert.Equal(totalFreeMonths, _rentCalculationPage.FooterFreeTotalMonthsRent());
        }

        [When(@"enters Rent Abatement Amount (.*)")]
        public void WhenEntersRentAbatementAmount(int Amount)
        {
            _rentCalculationPage.InputRentAbatementAmount(Amount);
        }

        [Then(@"(.*)nd row of RentsGrid table has (.*) Free Rent Months")]
        [Then(@"(.*)st row of RentsGrid table has (.*) Free Rent Months")]
        public void ThenStRowOfRentsGridTableHasFreeRentMonths(int rowIndex, int freeRentMonthsExpected)
        {
            var rentsGridBodyRows = _rentCalculationPage.GetRentsGridTableRows();

            Assert.Equal(freeRentMonthsExpected, _rentCalculationPage.TdFreeRentMonths(rentsGridBodyRows[rowIndex-1]));
        }

        [Then(@"(.*)nd row of RentsGrid table has (.*) Free Rent Amount")]
        [Then(@"(.*)st row of RentsGrid table has (.*) Free Rent Amount")]
        public void ThenStRowOfRentsGridTableHasFreeRentAmount(int rowIndex, int freeRentAmountExpected)
        {
            var rentsGridBodyRows = _rentCalculationPage.GetRentsGridTableRows();

            Assert.Equal(freeRentAmountExpected, _rentCalculationPage.TdFreeRentAmount(rentsGridBodyRows[rowIndex - 1]));
        }

        [Given(@"Tenant Rep Fee (.*)% is entered")]
        [When(@"the user enters (.*) for Tenant Rep Fee")]
        public void WhenTheUserEntersForTenantRepFee(double percentage)
        {
            sct.Add("tenantPercentage",percentage);
            _rentCalculationPage.InputRentClientRepFee(percentage);
        }

        [Given(@"Landlord Rep Fee (.*)% is entered")]
        [When(@"the user enters (.*) for Landlord Rep Fee")]
        public void WhenTheUserEntersForLandlordRepFee(double percentage)
        {
            sct.Add("landlordPercentage", percentage);
            _rentCalculationPage.InputRentOppositeSideRepFee(percentage);
        }

        [When(@"the user enters (.*) \$ per Sf for Tenant Rep Fee")]
        public void WhenTheUserEntersPerSfForTenantRepFee(double amount)
        {
            _rentCalculationPage.InputRentClientRepFeePerSf(amount);
        }

        [When(@"the user enters (.*) \$ per Sf for Landlord Rep Fee")]
        public void WhenTheUserEntersPerSfForLandlordRepFee(double amount)
        {
            _rentCalculationPage.InputRentOppositeSideRepFeePerSf(amount);
        }

        [Then(@"all rows \(Annual Years\) have (.*) added on the Tenant Rep Column")]
        public void ThenForAllRentsGrid(double percentageExpected)
        {
            var rentsGridBodyRows = _rentCalculationPage.GetRentsGridTableRows();

            foreach (var row in rentsGridBodyRows)
            {
                Assert.Equal(percentageExpected, _rentCalculationPage.TdClientRepFeeInput(row));
            }
        }

        [Then(@"all rows \(Annual Years\) have (.*) added on the Landlord Rep Column")]
        public void ThenAllRowsAnnualYearsHaveAddedOnTheLandlordRepColumn(double percentageExpected)
        {
            var rentsGridBodyRows = _rentCalculationPage.GetRentsGridTableRows();

            foreach (var row in rentsGridBodyRows)
            {
                Assert.Equal(percentageExpected, _rentCalculationPage.TdOppositeSideRepFeeInput(row));
            }
        }

        [Then(@"all rows \(Annual Years\) show \$(.*) under the House column")]
        public void ThenAllRowsAnnualYearsShowUnderTheHouseColumn(int ClientRepFeeAmount)
        {
            var rentsGridBodyRows = _rentCalculationPage.GetRentsGridTableRows();

            foreach (var row in rentsGridBodyRows)
            {
                Assert.Equal(ClientRepFeeAmount, _rentCalculationPage.TdClientRepFeeAmountInput(row));
            }
        }

        [Then(@"all rows \(Annual Years\) show \$(.*) under the Outside column")]
        public void ThenAllRowsAnnualYearsShowUnderTheOutsideColumn(double OppositeSideRepFeeAmount)
        {
            var rentsGridBodyRows = _rentCalculationPage.GetRentsGridTableRows();

            foreach (var row in rentsGridBodyRows)
            {
                Assert.Equal(OppositeSideRepFeeAmount, _rentCalculationPage.TdOppositeSideRepFeeAmountInput(row));
            }
        }


        [Then(@"the (.*) is disabled")]
        public void ThenTheToggleIsDisabled(string toggleInputName)
        {
            Assert.False(_rentCalculationPage.ToggleInputByName(toggleInputName).Enabled);
        }
        
        [When(@"the user enters Expense Stop \$(.*)")]
        public void WhenTheUserEntersExpenseStop(double expenseValue)
        {
            _rentCalculationPage.InputExpenseStop(expenseValue);
        }

        [Then(@"(.*)th row of RentsGrid table has \$(.*) Expense")]
        [Then(@"(.*)nd row of RentsGrid table has \$(.*) Expense")]
        [Then(@"(.*)st row of RentsGrid table has \$(.*) Expense")]
        public void ThenStRowOfRentsGridTableHasExpense(int rowIndex, double expense)
        {
            var rentsGridBodyRows = _rentCalculationPage.GetRentsGridTableRows();
            
            Assert.Equal(expense, _rentCalculationPage.TdExpenseInput(rentsGridBodyRows[rowIndex-1]));
        }

        [Then(@"(.*)st row of RentsGrid table has \$(.*) House")]
        [Then(@"(.*)nd row of RentsGrid table has \$(.*) House")]
        public void ThenNdRowOfRentsGridTableHasHouse(int rowIndex, double house)
        {
            var rentsGridBodyRows = _rentCalculationPage.GetRentsGridTableRows();
            Assert.Equal(house, _rentCalculationPage.TdClientRepFeeAmountInput(rentsGridBodyRows[rowIndex-1]));
        }

        [When(@"deletes the (.*)th row")]
        [When(@"deletes the (.*)nd row")]
        [When(@"deletes the (.*)st row")]
        public void WhenDeletesTheStRow(int rowIndex)
        {
            var rentsGridBodyRows = _rentCalculationPage.GetRentsGridTableRows();
            sct.Add("rentsGridBodyRows", rentsGridBodyRows);

            _rentCalculationPage.DeletRowButtonClick(rentsGridBodyRows[rowIndex-1]);
            
        }

        [When(@"clicks the add rent button")]
        public void WhenClicksTheAddRentButton()
        {
            sct.Add("rentsGridBodyRows", _rentCalculationPage.GetRentsGridTableRows().Count);
            _rentCalculationPage.ClickAddRent();
        }

        [Then(@"an additional row is added")]
        public void ThenAnAdditionalRowIsAdded()
        {
            var rowsExpected = _rentCalculationPage.GetRentsGridTableRows().Count;
            var rows = sct.Get<int>("rentsGridBodyRows");

            Assert.Equal(rowsExpected, rows+1);
        }

        [Then(@"the row is deleted from the table")]
        public void ThenTheRowIsDeletedFromTheTable()
        {
            var rentsGridBodyRows = _rentCalculationPage.GetRentsGridTableRows();
            var RowsExpected = sct.Get<ReadOnlyCollection<IWebElement>>("rentsGridBodyRows");

            Assert.Equal(RowsExpected.Count-1, rentsGridBodyRows.Count);
        }

        [Then(@"the Effective Rate is (.*)\$ per SF")]
        public void ThenTheEffectiveRateIsPerSF(double effectiveRate)
        {
            Assert.Equal(_rentCalculationPage.EffectiveRatePerSf(), effectiveRate);
        }

        [Then(@"footer rent grid Total Months is (.*)")]
        public void ThenFooterRentGridTotalMonthsIs(double totalMonths)
        {
            Assert.Equal(totalMonths, _rentCalculationPage.FooterTotalMonthsRent());
        }

        [Then(@"the footer Total Gross Commission is \$(.*)")]
        public void ThenTheFooterTotalGrossCommissionIs(double totalGrossCommission)
        {
            Assert.Equal(totalGrossCommission, _rentCalculationPage.FooterTotalClientRepFeeAmount());
        }

        [Then(@"the footer total expense is \$(.*)")]
        public void ThenTheFooterTotalExpenseIs(double totalExpenses)
        {
            Assert.Equal(totalExpenses, _rentCalculationPage.FooterTotalExpensesAmount());
        }

        [When(@"the user clicks the Add Expansion plus button on the (.*)st row")]
        [When(@"the user clicks the Add Expansion plus button on the (.*)nd row")]
        [When(@"the user clicks the Add Expansion plus button on the (.*)th row")]
        public void WhenTheUserClicksTheAddExpansionPlusButtonOnTheStRow(int rowIndex)
        {
            var RentGridRow = _rentCalculationPage.GetRentsGridTableRows()[rowIndex - 1];
            sct.Add("RentGridRow", RentGridRow);

            _rentCalculationPage.ClickAddExpansion(RentGridRow);

        }

        [Then(@"an additional row for expansion is added under it")]
        public void ThenAnAdditionalRowForExpansionIsAddedUnderIt()
        {
            var MainRow = sct.Get<IWebElement>("RentGridRow");
            
            Assert.True(_rentCalculationPage.CheckExpansionRowIsDisplayed(MainRow));
        }

        [Given(@"an outside broker is added that doesn\'t share internal broker\'s commission")]
        public void GivenAnOutsideBrokerIsAddedThatDoesnSCommission()
        {
            dealsProfilePage.ClickAddOutsideBrokerButton();
            dealsProfilePage.ClickPopUpNoButton();
            dealsProfilePage.InputSecondBrokerName("Outside Broker");
            dealsProfilePage.ClickAddNew();
            dealsProfilePage.SelectCompanyName("RandCompany"); //pre-requisite
            dealsProfilePage.InputCommissionPercentageForSecondBroker("100");
            //dealsProfilePage.ClickFormContactDetailsSaveButton();
        }

        [Then(@"the footer Total Opposite Rep Fee Amount is \$(.*)")]
        public void ThenTheFooterTotalOppositeRepFeeAmountIs(double totalOppositeSideRepFeeAmount)
        {
            Assert.Equal(totalOppositeSideRepFeeAmount, _rentCalculationPage.FooterTotalOppositeSideRepFeeAmount());
        }

        [Then(@"the footer Total \(Commission\) Amount is \$(.*)")]
        public void ThenTheFooterTotalCommissionAmountIs(double totalCommission)
        {
            Assert.Equal(totalCommission, _rentCalculationPage.FooterTotalCommissionAmount());
        }

        [Then(@"House Broker Total percentage is (.*)%")]
        public void ThenHouseBrokerPercentageTotalIs(double percentage)
        {
            Assert.Equal(percentage, _rentCalculationPage.HouseBrokerTotalPercentageOfDeal());
        }
        
        [Then(@"House Broker Total amount is \$(.*)")]
        public void ThenHouseBrokerTotalAmountIs(double amount)
        {
            Assert.Equal(amount, _rentCalculationPage.HouseBrokerTotalAmountOfDeal());
        }

        [Then(@"percentage of House Broker on first row is (.*)%")]
        public void ThenPercentageOfHouseBrokerOnFirstRowIs(double percentage)
        {
            Assert.Equal(percentage, _rentCalculationPage.FirstHouseBrokerPercentageofDeal());
        }

        [Then(@"amount earned of House Broker on first row is \$(.*)")]
        public void ThenAmountEarnedOfHouseBrokerOnFirstRowIs(double amountEarned)
        {
            Assert.Equal(amountEarned, _rentCalculationPage.FirstHouseBrokerAmountOfDeal());
        }

        [Then(@"Outside Brokers Total amount is \$(.*)")]
        public void ThenOutsideBrokersTotalAmountIs(double amount)
        {
            Assert.Equal(amount, _rentCalculationPage.OutsideBrokersTotalAmountEarnedOfDeal());
        }

        [Then(@"Outside Broker Total percentage is (.*)%")]
        public void ThenOutsideBrokerTotalPercentageIs(double percentage)
        {
            Assert.Equal(percentage, _rentCalculationPage.OutsideBrokersTotalPercentageOfDeal());
        }

        [Then(@"percentage of Outside Broker on first row is (.*)%")]
        public void ThenPercentageOfOutsideBrokerOnFirstRowIs(double percentage)
        {
            Assert.Equal(percentage, _rentCalculationPage.FirstRowOutsideBrokersPercentageOfDeal());
        }

        [Then(@"amount earned of Outside Broker on first row is \$(.*)")]
        public void ThenAmountEarnedOfOutsideBrokerOnFirstRowIs(double amount)
        {
            Assert.Equal(amount, _rentCalculationPage.FirstRowOutsideBrokersAmountOfDeal());
        }

        [Then(@"total commission is \$(.*)")]
        public void ThenTotalCommissionIs(double totalCommission)
        {
            Assert.Equal(totalCommission, _rentCalculationPage.TotalCommissionAmount());
        }

    }
}