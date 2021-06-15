using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using Dealius.Models;
using OpenQA.Selenium.Interactions;
using Xunit;

namespace Dealius.Pages
{
    public class RentCalculationPage : BasePage
    {
        #region Locators
        private static readonly By SaveButton = By.Id("save");
        private static readonly By GenerateScheduleButton = By.XPath("//a[text()='Generate Schedule']");
        private static readonly By AddRentButton = By.Id("btnAddNewRent");
        private static readonly By DeleteRowButton = By.CssSelector("a.remove-link");
        private static readonly By DateRange = By.CssSelector("div[data-bind='text: datesRange']");
        private static readonly By RentAbatementMonths = By.Name("RentAbatementMonths");
        private static readonly By RatePerSfInput = By.Name("BaseRatePerSf");
        private static readonly By BaseRateInput = By.Name("BaseRate");
        private static readonly By AnnualIncreaseInput = By.Name("AnnualIncrease");
        private static readonly By RentAbatementMonthsInput = By.Name("RentAbatementMonths");
        private static readonly By RentAbatementAmountInput = By.Name("RentAbatementAmount");
        private static readonly By RentClientRepFeePercentageInput = By.Name("RentClientRepFeeUi");
        private static readonly By RentOppositeSideRepFeeInput = By.Name("RentOppositeSideRepFeeUi");
        private static readonly By RentClientRepFeePerSfInput = By.Name("RentClientRepFeePerSfUi");
        private static readonly By RentOppositeSideRepFeePerSfInput = By.Name("RentOppositeSideRepFeePerSfUi");
        private static readonly By ExpenseStopInput = By.Name("ExpensesStopAmount");
        private static readonly By LeaseStartDatetd = By.CssSelector("td[data-name='DealInfoLeaseStartDate']");
        private static readonly By LeaseExpirationDatetd = By.CssSelector("td[data-name='DealInfoLeaseTermDate']");
        private static readonly By LeaseTypetd = By.CssSelector("td[data-name='DealInfoLeaseType']");
        private static readonly By TermMonthstd = By.CssSelector("td[data-name='DealInfoTerm']");
        private static readonly By SquareFootagetd = By.CssSelector("td[data-name='DealInfoSquareFootage']");
        private static readonly By tdRentalRate = By.CssSelector("td > span[data-name='DealInfoRentalRate']");
        private static readonly By TableBodyRows = By.CssSelector("body > tr");
        private static readonly By RentsGridTable =
            By.CssSelector("#rentsGrid > div.k-grid-content > table");
        private static readonly By RateTypeSelect = By.CssSelector("select[name='RateType']");
        private static readonly By RentClientRepFeeTypeSelect = By.Name("RentClientRepFeeTypeUi");
        private static readonly By RentOppositeSideRepFeeSelect = By.Name("RentOppositeSideRepFeeTypeUi");
        private static readonly By RentAbatementTypeSelect = By.Name("RentAbatementType");
        private static readonly By AmortizeFreeRentToggle = By.CssSelector("input[name='AmortizeFreeRent']");
        private static By ToggleByName(string name) => By.Name(name);
        private static By RateTypeButton(string rateType) => By.CssSelector($"button[title='{rateType}']");
        private static By EffectiveRate = By.CssSelector("span[data-bind='textMoney: effectiveRate']");
        private static By FooterTotalMonths = By.CssSelector("span[data-name='RentsTotalMonths']");
        private static By FooterFreeTotalMonths = By.CssSelector("td[data-name='RentsTotalFreeRentMonths']");
        private static By FooterTotalClientRepAmount = By.CssSelector("td[data-name='RentsClientRepFeeAmount']");
        private static By FooterTotalOppositeSideRepAmount = By.CssSelector("td[data-name='RentsTotalOppositeRepFeeAmount']");
        private static By FooterTotalExpenses = By.CssSelector("td[data-name='RentsTotalExpensesAmount']");
        private static By FooterTotalCommisionAmount = By.CssSelector("td[data-name='RentsTotalCommissionAmount']");
        private static By ColumnTitle(string title) => By.XPath($"//descendant::th[contains(text(),'{title}')][@data-bind]");
        //====================================================================
        private static By tdMonthsInput = By.XPath(".//input[(contains(@name,'[Months]'))]");
        private static By tdRentPerSfInput = By.XPath(".//input[(contains(@name,'[RentPerSf]'))]");
        private static By tdRentPerSfPerMonthInput = By.XPath(".//input[(contains(@name,'[MonthlyRatePerSf]'))]");
        private static By tdFreeMonthsInput = By.XPath(".//input[(contains(@name,'[FreeMonths]'))]");
        private static By tdBaseRentAmountInput = By.XPath(".//input[(contains(@name,'[BaseRentAmount]'))]");
        private static By tdFreeRentAmountInput = By.XPath(".//input[(contains(@name,'[FreeRentAmount]'))]");
        private static By tdRentPerMonthAmountInput = By.XPath(".//input[(contains(@name,'[RentPerMonthAmount]'))]");
        private static By tdTotalLeaseAmountInput = By.XPath(".//input[(contains(@name,'[TotalLeaseAmount]'))]");
        private static By tdClientRepFeeInput = By.XPath(".//input[(contains(@name,'[ClientRepFee]'))]");
        private static By tdOppositeSideRepFeeInput = By.XPath(".//input[(contains(@name,'[OppositeSideRepFee]'))]");
        private static By tdClientRepFeeAmountInput = By.XPath(".//input[(contains(@name,'[ClientRepFeeAmount]'))]");
        private static By tdOppositeSideRepFeeAmountInput = By.XPath(".//input[(contains(@name,'[OppositeSideRepFeeAmount]'))]");
        private static By tdExpensesAmountInput = By.XPath(".//input[(contains(@name,'[ExpensesAmount]'))]");
        //=====================================================================
        /*private static By BaseRentMonthsInput(int i) => By.CssSelector($"input[name = 'Rents[{i-1}][Months]']");
        private static By FreeMonthInput(int i) => By.Name($"Rents[{i-1}][FreeMonths]");
        private static By MonthlyRatePerSf(int i) => By.Name($"Rents[{i-1}][MonthlyRatePerSf]");
        private static By RentPerMonthAmountInput(int i) => By.Name($"Rents[{i-1}][RentPerMonthAmount]");
        //private static By TotalLeaseAmountPerRowInput(int i) => By.Name($"Rents[{i-1}][TotalLeaseAmount]"); */
        #endregion
        public RentCalculationPage(IWebDriver driver) :base(driver) { }

