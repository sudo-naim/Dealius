using OpenQA.Selenium;
using System;
using OpenQA.Selenium.Support.UI;
using Dealius.Utils;

namespace Dealius.Pages
{
    class DealsPage
    {
        #region Locators
        By AddDealButton = By.XPath("//a[contains(text(),'ADD DEAL')]");
        By AddTenantRepDeal = By.XPath("//a[text()='TENANT REP']");
        #endregion
        IWebDriver driver;
        WebDriverWait wait;
        public DealsPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

        public void ClickAddDeal()
        {
            Method.WaitElementToBeClickable(wait, AddDealButton).Click();
        }

        public void ClickTenantRep()
        {
            Method.WaitElementToBeClickable(wait, AddTenantRepDeal).Click();
        }
        
    }
}
