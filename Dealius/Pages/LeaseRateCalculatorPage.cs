using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using Dealius.Utils;
using Xunit;
using System.Collections.Generic;

namespace Dealius.Pages
{
    class LeaseRateCalculatorPage
    {
        #region Locators
        By GenerateScheduleButton = By.XPath("//a[text()='Generate Schedule']");
        By DateRange = By.CssSelector("div[data-bind='text: datesRange']");
        By RatePerSFInput = By.Name("BaseRatePerSf");
        By RentAbatementMonths = By.Name("RentAbatementMonths");
        By LeaseStartDatetd = By.CssSelector("td[data-name='DealInfoLeaseStartDate']");
        By LeaseExpirationDatetd = By.CssSelector("td[data-name='DealInfoLeaseTermDate']");
        By LeaseTypetd = By.CssSelector("td[data-name='DealInfoLeaseType']");
        By TermMonthstd = By.CssSelector("td[data-name='DealInfoTerm']");
        By SquareFootagetd = By.CssSelector("td[data-name='DealInfoSquareFootage']");
        By RentalRatetd = By.CssSelector("td[data-name='DealInfoRentalRate']");
        By BaseRentMonthsInput(int i) => By.CssSelector($"input[name = 'Rents[{i}][Months]']");
        By FreeMonthInput(int i) => By.Name($"Rents[{i}][FreeMonths]");
        By MonthlyRatePerSF(int i) => By.Name($"Rents[{i}][MonthlyRatePerSf]");
        By RentPerMonthAmountInput(int i) => By.Name($"Rents[{i}][RentPerMonthAmount]");
        By TotalLeaseAmountPerRowInput(int i) => By.Name($"Rents[{i}][TotalLeaseAmount]");
        By TotalLeaseAmountRow(int i) => By.CssSelector($"input[name='Rents[{i}][TotalLeaseAmount]']");
        private IWebElement _TotalLeaseRow(int row) => driver.FindElement(TotalLeaseAmountRow(row));
        private IWebElement _DateRange => driver.FindElement(DateRange);
        #endregion
        IWebDriver driver;
        WebDriverWait wait;
        double RatePerSF { get; set; }
        int MonthlyRate { get; set; }
        int TotalLeasePerRow { get; set; }
        public LeaseRateCalculatorPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

        public void InputRatePerSF(double RatePerSF)
        {
            this.RatePerSF = RatePerSF;
            Method.Input(wait, RatePerSFInput, RatePerSF + Keys.Enter);
        }

        public void ClickGenerateScheduleButton()
        {
            Method.WaitElementToBeClickable(wait, GenerateScheduleButton).Click();
        }
        //======================================================================================
        public void CheckNumberOfRows(int rowIndex)
        {
            //"//div[@class='k-grid-content']/following::tbody[@role='rowgroup']/tr"
            int numberOfRows = driver.FindElements(By.XPath("//div[@class='k-grid-content']/following::tbody[@role='rowgroup']/tr")).Count;
            int numberOfRowsExpected = (int)Math.Ceiling((double)DealsProfilePage.TermInMonths / 12);
            Assert.Equal(numberOfRowsExpected, numberOfRows);
        }

        public void CheckRentMonths(int rowIndex)
        {
            double Months = Method.GetElementValueDouble(wait, BaseRentMonthsInput(rowIndex));
            int numberOfRows = (int)Math.Ceiling((double)DealsProfilePage.TermInMonths / 12);
            if (rowIndex == 0)
                Assert.Equal(DealsProfilePage.TermInMonths - ((numberOfRows - 1) * 12), Months);
            else
            {
                Assert.Equal(12, Months);
            }
        }
        public void CheckMonthlyRatePerSf(int rowIndex)
        {
            Assert.Equal(RatePerSF, Method.GetElementValueDouble(wait, MonthlyRatePerSF(rowIndex)));
        }

        public void CheckFreeRentMonths(int rowIndex)
        {
            Assert.Equal(Method.GetElementValueDouble(wait, FreeMonthInput(rowIndex)), Method.GetElementValueDouble(wait, RentAbatementMonths));
            Assert.Equal(0, Method.GetElementValueDouble(wait, FreeMonthInput(rowIndex)));
        }

