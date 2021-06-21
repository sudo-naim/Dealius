using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealius.Pages
{
    class MakePaymentPage : BasePage
    {
        #region locators
        private static By AmoountPayablesInput = By.Name("Payables[0][Payments][0][Amount]");
        private static By SecondAmoountPayablesInput = By.Name("Payables[1][Payments][0][Amount]");
        private static By AmountPaymentInput(int index) => By.Name($"Payables[{index}][Payments][0][Amount]");
        private static By SaveButton = By.XPath("//button[contains(text(),'SAVE')]");
        private static By YesButton = By.CssSelector("button.btn.btn-primary.js-yes");
        private static By NoButton = By.CssSelector("button.btn.btn-default.js-no");
        private static By PaymentMethod = By.XPath("//select[contains(@name,'[Payments][0][MethodId]')]");
        private static By PaymentMethodSelectForPayable(int index) => By.Name($"Payables[{index}][Payments][0][MethodId]");
        private static By ReferenceInputForPayable(int index) => By.Name($"Payables[{index}][Payments][0][ReferenceNumber]");
        private static By ReferenceInput = By.XPath("//input[contains(@name,'[Payments][0][ReferenceNumber]')]");
        private static By PaymentRows = By.XPath("//div[@class='row d-flex payment-row']");
        private static By TotalHouseNet = By.CssSelector("label[data-name='TotalHouseNet']");

        #endregion
        public MakePaymentPage(IWebDriver driver) : base(driver) { }

        public void ClickSaveButton()
        {
            click(SaveButton);
        }

        public void ClickYesButton()
        {
            click(YesButton);
        }
        
        public void ClickNoButton()
        {
            click(NoButton);
        }

        public void SelectPaymentMethodForAllPayees(string value = "2")
        {
            WaitElementDisplayed(PaymentRows);

            var paymentMethods = driver.FindElements(PaymentMethod);
            foreach (var payMethod in paymentMethods)
            {
            var select = new SelectElement(WaitElementEnabled(payMethod)); //heree name="Payables[0][Payments][0][MethodId]
                select.SelectByValue(value);                                           //Payables[1][Payments][0][MethodId]
            }
        }

        public void SelectPayableMethodForPayee(int payableIndex, string text)
        {
            WaitElementDisplayed(PaymentRows);
            var select = new SelectElement(WaitElementEnabled(PaymentMethodSelectForPayable(payableIndex-1)));

            select.SelectByText(text);
        }

        public void InputReferenceForAllPayees(string text = "111")
        {
            WaitElementDisplayed(PaymentRows);
            var ReferenceInputs = driver.FindElements(ReferenceInput);

            foreach (var refInput in ReferenceInputs)
            {
                Input(refInput, text);
            }
            
        }

        public void InputReferenceForPayee(int payableIndex, string text)
        {
            WaitElementDisplayed(PaymentRows);
            Input(ReferenceInputForPayable(payableIndex-1), text);
        }

        public void InputAmountForPayee(int payableIndex, double amount)
        {
            Input(AmountPaymentInput(payableIndex-1), Keys.Control + 'a');
            Input(AmountPaymentInput(payableIndex-1), amount.ToString());
        }

        public void InputFirstPaymentAmount(double amount)
        {
            Input(AmoountPayablesInput, Keys.Control + 'a');
            Input(AmoountPayablesInput, amount.ToString());
        }

        public void InputSecondPaymentAmount(double amount)
        {
            Input(SecondAmoountPayablesInput, Keys.Control + 'a');
            Input(SecondAmoountPayablesInput, amount.ToString());
        }

        public double TotalHouseNetAmount()
        {

            return double.Parse(WaitElementDisplayed(TotalHouseNet).Text.TrimStart('$'));
        }
    }
}
