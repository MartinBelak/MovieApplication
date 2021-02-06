using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace SeleniumTests
{
    public static class SeleniumHelper
    {       
        public static IWebDriver WaitUntilElementVisible(this IWebDriver driver, By by, int timeout = 30)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            IWebElement element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible((by)));
            return driver;
        }

        public static IWebDriver ClearAndSendkeys(this IWebDriver driver, By by, string value)
        {
            var element = driver.FindElement(by);
            element.Clear();
            element.SendKeys(value);
            return driver;
        }

        public static IWebDriver WaitAndSendKeys(this IWebDriver driver, By by, string value, int timeout = 30)
        {
            WaitUntilElementVisible(driver, by, timeout);
            driver.FindElement(by).SendKeys(value);
            return driver;
        }

        public static IWebDriver WaitUntilPageLoadedCompletely(this IWebDriver driver)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("$(document).ready(function() { return true; });");
            return driver;
        }

        public static IWebDriver LoginUser(this IWebDriver driver, string username, string password)
        {
            driver.ClearAndSendkeys(By.Id("UserName"), username);
            driver.ClearAndSendkeys(By.Id("Password"), password);
            driver.FindElement(By.Id("SubmitButton")).Click();
            driver.WaitUntilElementVisible(By.Id("LogoutButton"));
            return driver;
        }      

        public static IWebDriver ScrollingToElementAndClick(this IWebDriver driver, By by)
        {
            WaitUntilElementVisible(driver, by);
            var element = driver.FindElement(by);
            Actions actions = new Actions(driver);
            actions.MoveToElement(element);
            actions.Perform();
            element.Click();
            return driver;
        }
    }
}
