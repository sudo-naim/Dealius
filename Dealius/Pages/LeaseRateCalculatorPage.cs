using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.CodeDom;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Xunit.Assert;

namespace Dealius.Pages
{
    public class LeaseRateCalculatorPage : BasePage
    {
        #region Locators
        private static readonly By GenerateScheduleButton = By.XPath("//a[text()='Generate Schedule']");
        private static readonly By DateRange = By.CssSelector("div[data-bind='text: datesRange']");
        private static readonly By RentAbatementMonths = By.Name("RentAbatementMonths");
        private static readonly By RatePerSfInput = By.Name("BaseRatePerSf");
        private static readonly By LeaseStartDatetd = By.CssSelector("td[data-name='DealInfoLeaseStartDate']");
        private static readonly By LeaseExpirationDatetd = By.CssSelector("td[data-name='DealInfoLeaseTermDate']");
        private static readonly By LeaseTypetd = By.CssSelector("td[data-name='DealInfoLeaseType']");
        private static readonly By TermMonthstd = By.CssSelector("td[data-name='DealInfoTerm']");
        private static readonly By SquareFootagetd = By.CssSelector("td[data-name='DealInfoSquareFootage']");
        private static readonly By tdRentalRate = By.CssSelector("td[data-name='DealInfoRentalRate']");
        private static readonly By TableBodyRows = By.CssSelector("body > tr");
        private static readonly By RentsGridTable =
            By.CssSelector("#rentsGrid > div.k-grid-content > table");
        private static readonly By RateTypeSelect = By.CssSelector("select[name='RateType']");
        //====================================================================
        private static By tdMonthsInput = By.XPath(".//input[(contains(@name,'[Months]'))]");
        private static By tdRentPerSfInput = By.XPath(".//input[(contains(@name,'[MonthlyRatePerSf]'))]");
        private static By tdFreeMonthsInput = By.XPath(".//input[(contains(@name,'[FreeMonths]'))]");
        private static By tdRentPerMonthAmountInput = By.XPath(".//input[(contains(@name,'[RentPerMonthAmount]'))]");
        private static By tdTotalLeaseAmountInput = By.XPath(".//input[(contains(@name,'[TotalLeaseAmount]'))]");
        //=====================================================================
        /*private static By BaseRentMonthsInput(int i) => By.CssSelector($"input[name = 'Rents[{i-1}][Months]']");
        private static By FreeMonthInput(int i) => By.Name($"Rents[{i-1}][FreeMonths]");
        private static By MonthlyRatePerSf(int i) => By.Name($"Rents[{i-1}][MonthlyRatePerSf]");
        private static By RentPerMonthAmountInput(int i) => By.Name($"Rents[{i-1}][RentPerMonthAmount]");
        //private static By TotalLeaseAmountPerRowInput(int i) => By.Name($"Rents[{i-1}][TotalLeaseAmount]"); */
        #endregion
        public LeaseRateCalculatorPage(IWebDriver driver) :base(driver) { }

        public void InputRatePerSf(double ratePerSf)
        {
           Input(RatePerSfInput, ratePerSf + Keys.Enter);
        }

        public void ClickGenerateScheduleButton()
        {
            WaitElementToBeClickable(GenerateScheduleButton).Click();
        }

        public void CheckAllRowsAreDisplayed(int Months)
        {
            var NumberOfScheduleRows = (Months/12)+1;

            Assert.True(AllRelativeElementsDisplayed(RentsGridTable, TableBodyRows), // throws an exception if all rows are not displayed
                "Not all rows are displayed"); 
            Assert.Equal(NumberOfScheduleRows, GetRentsGridTableRows().Count); // asserts the number of rows is correct
        }

        public void CheckInputFieldsForRow(DateTime d, int Months,  int spaceInSf, double ratePerSf)
        {
            var tablerows = GetRentsGridTableRows().Skip(1);
            foreach (var row in tablerows)
            {
                CheckRentMonths(row);
                CheckMonthlyRatePerSf(row, ratePerSf);
                CheckFreeRentMonths(row);
                CheckRentPerMonthAmount(row, spaceInSf, ratePerSf);
                CheckTotalLeaseAmount(row, spaceInSf, ratePerSf);
            }
            
        }

