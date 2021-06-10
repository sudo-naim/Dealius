using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using Dealius.Utils;
using Dealius.Pages;

namespace Dealius
{
    public class DealiusPage : BasePage
    {
        #region Locators
        By LoginInput = By.Name("login");
        By PasswordInput = By.Name("password");
        By Deals = By.XPath("//a[text() = 'Deals']");
        By Accounting = By.XPath("//a[text() = 'Accounting']");
        By SignIn = By.CssSelector("button[type='submit']");
        #endregion

        public DealiusPage(IWebDriver driver) : base(driver) { }

        public void Login(string email, string password)
        {
            Input(LoginInput, email);
            Input(PasswordInput, password);
            WaitElementToBeClickable(SignIn).Click();
        }

        public void ClickDeals()
        {
            WaitElementToBeClickable(Deals).Click();
        }

        public void ClickAccounting()
        {
            click(Accounting);
        }
    }
}
