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
        private static By SaveButton = By.XPath("//button[contains(text(),'SAVE')]");
        private static By PaymentMethod = By.XPath("//select[contains(@name,'[Payments][0][MethodId]')]");
        private static By ReferenceInput = By.XPath("//input[contains(@name,'[Payments][0][ReferenceNumber]')]");
        private static By PaymentRows = By.XPath("//div[@class='row d-flex payment-row']");

        #endregion
        public MakePaymentPage(IWebDriver driver) : base(driver) { }

        public void ClickSaveButton()
        {
            click(SaveButton);
        }

        public void SelectPaymentMethod(string value = "2")
        {
            WaitElementDisplayed(PaymentRows);

            var paymentMethods = driver.FindElements(PaymentMethod);
            foreach (var payMethod in paymentMethods)
            {
            var select = new SelectElement(WaitElementEnabled(payMethod)); //heree name="Payables[0][Payments][0][MethodId]
                select.SelectByValue(value);                                           //Payables[1][Payments][0][MethodId]
            }
        }

        public void InputReference(string text = "111")
        {
            WaitElementDisplayed(PaymentRows);
            var ReferenceInputs = driver.FindElements(ReferenceInput);

            foreach (var refInput in ReferenceInputs)
            {
                Input(refInput, text);
            }
            
        }

        public void InputFirstPaymentAmount(double amount)
        {
            Input(AmoountPayablesInput, Keys.Control + 'a');
            Input(AmoountPayablesInput, amount.ToString());
        }
    }
}
