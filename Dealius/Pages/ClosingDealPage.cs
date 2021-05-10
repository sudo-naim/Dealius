using OpenQA.Selenium;
using System;

namespace Dealius.Pages
{
    class ClosingDealPage : BasePage
    {
        #region Locators
        private static By SubmitButton = By.XPath("//button[contains(@class,'js-close-action')][contains(text(),'SUBMIT')]");
        private By PopupSubmitButton = By.XPath("//div[@id='popup-close-request']/descendant::button[contains(text(),'SUBMIT')]");
        private By ApproveButton = By.XPath("//button[contains(text(),'APPROVE')]");
        private By PopUpOKButton = By.CssSelector("button.btn.btn-primary.js-ok");
        #endregion
        public ClosingDealPage(IWebDriver driver) : base(driver) { }

        public void ClickSubmitButton()
        {
            click(SubmitButton);
        }
        public void ClickPopupSubmitButton()
        {
            click(PopupSubmitButton);
        }

        public void ClickApproveButton()
        {
            click(ApproveButton);
            WaitElementDisplayed(By.XPath("//button[text()='PROCESSING...']"));
            WaitElementDisappears(By.XPath("//button[text()='PROCESSING...']"));
        }

        public void ClickPopUpOKButton()
        {
            click(PopUpOKButton);
        }

        public int DealID()
        {
            var value = Find(By.Name("LinkedDealID")).GetAttribute("value");

            var id = Int32.Parse(value);
            return id;
        }

    }
}