        public void CheckRentPerMonthAmount(int rowIndex)
        {
            MonthlyRate = (int)(DealsProfilePage.SpaceInSF * RatePerSF);
            Assert.Equal(MonthlyRate, Method.GetElementValueDouble(wait, RentPerMonthAmountInput(rowIndex)));

        }

        public void CheckTotalLeaseAmount(int rowIndex)
        {
            double TotalLeaseRow = Method.GetElementValueDouble(wait, BaseRentMonthsInput(rowIndex)) * MonthlyRate;
            Assert.Equal(Method.GetElementValueDouble(wait, TotalLeaseAmountPerRowInput(rowIndex)), TotalLeaseRow);
        }
        public void CheckDateRange(int rowIndex)
        {
            double months = Method.GetElementValueDouble(wait, BaseRentMonthsInput(rowIndex));
            string ExpectedRange = $"{DealsProfilePage.StartDate:MM/dd/yyyy} - {DealsProfilePage.StartDate.AddMonths((int)months).AddDays(-1):MM/dd/yyyy}";
            Assert.Equal(ExpectedRange, _DateRange.Text);
        }

        public void CheckLeaseCalculatorPageLanded()
        {
            Method.WaitUrlContains(wait, "calculate-rent");
        }
        /*
        public void CheckHouseGrossCommission()
        {

        }
        */
        public void CheckLeaseType(string LeaseTypeExpected)
        {
            string LeaseType = driver.FindElement(LeaseTypetd).Text;
            Assert.Equal(LeaseTypeExpected, LeaseType);
        }

        public void CheckLeaseCommencement(DateTime StartDate)
        {
            string LeaseCommencement = driver.FindElement(LeaseStartDatetd).Text;
            Assert.Equal(StartDate.ToString("MM/dd/yyyy"), LeaseCommencement);
        }

        public void CheckLeaseExpiration(DateTime StartDate, int TermMonths)
        {
            string LeaseExpirationDateExpected = StartDate.AddMonths((int)TermMonths).AddDays(-1).ToString("MM/dd/yyyy");
            string LeaseExpirationDate = driver.FindElement(LeaseExpirationDatetd).Text;
            Assert.Equal(LeaseExpirationDateExpected, LeaseExpirationDate);
        }

        public void CheckMonths(int MonthsExpected)
        {
            double months = double.Parse(driver.FindElement(TermMonthstd).Text);
            Assert.Equal(MonthsExpected, months);
        }

        public void CheckSquareFootage(int SquareFootageExpected)
        {
            double SquareFootage = double.Parse(driver.FindElement(SquareFootagetd).Text);
            Assert.Equal(SquareFootageExpected, SquareFootage);
        }

        public void CheckRateType(string RateTypeText)
        {
            IWebElement RateTypeSelect = Method.WaitElementEnabled(wait, By.CssSelector("select[name='RateType']"));
            SelectElement select = new SelectElement(RateTypeSelect);

            //IList<IWebElement> options =  select.AllSelectedOptions;
            //foreach (var optionn in options)
            //{

            //}
            string option = select.SelectedOption.Text;
            Assert.Equal(RateTypeText, option);
        }
        
        public void CheckBaseRateTitle(string BaseRateTitle)
        {
            string BaseRateTitleText = Method.WaitElementEnabled(wait, By.XPath("//div[@id='rentsGrid']/descendant::th[contains(text(),'Monthly Rate')]")).Text;
            //string BaseRateTitleText = Method.WaitForElement(wait, By.XPath("//div[@id='rentsGrid']/descendant::th[13]")).Text;
            Assert.Equal(BaseRateTitle, BaseRateTitleText);
            Assert.
        }

        public void SelectRateType(string RateTypeOption)
        {
            IWebElement RateTypeSelect = Method.WaitElementEnabled(wait, By.CssSelector("select[name='RateType']"));
            SelectElement select = new SelectElement(RateTypeSelect);

            switch (RateTypeOption)
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
}

