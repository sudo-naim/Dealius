﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Internal;
using Xunit.Sdk;

namespace Dealius.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;
        private IJavaScriptExecutor js;
        protected WebDriverWait wait;
        protected WebDriverWait waitImmediate;
        public BasePage(IWebDriver driver) 
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            waitImmediate = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            js = driver as IJavaScriptExecutor;
        }

        public void Visit(String URL) { driver.Navigate().GoToUrl(URL); }

        public ReadOnlyCollection<IWebElement> FindElements(By locator)
        {
            return driver.FindElements(locator);
        }

        public IWebElement Find(By locator) { return driver.FindElement(locator); }

        public IWebElement Find(IWebElement parent, By locator)
        {
            return WaitForElement(parent.FindElement(locator));
        }

        public IWebElement Find(By parent, By Locator)
        {
            return WaitForElement(Find(parent).FindElement(Locator));
        }

        public void click(By locator) { WaitElementToBeClickable(locator).Click(); }
        public void click(IWebElement element) { WaitElementToBeClickable(element).Click(); }
        public void click(IWebElement element, By locator) { WaitElementRelative(element, locator).Click(); }

        //public void click(IWebElement element) { WaitElementToBeClickable(element).Click();}

        public void Input(By locator, string text)
        {
            WaitForElement(locator).SendKeys(text);
        }

        public void Input(IWebElement el, string text)
        {
            WaitForElement(el).SendKeys(text);
        }

        public double GetElementValueDouble(By locator)
        {
            IWebElement e = WaitForElement(locator);
            double value = string.IsNullOrEmpty(e.GetAttribute("value")) ? 0 : Double.Parse(e.GetAttribute("value"));
            return value;
        }

        public double GetElementValueDouble(IWebElement element)
        {
            IWebElement e = WaitElementEnabled(element);
            double value = string.IsNullOrEmpty(e.GetAttribute("value")) ? 0 : Double.Parse(e.GetAttribute("value"));
            return value;
        }

        public  void WaitUrlContains(string text)
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

        public IWebElement WaitForElement(By locator)
        {
            try
            {
                IWebElement element = wait.Until(drv =>
                {
                    try
                    {
                        var el = Find(locator);
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

        public IWebElement WaitForElement(IWebElement elem)
        {
            try
            {
                IWebElement element = wait.Until(drv =>
                {
                    try
                    {
                        var el = elem;
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
                throw new WebDriverTimeoutException($"Timeout while waiting for {elem}");
            }
        }

        public IWebElement WaitElementDisplayed(By locator)
        {
            try
            {
                var element = wait.Until(drv =>
                {
                    try
                    {
                        var el = Find(locator);
                        return el.Displayed ? el : null;
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
                throw new WebDriverTimeoutException($"Timeout while waiting for {locator} to be displayed");
            }
        }

        public IWebElement WaitElementDisplayedImmediate(By locator)
        {
            try
            {

                var element = waitImmediate.Until(drv =>
                {
                    try
                    {
                        var el = Find(locator);
                        return el.Displayed ? el : null;
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
                throw new WebDriverTimeoutException($"Timeout while waiting for {locator} to be displayed");
            }
        }

        public void WaitElementDisappears(By locator)
        {
            try
            {
                wait.Until(drv =>
                {
                    try
                    {
                        var el = Find(locator);
                        return !el.Displayed;
                    }
                    catch (Exception e)
                    {
                        if(e is NoSuchElementException)
                            return true;
                        if(e is StaleElementReferenceException)
                            return false;
                        throw;
                    }
                });
            }
            catch (Exception e)
            {
                throw new WebDriverTimeoutException($"Timeout while waiting for {locator} to be NOT displayed");
            }
        }

        public IWebElement WaitElementRelative(IWebElement rel, By locator)
        {
            try
            {
                IWebElement element = wait.Until(drv =>
                {
                    try
                    {

                        var el = rel.FindElement(locator);
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

        public void WaitElementDisappearsRelative(By parentLocator, By childLocator)
        {
            try
            {
                wait.Until(drv =>
                {
                    try
                    {
                        var el = Find(parentLocator).FindElement(childLocator);
                        return !el.Displayed;
                    }
                    catch (Exception e)
                    {
                        if (e is NoSuchElementException)
                            return true;
                        if (e is StaleElementReferenceException)
                            return false;
                        throw;
                    }
                });
            }
            catch (Exception e)
            {
                throw new WebDriverTimeoutException($"Timeout while waiting for {parentLocator} {childLocator} to be NOT displayed");
            }
        }

        public void WaitElementDisappears(IWebElement element)
        {
            try
            {
                wait.Until(drv =>
                {
                    try
                    {
                        return !element.Displayed;
                    }
                    catch (Exception e)
                    {
                        if (e is NoSuchElementException)
                            return true;
                        if (e is StaleElementReferenceException)
                            return false;
                        throw;
                    }
                });
            }
            catch (Exception e)
            {
                throw new WebDriverTimeoutException($"Timeout while waiting for {element} to be NOT displayed");
            }
        }

        public  IWebElement WaitElementToBeClickable(By locator)
        {
            try
            {
                IWebElement element = wait.Until(drv =>
                {
                    try
                    {
                        var el = Find(locator);
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

        public IWebElement WaitElementClick(By locator)
        {
            IWebElement element = wait.Until(drv =>
            {
                try
                {
                    var el = drv.FindElement(locator);
                    if (el.Displayed && el.Enabled)
                    {
                        el.Click();
                        return el;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    if (e is NoSuchElementException || e is StaleElementReferenceException)
                        return null;
                    if (e.Message.Contains("is not clickable"))
                        return null;
                    throw new WebDriverTimeoutException($"Timeout while waiting for {locator}");
                }
            });
            return element;
        }

        public IWebElement WaitElementToBeClickable(IWebElement element)
        {
            try
            {
                IWebElement e = wait.Until(drv =>
                {
                    try
                    {
                        return (element.Displayed && element.Enabled) ? element : null;
                    }
                    catch (Exception exception)
                    {
                        if (exception is NoSuchElementException || exception is StaleElementReferenceException)
                            return null;
                        throw;
                    }
                });
                return e;
            }
            catch (Exception exception)
            {
                throw new WebDriverTimeoutException($"Timeout while waiting for {element.TagName} {element.Text}");
            }
        }

        public IWebElement WaitElementAndClick(By locator)
        {
            try
            {
                IWebElement element = wait.Until(drv =>
                {
                    try
                    {
                        var el = Find(locator);
                        if (el.Displayed && el.Enabled)
                        {
                            el.Click();
                            return el;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    catch (Exception e)
                    {
                        if (e is NoSuchElementException || e is StaleElementReferenceException || e is ElementClickInterceptedException)
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

        public  IWebElement WaitElementEnabled(By locator)
        {

            try
            {
                IWebElement element = wait.Until(drv =>
                {
                    try
                    {
                        var el = Find(locator);
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
            catch (Exception e)
            {
                throw new WebDriverTimeoutException($"Timeout while waiting for {locator}");
            }
        }

        public IWebElement WaitElementEnabled(IWebElement el)
        {

            try
            {
                IWebElement element = wait.Until(drv =>
                {
                    try
                    {
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
            catch (Exception e)
            {
                throw new WebDriverTimeoutException($"Timeout while waiting for {el}");
            }
        }

        public ReadOnlyCollection<IWebElement> FindAllTableBodyRows(By tableBodyLocator)
        {
            return Find(tableBodyLocator).FindElements(By.CssSelector("tr"));
        }

        public bool AllRelativeElementsDisplayed(By parentLocator, By locator)
        {
            try
            {
                bool elementsDisplayed = wait.Until(drv =>
                {
                    try
                    {
                        var el = Find(parentLocator).FindElements(locator);
                        return el.All(elem => elem.Displayed && elem.Enabled);
                    }
                    catch (Exception e)
                    {
                        if (e is NoSuchElementException || e is StaleElementReferenceException)
                            return false;
                        throw;
                    }
                });
                return elementsDisplayed;
            }
            catch (Exception e)
            {
                throw new WebDriverTimeoutException($"Timeout while waiting for {locator}");
            }
        }

        public void WaitDocumentReadyState()
        {
            wait.Until(driver1 => js
                .ExecuteScript("return document.readyState")
                .Equals("complete"));
        }

        public void ScrollToElement(By locator)
        {
            try
            {
                const string script = "arguments[0].scrollIntoView({block: 'center', inline: 'center'})";
                IJavaScriptExecutor js = driver as IJavaScriptExecutor;
                js.ExecuteScript(script, Find(locator));
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }
        /*public string GetPseudoElementCSSContentValue()
        {
            const string script = "return window.getComputedStyle(document.querySelector('input[name=\"AmortizeFreeRent\"] + span'),':before').getPropertyValue('content');";
            var result = (string)js
                .ExecuteScript(script);
            return result;
        } */
        
    }
}