        public void ClickSaveButton()
        {
            click(SaveButton);
        }
        public void InputRatePerSf(double ratePerSf)
        {
           Input(RatePerSfInput, ratePerSf + Keys.Tab);
        }

        public void ClickGenerateScheduleButton()
        {
            WaitElementToBeClickable(GenerateScheduleButton).Click();
        }

        public void CheckAllRowsAreDisplayed(int Months)
        {
            var NumberOfScheduleRows = Math.Ceiling((double)Months/12);

            Assert.True(AllRelativeElementsDisplayed(RentsGridTable, TableBodyRows), // throws an exception if all rows are not displayed
                "Not all rows are displayed"); 
            Assert.Equal(NumberOfScheduleRows, GetRentsGridTableRows().Count); // asserts the number of rows is correct
        }

        public double RentMonths(IWebElement row)
        {
            return GetElementValueDouble(Find(row, tdMonthsInput));
        }

        // Date Range
        //public void CheckDateRange(int termInMonths, DateTime startDate, int numberOfRows,int row)
        //{
        //    var TableRow = Find(RentsGridTable).FindElements(By.CssSelector("body > tr"));
        //    var dateRange = TableRow[0].FindElement(DateRange).Text;
        //    if (row == 1)
        //    {
        //        dateRange = TableRow[row-1].FindElement(DateRange).Text;
        //        var months = 12 - ((numberOfRows * 12) - termInMonths);
        //        var DateRangeExpected = $"{startDate:MM/dd/yyyy} - {startDate.AddMonths(months).AddDays(-1):MM/dd/yyyy}";
        //        Assert.Equal(DateRangeExpected, dateRange);
        //    }

        //    else
        //    {
        //        Assert.Equal(12, 12);
        //    }
        //}
        public double tdMonthlyRatePerSf(IWebElement row)
        {
            return GetElementValueDouble(Find(row, tdRentPerSfPerMonthInput));
        }

        public double tdRatePerSf(IWebElement row)
        {
            return GetElementValueDouble(Find(row, tdRentPerSfInput));
        }

        public double FreeRentMonths(IWebElement row)
        {
            return GetElementValueDouble(Find(row, tdFreeMonthsInput));
        }

        public double RentPerMonthAmount(IWebElement row)
        {
            return GetElementValueDouble(Find(row, tdRentPerMonthAmountInput));
        }

        public double TotalLeaseAmount(IWebElement row)
        {
            return GetElementValueDouble(Find(row, tdTotalLeaseAmountInput));
        }

        public void CheckLeaseCalculatorPageLanded()
        {
            WaitUrlContains("calculate-rent");
        }

        public void CheckLeaseType(string leaseTypeExpected)
        {
            string leaseType = WaitForElement(LeaseTypetd).Text;
            Assert.Equal(leaseTypeExpected, leaseType);
        }

        public void CheckLeaseCommencement(DateTime startDate)
        {
            string leaseCommencement = driver.FindElement(LeaseStartDatetd).Text;
            Assert.Equal(startDate.ToString("MM/dd/yyyy"), leaseCommencement);
        }

