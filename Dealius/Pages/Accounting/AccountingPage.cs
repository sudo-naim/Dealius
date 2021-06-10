using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Dealius.Pages
{
    class AccountingPage : BasePage
    {
        #region locators
        private static By ViewReceiptButtonn = By.XPath("//a[@title='View Receipt']");
        private static By DeletePaymentButtonn = By.XPath("//a[@title='Delete Payment']");
        private static By PrintReceiptButtonn = By.CssSelector("a[title='Print Receipt']");
        private static By MakePaymentButton = By.XPath("//a[@title='Make Payment']");
        private static By EmailInvoiceButton = By.XPath("//a[@title='Email Invoice']");
        private static By PopUpYESButton = By.XPath("//button[text()='YES']");
        private static By PopUpSendButton = By.XPath("//button[contains(text(),'SEND')]");
        private static By PopUpConfirm = By.XPath("//button[contains(text(),'CONFIRM')]");
        private static By ViewButton = By.XPath("//a[@title='View']");
        private static By AddReceiptButton = By.CssSelector("a[title='Add Receipt']");
        private static By ClearOpenBalanceButton = By.CssSelector("a[title='Clear Open Balance']");
        private static By LoadingImage = By.XPath("//div[@class='k-loading-image']");
        private static By SearchTermInputReceivables = By.XPath("//form[@data-tab='receivables']/descendant::input[@name='SearchTerm']");
        private static By SearchTermInputPayables = By.XPath("//form[@data-tab='payables']/descendant::input[@name='SearchTerm']");
        private static By SearchTermInputInvoices = By.XPath("//form[@data-tab='invoices']/descendant::input[@name='SearchTerm']");
        private static By UlReceivablesTab = By.XPath("//ul[@id='tabs-general']/li/a[contains(text(),'Receivables')]");
        private static By FilterDateRangeAllReceivables = By.XPath("//form[@data-tab='receivables']/descendant::input[@name='DateRange']/..");
        private static By FilterDateRangeAllPayables = By.XPath("//form[@data-tab='payables']/descendant::input[@name='DateRange']/..");
        private static By FilterDateRangeAllInvoices = By.XPath("//form[@data-tab='invoices']/descendant::input[@name='DateRange']/..");
        //By.XPath("//td[@data-column='Status']/span");
        private static By RelevanceFilter = By.Name("Relevance");
        private static By InvoiceStatus = By.CssSelector("td[data-column='Status'] >span");

        private static By tdDealID = By.CssSelector("td[data-column='DealID'] > span");
        private static By tdDealReceivablesDueAmount = By.CssSelector("td[data-column='DealReceivablesDueAmount']");
        private static By tdDueAmount = By.CssSelector("td[data-column='DueAmount']");
        private static By tdPaymentMethod = By.CssSelector("td[data-column='PaymentMethod']");
        private static By tdReference = By.CssSelector("td[data-column='PaymentReferenceNo']");
        private static By tdPaidDate = By.CssSelector("td[data-column='PaidDate']");
        private static By tdAmountRecevied = By.CssSelector("td[data-column='PaidAmount']");
        private static By tdOpenBalanceAmount = By.CssSelector("td[data-column='OpenBalanceAmount']");
        private static By tdAmountPaid = By.XPath("//div[@data-tab='payables']/descendant::td[@data-column='PaidAmount']");

        private static By trBroker(string brokerName) => By.XPath($"//td[@data-column='Payee'][contains(text(),'{brokerName}')]/..");

        private static By PayablesTab = By.CssSelector("div[data-tab='payables']");
        private static By ReceivablesTab = By.CssSelector("div[data-tab='receivables']");
        private static By InvoicesTab = By.CssSelector("div[data-tab='invoices']");

        private static By UlInvoicesTab = By.XPath("//ul[@id='tabs-general']/li/a[contains(text(),'Invoices')]");

        private static By ToEmailInput = By.CssSelector("input[class='select2-search__field']");
        private static By EmailSuccesfullySentPopUp = By.XPath("//span[contains(text(),'Email successfully sent')]");
        #endregion
        public AccountingPage(IWebDriver driver) : base(driver) { }

        public IWebElement ViewReceiptButton()
        {
            return WaitElementDisplayed(ViewReceiptButtonn);
        }

        public IWebElement DeletePaymentButton()
        {
            return WaitElementDisplayed(DeletePaymentButtonn);
        }

        public IWebElement PrintReceiptButton()
        {
            return WaitElementDisplayed(PrintReceiptButtonn);
        }

        public void ClickPopUpConfirm()
        {
            click(PopUpConfirm);
            try
            {
                WaitElementDisplayed(LoadingImage);
            }
            catch (Exception e)
            {
                WaitElementDisappears(LoadingImage);
            }
            WaitElementDisappears(LoadingImage);
        }

        public void ClickYes()
        {
            click(PopUpYESButton);
            WaitElementDisappears(LoadingImage);
        }

        public void ClickReceivablesTab()
        {
            click(UlReceivablesTab);
        }

        public void ClickPayablesTab()
        {
            click(PayablesTab);
        }

        public void ClickInvoicesTab()
        {
            click(UlInvoicesTab);
        }

        public void ClickViewReceiptButton()
        {
            click(ViewReceiptButtonn);
        }

        public void ClickAddReceiptButton()
        {
            WaitElementAndClick(AddReceiptButton);
        }

        public void ClickDeletePaymentButton()
        {
            click(DeletePaymentButtonn);
        }

        public IWebElement OpenClearBalanceButton()
        {
            return WaitElementDisplayed(ClearOpenBalanceButton);
        }

        public void InputSearchTermReceivables(string searchText)
        {
            WaitElementEnabled(SearchTermInputReceivables).SendKeys(searchText);
            //WaitElementDisplayed(LoadingImage);
            WaitElementDisappears(LoadingImage);
        }

        public void InputSearchTermInvoices(string searchText)
        {
            WaitElementEnabled(SearchTermInputInvoices).SendKeys(searchText);
            //WaitElementDisplayed(LoadingImage);
            WaitElementDisappears(LoadingImage);
        }

        public void InputSearchTermPayables(string searchText)
        {
            WaitElementEnabled(SearchTermInputPayables).SendKeys(searchText);
            WaitElementDisappears(LoadingImage);
        }

        public void ClickFilterDateRangeAllReceivables()
        {
            WaitElementAndClick(FilterDateRangeAllReceivables);
            WaitElementDisappears(LoadingImage);
        }

        public void ClickFilterDateRangeAllInvoices()
        {
            WaitElementAndClick(FilterDateRangeAllInvoices);
        }
        
        public void ClickFilterDateRangeAllPayables()
        {
            WaitElementAndClick(FilterDateRangeAllPayables);
            WaitElementDisappears(LoadingImage);
        }

        public void SelectAllRelevance()
        {
            var select = new SelectElement(WaitElementEnabled(RelevanceFilter));
            select.SelectByValue("");
        }

        public void SelectAllRelevancePayables()
        {
            var el = WaitElementEnabled(FindElements(RelevanceFilter)[1]);
            var select = new SelectElement(el);
            select.SelectByValue("");
        }

        public void SelectOpenReceivablesRelevance()
        {
            var select = new SelectElement(WaitElementEnabled(RelevanceFilter));
            select.SelectByValue("1");
        }

        public int DealId()
        {
            var DealId = int.Parse(WaitElementDisplayed(tdDealID).Text);
            return DealId;
        }

        public double TotalDueReceivable()
        {

            return double.Parse(WaitElementDisplayed(tdDealReceivablesDueAmount).Text.TrimStart('$'));
        }

        public double AmountDueReceivable()
        {

            return double.Parse(WaitElementDisplayed(tdDueAmount).Text.TrimStart('$'));
        }

        public double OpenBalanceAmountReceivable()
        {

            return double.Parse(WaitElementDisplayed(tdOpenBalanceAmount).Text.TrimStart('$'));
        }
        
        public string OpenBalanceAmountReceivableNegative()
        {

            return WaitElementDisplayed(tdOpenBalanceAmount).Text;
        }

        public string OpenBalanceAmountReceivableText()
        {

            return WaitElementDisplayed(tdOpenBalanceAmount).Text;
        }

        public double AmountReceviedReceivable()
        {

            return double.Parse(WaitElementDisplayed(tdAmountRecevied).Text.TrimStart('$'));
        }
        
        public double PayablesPayeeAmountPaid(string payee)
        {

            WaitForElement(trBroker(payee));

            var el = Find(trBroker(payee)).FindElement(tdAmountRecevied);

            return double.Parse(el.Text.TrimStart('$'));
        }

        public void ClickDeletePaymentButton(string payee)
        {
            WaitForElement(trBroker(payee));

            var el = Find(trBroker(payee)).FindElement(DeletePaymentButtonn);

            el.Click();
        }

        public string AmountReceviedReceivableText()
        {
            return WaitElementDisplayed(tdAmountRecevied).Text;
        }

        public string AmountPaidPayablesText(string payee)
        {
            WaitForElement(trBroker(payee));
            return Find(trBroker(payee)).FindElement(tdAmountRecevied).Text;
        }

        public string PaymentMethodPayablesText(string payee)
        {
            WaitForElement(trBroker(payee));
            return Find(trBroker(payee)).FindElement(tdPaymentMethod).Text;
        }

        public string PaymentReferencePayablesText(string payee)
        {
            WaitForElement(trBroker(payee));
            return Find(trBroker(payee)).FindElement(tdReference).Text;
        }

        public string PaidDatePayablesText(string payee)
        {
            WaitForElement(trBroker(payee));
            return Find(trBroker(payee)).FindElement(tdPaidDate).Text;
        }
        
        public string OpenBalanceAmountPayablesText(string payee)
        {
            WaitForElement(trBroker(payee));
            return Find(trBroker(payee)).FindElement(tdOpenBalanceAmount).Text;
        }

        public void ClickClearOpenBalance()
        {
            click(ClearOpenBalanceButton);
        }

        public void WaitForLoadingImage()
        {
            try
            {
                WaitElementDisplayed(LoadingImage);
            }
            catch (Exception e)
            {
                WaitElementDisappears(LoadingImage);
            }
            WaitElementDisappears(LoadingImage);
        }

        public void refreshPage()
        {
            driver.Navigate().Refresh();
        }

        public string InvoiceStatusText()
        {
            return WaitElementDisplayed(InvoiceStatus).Text;
        }

        public void ClickEmailInvoice()
        {
            click(EmailInvoiceButton);
        }
        
        public void ClickPopUpSendButton()
        {
            click(PopUpSendButton);
        }

        public void InputPopUpToEmail()
        {
            Input(ToEmailInput, "test@hotmail.com"+Keys.Enter);
        }

        public void WaitForLoadingImageToDissapear()
        {
            WaitElementDisplayed(LoadingImage);
            WaitElementDisappears(LoadingImage);
        }
    }
}
