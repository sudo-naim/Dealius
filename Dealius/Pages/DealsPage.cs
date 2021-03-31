using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;
using Dealius.Utils;

namespace Dealius.Pages
{
    class DealsPage : BasePage
    {
        #region Locators
        By AddDealButton = By.XPath("//div/a[contains(text(),'ADD DEAL')]");
        By AddTenantRepDeal(string RepType) => By.XPath($"//li/a[text()='{RepType} REP']");
        #endregion
        public DealsPage(IWebDriver driver) : base(driver) { }

        public void ClickAddDeal()
        {
            WaitElementToBeClickable(AddDealButton).Click();
        }

        public void ClickRepType(string repType)
        {
            WaitElementToBeClickable(AddTenantRepDeal(repType.ToUpper())).Click();
        }
        
    }
}
