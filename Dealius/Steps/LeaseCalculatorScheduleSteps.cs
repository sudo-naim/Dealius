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
        public LeaseCalculatorScheduleSteps(IWebDriver driver, FeatureContext fct , ScenarioContext sct)
        {
            this.sct = sct;
            this.fct = fct;
            dealiusPage = new DealiusPage(driver);
            dealsPage = new DealsPage(driver);
            dealsProfilePage = new DealsProfilePage(driver);
            _rentCalculationPage = new RentCalculationPage(driver);
        }

        [Given(@"deal transaction information is entered")]
        [When(@"deal transaction information is entered")]
        public void GivenDealInformationIsEntered(Table table)
        {
            Deal = table.CreateInstance<CalculatorDealInfo>();
            sct.Add("Deal", Deal);

            dealsProfilePage.ClickCalculate();
            dealsProfilePage.InputLeaseType(Deal.LeaseType);
            dealsProfilePage.InputCalculationStartDate(Deal.StartDate);
            dealsProfilePage.InputTerm(Deal.Term);
            dealsProfilePage.InputSpaceRequired(Deal.SpaceRequired);
            dealsProfilePage.ClickContinue();
        }

        [Given(@"deal info (.*) (.*) (.*) (.*) is entered")]
        public void GivenDealInformationIsEntered(string startDate, string leaseType, int term, int spaceRequired)
        {
            
            dealsProfilePage.ClickCalculate();
            dealsProfilePage.InputLeaseType(leaseType);
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

            dealiusPage.Login();
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
        public void WhenRatePerSFIsEntered()
        {
            leaseVariables.RatePerSf = new Random().Next(999);
            _rentCalculationPage.InputRatePerSf(leaseVariables.RatePerSf);
        }
        
        [Then(@"all the rows data are displayed correctly")]
        public void ThenAllTheRowInputFieldsAreDisplayedCorrectly()
        {
            var rentsGridBodyRows = _rentCalculationPage.GetRentsGridTableRows();
            var firstRow = rentsGridBodyRows[0]; 
            //we will first assert the first row since it can differ from the number of months
            //first the expected results of the table cells are calculated
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

        [Then(@"Total Lease footer cell displays correct value")]
        public void ThenTotalLeaseFooterCellDisplaysCorrectValue()
        {
            var TotalLease = (int)((leaseVariables.RatePerSf * Deal.SpaceRequired)*Deal.Term);

            Assert.Equal(TotalLease,_rentCalculationPage.FooterTotalLeaseAmount());
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
            dealsProfilePage.InputLeaseType(Deal.LeaseType);
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
            _rentCalculationPage.CheckLeaseCalculatorPageLanded();
        }

        [Then(@"all Deal Information is displayed correctly")]
        public void ThenAllDealInformationIsDisplayed()
        {
            var Deal = sct.Get<CalculatorDealInfo>("Deal");

            _rentCalculationPage.CheckLeaseType(Deal.LeaseType);
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

        [Then(@"the Rental Rate is calculated accordingly")]
        public void ThenTheRentalRateIsRecalculatedAccordingly()
        {
            _rentCalculationPage.CheckDealInfoRentalRate(leaseVariables.RatePerSf, Deal.SpaceRequired);
        }

        [Given(@"(.*) Tenant Rep fee type is selected")]
        [When(@"the user selects (.*) Tenant Rep fee type")]
        public void WhenTheUserSelectsSFTenantRepFeeType(string RentClientRepFeeType)
        {
            _rentCalculationPage.SelectRentClientRepFeeType(RentClientRepFeeType);
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
        public void WhenTheUserEntersForTenantRepFee(int percentage)
        {
            sct.Add("percentage",percentage);
            _rentCalculationPage.InputRentClientRepFee(percentage);
        }

        [When(@"the user enters (.*) \$ per Sf for Tenant Rep Fee")]
        public void WhenTheUserEntersPerSfForTenantRepFee(int amount)
        {
            _rentCalculationPage.InputRentClientRepFeePerSf(amount);
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

        [Then(@"all rows \(Annual Years\) show \$(.*) under the House column")]
        public void ThenAllRowsAnnualYearsShowUnderTheHouseColumn(int ClientRepFeeAmount)
        {
            var rentsGridBodyRows = _rentCalculationPage.GetRentsGridTableRows();

            foreach (var row in rentsGridBodyRows)
            {
                Assert.Equal(ClientRepFeeAmount, _rentCalculationPage.TdClientRepFeeAmountInput(row));
            }
        }

        [Then(@"the (.*) is disabled")]
        public void ThenTheToggleIsDisabled(string toggleInputName)
        {
            Assert.False(_rentCalculationPage.ToggleInputByName(toggleInputName).Enabled);
        }

        [When(@"the user enters Expense Stop (.*)")]
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


    }
}