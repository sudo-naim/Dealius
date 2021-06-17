using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealius.Pages
{
    class AddReceiptPage : BasePage
    {
        #region locators
        private static By AmountInput = By.Name("Amount");
        private static By PaidDateInput = By.Name("PaidDate");
        private static By ReferenceInput = By.Id("Reference");
        private static By SavePayButton = By.XPath("//button[contains(text(),'SAVE & PAY')]");
        private static By SaveButton = By.XPath("//button[text()='SAVE']");
        private static By PaymentMethod = By.Name("PaymentMethod");
        private static By RelevanceFilter = By.Name("Relevance");
        private static By PopUpYESButton = By.XPath("//button[contains(@class,'js-yes')]");
        private static By SecondPopUpYesButton = By.XPath("//button[text()='YES']");

        private static By tdDealReceivablesDueAmount = By.CssSelector("td[data-column='DealReceivablesDueAmount']");
        private static By tdDueAmount = By.CssSelector("td[data-column='DueAmount']");
        private static By tdAmountRecevied = By.CssSelector("td[data-column='PaidAmount']");
        private static By tdOpenBalanceAmount = By.CssSelector("td[data-column='OpenBalanceAmount']");

        private static By PopUpConfirm = By.CssSelector("div.popup.confirm");

        #endregion
        public AddReceiptPage(IWebDriver driver) : base(driver) { }

        public void InputAmount(double amount)
        {
            Find(AmountInput).Clear();
            Input(AmountInput, amount.ToString());
        }

        public void ClickSavePayButton()
        {
            click(SavePayButton);
        }

        public void ClickSecondPopUpSaveButton()
        {
            WaitElementClick(SecondPopUpYesButton);
        }

        public void ClickSaveButton()
        {
            click(SaveButton);
        }

        public void SelectPaymentMethod(string value= "2")
        {
            var select = new SelectElement(WaitElementEnabled(PaymentMethod));
            select.SelectByValue(value);
        }

        public void InputReference(string text= "111")
        {
            Input(ReferenceInput, text);
        }

        public void SelectAllRelevanceFilter()
        {
            var select = new SelectElement(WaitElementEnabled(RelevanceFilter));

            select.SelectByValue("1");  //value 1 is equal to All accounts or All Receivables or All Payables or All Statuses
        }

        public void ClickPopUpYesButton()
        {
            click(PopUpYESButton);
        }

        public IWebElement ConfirmPopUp()
        {
            return Find(PopUpConfirm);
        }

        public void WaitForPopUpWriteOffPayablesDisplay()
        {
            WaitElementDisplayed(By.XPath("//table[@id='payables']"));
        }
    }
}
