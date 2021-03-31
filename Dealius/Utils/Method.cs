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
            var element = WaitForElement(wait, selector);
            element.SendKeys(Keys.Control + 'a');
            element.SendKeys(Keys.Backspace);
            element.SendKeys(text);
        }

        public static double GetElementValueDouble(WebDriverWait wait,By locator)
        {
            IWebElement e = WaitForElement(wait, locator);
            double value = string.IsNullOrEmpty(e.GetAttribute("value")) ? 0 : double.Parse(e.GetAttribute("value"));
            return value;
        }

        public static void WaitUrlContains(WebDriverWait wait, string text)
        {
            try
            {
                
                wait.Until(drv => drv.Url.Contains($"{text}"));
            }
            catch
            {
                throw new ArgumentException($"string:'{text}' not found in the URL");
            }
        }

        public static IWebElement WaitForElement(DefaultWait<IWebDriver> wait, By locator)
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
            catch (Exception)
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
            catch (Exception )
            {
                throw new WebDriverTimeoutException($"Timeout while waiting for {locator}");
            }
        }

        public static IWebElement WaitElementEnabled(DefaultWait<IWebDriver> wait, By locator)
        {

            try
            {
                IWebElement element = wait.Until(drv =>
                {
                    try
                    {
                        var el = drv.FindElement(locator);
                        return el.Enabled ? el : null;
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
            catch (Exception)
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

        //public String GetElementValueByCssSelector(string selector)
        //{
        //    IJavaScriptExecutor e = (IJavaScriptExecutor)driver;
        //    return (String)e.ExecuteScript(String.Format($"return $(\"{selector}\").val();"));
        //  //return (String)e.ExecuteScript(String.Format("return $(\"input[name = 'Rents[0][Months]']\").val();", webElement.GetAttribute("name")));

        //}
    }
}
