using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealius.Pages
{
    class AccountingPage : BasePage
    {
        #region locators
        private static By SearchTermInput = By.Name("SearchTerm");
        private static By ReceivablesTab = By.XPath("//ul[@id='tabs-general']/li/a[contains(text(),'Receivables')]");
        private static By ViewReceiptButton = By.XPath("//a[@title='View Receipt']");
        private static By DeletePaymentButton = By.XPath("//a[@title='Delete Payment']");
        private static By MakePaymentButton = By.XPath("//a[@title='Make Payment']");
        private static By EmailInvoiceButton = By.XPath("//a[@title='Email Invoice']");
        private static By ViewButton = By.XPath("//a[@title='View']");
        private static By tdOpenBalanceAmount = By.CssSelector("td[data-column='OpenBalanceAmount']");
        private static By tdPaidAmount = By.CssSelector("td[data-column='PaidAmount']");
        private static By tdDueAmount = By.CssSelector("td[data-column='DueAmount']");
        private static By tdDealReceivablesDueAmount = By.CssSelector("td[data-column='DealReceivablesDueAmount']");

        private static By FilterDateRangeAll = By.CssSelector("input[name='DateRange']");
        //By.XPath("//td[@data-column='Status']/span");
        #endregion
        public AccountingPage(IWebDriver driver) : base(driver) { }

        public void ClickReceivablesTab()
        {
            click(ReceivablesTab);
        }

        public void ClickViewReceiptButton()
        {
            click(ViewReceiptButton);
        }


        public void ClickDeletePaymentButton()
        {
            click(DeletePaymentButton);
        }

        public void InputSearchTerm(string searchText)
        {
            Input(SearchTermInput, searchText);
        }

        public void ClickFilterDateRangeAll()
        {
            click(FilterDateRangeAll);
        }
    }
}
