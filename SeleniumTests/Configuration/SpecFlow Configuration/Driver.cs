using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SeleniumTests.Configuration.SpecFlow_Configuration
{
    public class Driver : RemoteDriver
    {
        protected IWebDriver driver;       
        private ScenarioContext context;

        public Driver(ScenarioContext context)
        {
            this.context = context;
        }

        public IWebDriver Init()
        {
            driver = new ChromeDriver();
            //remote TestingBot driver
            //driver = GetDriverInstance();
            return driver;
        }

        public void CleanUp()
        {
            if (driver is ChromeDriver)
            {
                driver.Close();

            }
            else
            {
                try
                {
                    // Logs the result to TestingBot
                    var passed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
                    ((IJavaScriptExecutor)driver).ExecuteScript("tb:test-result=" + (passed ? "passed" : "failed"));
                }
                finally
                {
                    // Terminates the remote webdriver session
                    driver.Quit();
                }
            }
        }
    }
}