        public void CheckRentMonthsFirstRow(IWebElement row, int termInMonths, int numberOfRows)
        {
            var monthsExpectedFirstRow = 12 - ((numberOfRows * 12) - termInMonths);
            var months = GetElementValueDouble(Find(row, tdMonthsInput)); //gets the months that are displayed on the first row

            Assert.Equal(monthsExpectedFirstRow, months);

        }

        //tdrow 1
        public void CheckRentMonths(IWebElement row)
        {
            var months = GetElementValueDouble(Find(row,tdMonthsInput));
            Assert.Equal(12, months);
        }

        //tdrow 2
        public void CheckMonthlyRatePerSf(IWebElement row, double RatePerSF)
        {
            Assert.Equal(RatePerSF, GetElementValueDouble(Find(row, tdRentPerSfInput)));
        }

        //tdrow 3
        public void CheckFreeRentMonths(IWebElement row)
        {
            if (GetPseudoElementCSSContentValue().Contains("no")) 
                Assert.Equal(0,GetElementValueDouble(Find(row, tdFreeMonthsInput)));
        }
        //tdrow 4
        public void CheckRentPerMonthAmount(IWebElement row, int spaceInSf, double ratePerSf)
        {
            double MonthlyRate = spaceInSf * ratePerSf; // calculate rate per month
            Assert.Equal(MonthlyRate, GetElementValueDouble(Find(row, tdRentPerMonthAmountInput)));
        }

        //tdrow 5
        public void CheckTotalLeaseAmount(IWebElement row, int spaceInSf, double ratePerSf)
        {
            double totalLeaseOfRow = (ratePerSf * spaceInSf)*12;
            Assert.Equal(totalLeaseOfRow, GetElementValueDouble(Find(row, tdTotalLeaseAmountInput)));
        }

        // column 6
        public void CheckDateRange(int termInMonths, DateTime startDate, int numberOfRows,int row)
        {
            var TableRow = Find(RentsGridTable).FindElements(By.CssSelector("body > tr"));
            var dateRange = TableRow[0].FindElement(DateRange).Text;
            if (row == 1)
            {
                dateRange = TableRow[row-1].FindElement(DateRange).Text;
                var months = 12 - ((numberOfRows * 12) - termInMonths);
                var DateRangeExpected = $"{startDate:MM/dd/yyyy} - {startDate.AddMonths(months).AddDays(-1):MM/dd/yyyy}";
                Assert.Equal(DateRangeExpected, dateRange);
            }

            else
            {
                Assert.Equal(12, 12);
            }
        }

        public void CheckLeaseCalculatorPageLanded()
        {
            WaitUrlContains("calculate-rent");
        }

        public void CheckLeaseType(string leaseTypeExpected)
        {
            string leaseType = driver.FindElement(LeaseTypetd).Text;
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
            var element = Find(RentsGridTable, By.XPath("descendant::th[contains(text(),'{baseRateTitle}')]"));           
            Assert.True(element.Displayed);
        }

        public void SelectRateType(string rateTypeOption)
        {
            
            var select = new SelectElement(WaitElementEnabled(RateTypeSelect));
            string option = select.SelectedOption.Text;
            if (rateTypeOption == option);
            else
            {
                switch (rateTypeOption)
                {
                    case "Per Month":
                        select.SelectByValue("1");
                        break;
                    case "Per Year":
                        select.SelectByValue("2");
                        break;
                }
            }

        }

        public void CheckHeaderUnderBaseRateIsNotDisplayed(string headertitle)
        {
            WaitElementDisappears(By.XPath("descendant::th[contains(text(),'{baseRateTitle}')]"));
        }
        //==================================================================
        public ReadOnlyCollection<IWebElement> GetRentsGridTableRows()
        {
            var tableRowss = FindAll(RentsGridTable).FindElements(TableBodyRows);
            var tableRows = FindAll(RentsGridTable).FindElements(By.TagName("tr"));
            return tableRows;
        }
    }
}

