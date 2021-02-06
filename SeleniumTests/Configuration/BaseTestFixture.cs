using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.Configuration
{
    public class BaseTestFixture
    {
        protected IWebDriver driver;
        protected string url = "http://localhost:50069/";
        protected string userName;
        protected string password;

        [SetUp]
        public void TestSetup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = url;
        }

        [TearDown]
        public void TestCleanUp()
        {
            //testing bot
            //var passed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
            //((IJavaScriptExecutor)driver).ExecuteScript("tb:test-result=" + (passed ? "passed" : "failed"));

            driver.Close();
        }   
    }
}
