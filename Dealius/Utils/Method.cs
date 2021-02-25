using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace Dealius.Utils
{
    static class Method
    {
        public static void Input(WebDriverWait wait, By selector, string text)
        {
            var element = WaitElement(wait, selector);
            element.SendKeys(Keys.Control + 'a');
            element.SendKeys(Keys.Backspace);
            element.SendKeys(text);
        }

        public static IWebElement WaitElement(DefaultWait<IWebDriver> wait, By locator)
        {
            try
            {
                IWebElement element = wait.Until(drv =>
                {
                    try
                    {
                        var el = drv.FindElement(locator);
                        //Scripts.ScrollToElement(drv, el);
                        return (el.Displayed && el.Enabled/* && Scripts.isElementClickable(drv, el)*/) ? el : null;
                    }
                    catch (Exception e)
                    {
                        if (e is NoSuchElementException || e is StaleElementReferenceException)
                            return null;
                        throw;
                    }
                });
                return element;
            }
            catch (Exception e)
            {
                throw new WebDriverTimeoutException($"Timeout while waiting for {locator}");
            }
        }

        public static IWebElement WaitElementToBeClickable(WebDriverWait wait, By locator)
        {
            try
            {
                IWebElement element = wait.Until(drv =>
                {
                    try 
                    { 
                        var el = drv.FindElement(locator);
                        return (el.Displayed && el.Enabled) ? el : null;
                    }
                    catch (Exception e)
                    {
                        if (e is NoSuchElementException || e is StaleElementReferenceException)
                            return null;
                        throw;
                    }
                });
                return element;
            }
            catch (Exception e)
            {
                throw new WebDriverTimeoutException($"Timeout while waiting for {locator}");
            }
        }

        //public static IList<IWebElement> WaitElementsToBeClickable(WebDriverWait wait, By locator)
        //{
        //    try
        //    {
        //        IWebElement element = wait.Until(drv =>
        //        {
        //            try
        //            {
        //                IList<IWebElement> d = drv.FindElements(locator);
        //                return (d.Count>0 ) ? d : null;
        //            }
        //            catch (Exception e)
        //            {
        //                if (e is NoSuchElementException || e is StaleElementReferenceException)
        //                    return null;
        //                throw;
        //            }
        //        },return d);
        //        //return d;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new WebDriverTimeoutException($"Timeout while waiting for {locator}");
        //    }
        //}
    }
}
