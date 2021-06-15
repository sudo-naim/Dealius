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
        private By PopUpOKButton = By.XPath("//button[@class='btn btn-primary js-ok']");
        private By PopUpYesButton = By.CssSelector("button.btn.btn-primary.js-yes");
        private By PooUpCloseProcessingButton = By.XPath("//div[@id='popup-close-request']/descendant::button[contains(text(),'PROCESSING...')]");
        private By PopupCloseRequest = By.Id("popup-layout");
        #endregion
        public ClosingDealPage(IWebDriver driver) : base(driver) { }

        public void ClickSubmitButton()
        {
            click(SubmitButton);
        }
        public void ClickPopupSubmitButton()
        {
            //the line below waits for CLOSE REQUEST DETAILS pop up input field (Lease Execution Date) until it is not empty
            WaitElementDisappears(By.XPath("//input[@name='CloseDate'][contains(@class,'empty')]"));
            click(PopupSubmitButton);
        }

        public void ClickApproveButton()
        {
            click(ApproveButton);
        }

        public void WaitForProcessingButtonToDissappear()
        {
            WaitElementDisplayed(By.XPath("//button[text()='PROCESSING...']"));
            WaitElementDisappears(By.XPath("//button[text()='PROCESSING...']"));
        }

        public void WaitPopUpCloseProcessingButtonToDissapear()
        {
            WaitElementDisappears(PooUpCloseProcessingButton);
        }

        public void ClickPopUpOKButton()
        {
            click(PopUpOKButton);
        }
        
        public void ClickPopUpYesButton()
        {
            click(PopUpYesButton);
        }

        public int DealID()
        {
            var value = Find(By.Name("LinkedDealID")).GetAttribute("value");

            var id = Int32.Parse(value);
            return id;
        }

    }
}
