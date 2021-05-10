using OpenQA.Selenium;
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
        private static By SaveButton = By.XPath("//button[contains(text(),'SAVE)]");
        #endregion
        public MakePaymentPage(IWebDriver driver) : base(driver) { }


    }
}