        public void CheckLeaseExpiration(DateTime startDate, int termMonths)
        {
            string leaseExpirationDateExpected = startDate.AddMonths((int)termMonths).AddDays(-1).ToString("MM/dd/yyyy");
            string leaseExpirationDate = driver.FindElement(LeaseExpirationDatetd).Text;
            Assert.Equal(leaseExpirationDateExpected, leaseExpirationDate);
        }

        public void CheckMonths(int monthsExpected)
        {
            double months = double.Parse(driver.FindElement(TermMonthstd).Text);
            Assert.Equal(monthsExpected, months);
        }

        public void CheckSquareFootage(int squareFootageExpected)
        {
            double squareFootage = double.Parse(driver.FindElement(SquareFootagetd).Text);
            Assert.Equal(squareFootageExpected, squareFootage);
        }
        
        public void CheckHeaderUnderBaseRateIsDisplayed(string headerTitle)
        {
            var element = Find(RentsGridTable, By.XPath($"//th[contains(text(),'{headerTitle}')]"));          
            Assert.True(element.Displayed);
        }

        public void SelectRateType(string rateTypeOption)
        {
            var select = new SelectElement(WaitElementEnabled(RateTypeSelect));
            string option = select.SelectedOption.Text;
            if (rateTypeOption == option);
            else
            {
                switch (rateTypeOption.ToLower())
                {
                    case "per month":
                        select.SelectByValue("1");
                        break;
                    case "per year":
                        select.SelectByValue("2");
                        break;
                }
            }

        }

        public void SelectRentClientRepFeeType(string optionToSelect)
        {
            var select = new SelectElement(WaitElementEnabled(RentClientRepFeeTypeSelect));
            string optionSelected = select.SelectedOption.Text;

            if (optionToSelect == optionSelected) ;
            switch (optionToSelect.ToLower())
            {
                case "%":
                    select.SelectByValue("1");
                    break;
                case "$/sf":
                    select.SelectByValue("2");
                    break;
            }
        }
        
        public void SelectRentOpositeSideRepFee(string optionToSelect)
        {
            var select = new SelectElement(WaitElementEnabled(RentOppositeSideRepFeeSelect));
            string optionSelected = select.SelectedOption.Text;

            if (optionToSelect == optionSelected) ;
            switch (optionToSelect.ToLower())
            {
                case "%":
                    select.SelectByValue("1");
                    break;
                case "$/sf":
                    select.SelectByValue("2");
                    break;
            }
        }

        public void CheckTenantRepColumnIsNotDisplayed(string columnHeaderTitle)
        {
            WaitElementDisappearsRelative(RentsGridTable, ColumnTitle(columnHeaderTitle));
        }

        public void CheckHeaderUnderBaseRateIsNotDisplayed(string headerTitle)
        {
            WaitElementDisappears(ColumnTitle(headerTitle));
        }

        public IWebElement ColumnHeader(string title)
        {
            return Find(RentsGridTable, ColumnTitle(title));

        }

        public double RentalRateValue()
        {
            var rentalRate = double.Parse(Find(tdRentalRate).Text.TrimStart('$')); ;

            return rentalRate;
        }

        public void InputRate(double rate)
        {
            Input(BaseRateInput,rate+Keys.Tab);
        }

        public double TdFreeRentMonths(IWebElement row)
        {
            return GetElementValueDouble(row.FindElement(tdFreeMonthsInput));
        }

        public double TdFreeRentAmount(IWebElement row)
        {
            return GetElementValueDouble(row.FindElement(tdFreeRentAmountInput));
        }

        public double TdClientRepFeeInput(IWebElement row)
        {
            return GetElementValueDouble(row.FindElement(tdClientRepFeeInput));
        }

        public double TdOppositeSideRepFeeInput(IWebElement row)
        {
            return GetElementValueDouble(row.FindElement(tdOppositeSideRepFeeInput));
        }

        public double TdClientRepFeeAmountInput(IWebElement row)
        {
            return GetElementValueDouble(row.FindElement(tdClientRepFeeAmountInput));
        }

        public double TdOppositeSideRepFeeAmountInput(IWebElement row)
        {
            return GetElementValueDouble(row.FindElement(tdOppositeSideRepFeeAmountInput));
        }

        public double TdExpenseInput(IWebElement row)
        {
            return GetElementValueDouble(row.FindElement(tdExpensesAmountInput));
        }
        
        public double TdBaseRentAmountInput(IWebElement row)
        {
            return GetElementValueDouble(row.FindElement(tdBaseRentAmountInput));
        }

        public int RatePerSf()
        {
            return (int)GetElementValueDouble(RatePerSfInput);
        }

        public int Rate()
        {
            return (int) GetElementValueDouble(BaseRateInput);
        }

