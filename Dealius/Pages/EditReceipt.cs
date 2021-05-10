using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealius.Pages
{
    class EditReceipt : BasePage
    {
        #region locators
        private static By AmountInput = By.Name("Amount");
        private static By PaidDateInput = By.Name("PaidDate");
        private static By ReferenceInput = By.Id("Reference");
        private static By SavePayButton = By.XPath("//button[contains(text(),'SAVE & PAY')]");
        private static By SaveButton = By.XPath("//button[contains(text(),'SAVE)]");
        #endregion
        public EditReceipt(IWebDriver driver) : base(driver) { }

        public void ClickSavePayButton()
        {
            click(SavePayButton);
        }

        public void ClickSaveButton()
        {
            click(SaveButton);
        }

    }
}
