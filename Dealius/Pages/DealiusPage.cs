using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using Dealius.Utils;
namespace Dealius
{
    public class DealiusPage
    {
        #region Locators
        By LoginInput = By.Name("login");
        By PasswordInput = By.Name("password");
        By Deals = By.XPath("//a[text() = 'Deals']");
        By SignIn = By.CssSelector("button[type='submit']");
        #endregion

        IWebDriver driver;
        private WebDriverWait wait;
        public DealiusPage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

        public void Login()
        {
            Method.Input(wait, LoginInput, "user-officeadmin@dealius.com");
            Method.Input(wait, PasswordInput, "123");
            Method.WaitElementToBeClickable(wait, SignIn).Click();
        }

        public void ClickDeals()
        {
            Method.WaitElementToBeClickable(wait, Deals).Click();

        }
    }
}