        public void ClickToggleByName(string toggleTitle)
        {
            WaitElementEnabled(ToggleByName(toggleTitle));
            Find(ToggleByName(toggleTitle), By.XPath("./following-sibling::span")).Click();
        }

        public IWebElement ToggleInputByName(string toggleName)
        {
            return Find(ToggleByName(toggleName));
        }

        public void InputAnnualIncrease(double annualIncreasePercentage)
        {
            Input(AnnualIncreaseInput, annualIncreasePercentage.ToString());
        }

        public void SelectRentAbatement(string optionToSelect)
        {
            var select = new SelectElement(WaitElementEnabled(RentAbatementTypeSelect));
            string optionSelected = select.SelectedOption.Text;
            if (optionToSelect == optionSelected);
            switch (optionToSelect.ToLower())
            {
                case "months":
                    select.SelectByValue("1");
                    break;
                case "amount":
                    select.SelectByValue("2");
                    break;
            }
        }

        public void InputRentAbatementMonths (int freeMonths)
        {
            Input(RentAbatementMonthsInput, freeMonths.ToString());
        }

        public void InputRentAbatementAmount(int amount)
        {
            Input(RentAbatementAmountInput, amount.ToString());
        }

        public void InputRentClientRepFee(double percentage)
        {
            Input(RentClientRepFeePercentageInput, percentage.ToString());
        }

        public void InputRentOppositeSideRepFee(double percentage)
        {
            Input(RentOppositeSideRepFeeInput, percentage.ToString());
        }

        public void InputRentClientRepFeePerSf(double amount)
        {
            Input(RentClientRepFeePerSfInput, amount.ToString());
        }
        
        public void InputRentOppositeSideRepFeePerSf(double amount)
        {
            Input(RentOppositeSideRepFeePerSfInput, amount.ToString());
        }

        public void InputExpenseStop(double expenseValue)
        {
            Input(ExpenseStopInput,expenseValue.ToString());
        }

        public void DeletRowButtonClick(IWebElement row)
        {
            row.FindElement(DeleteRowButton).Click();
        }

        public double FooterTotalLeaseAmount()
        {
            var TotalLeaseAmount = Find(By.CssSelector("td[data-name='RentsTotalLeaseAmount']")).Text.TrimStart('$');
           
            return double.Parse(TotalLeaseAmount);
        }

        public double FooterTotalClientRepFee()
        {
            var TotalClientRepFee = Find(By.CssSelector("td[data-name='RentsClientRepFeeAmount']")).Text.TrimStart('$');

            return double.Parse(TotalClientRepFee);
        }

        public double EffectiveRatePerSf()
        {
            var effectiveRate = Find(EffectiveRate).Text.TrimStart('$');

            return double.Parse(effectiveRate);
        }

        public double FooterTotalMonthsRent()
        {
            var totalMonths = Find(FooterTotalMonths).Text.TrimStart('$');

            return double.Parse(totalMonths);
        }
        
        public double FooterFreeTotalMonthsRent()
        {
            var totalFreeMonths = Find(FooterFreeTotalMonths).Text.TrimStart('$');

            return double.Parse(totalFreeMonths);
        }
        
        public double FooterTotalClientRepFeeAmount()
        {
            var totalClientRepFeeAmount = Find(FooterTotalClientRepAmount).Text.TrimStart('$');

            return double.Parse(totalClientRepFeeAmount);
        }

        public double FooterTotalOppositeSideRepFeeAmount()
        {
            var totalOppositeSideRepFeeAmount = Find(FooterTotalOppositeSideRepAmount).Text.TrimStart('$');

            return double.Parse(totalOppositeSideRepFeeAmount);
        }

        public double FooterTotalCommissionAmount()
        {
            var totalCommissionAmount = Find(FooterTotalCommisionAmount).Text.TrimStart('$');

            return double.Parse(totalCommissionAmount);
        }

        public double FooterTotalExpensesAmount()
        {
            var totalExpenses = Find(FooterTotalExpenses).Text.TrimStart('$');

            return double.Parse(totalExpenses);
        }

        public void ClickAddRent()
        {
            click(AddRentButton);
        }
        
        public void ClickAddExpansion(IWebElement row)
        {
            row.FindElement(By.XPath("./descendant::a[@title='Add Expansion']")).Click();
        }
        
        public bool CheckExpansionRowIsDisplayed(IWebElement row)
        {
            var expansionRow = row.FindElement(By.XPath("./following-sibling::tr[@class='expansion']"));

            return expansionRow.Displayed;
        }
        //==================================================================
        public ReadOnlyCollection<IWebElement> GetRentsGridTableRows()
        {
            var tableRows = Find(RentsGridTable).FindElements(By.TagName("tr"));
            return tableRows;
        }
    }
}