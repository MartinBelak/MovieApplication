﻿using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SeleniumTests.Configuration
{
    public class BaseTestFixture : RemoteSetup
    {
        protected IWebDriver driver;
        protected string url = "http://localhost:50260/"; //http://localhost:50069/ , http://localhost:50260/
        protected string userName;
        protected string password;

        [SetUp]
        public void TestSetup()
        {
            driver = new ChromeDriver(); //for local testing
            //driver = GetDriverInstance(); //for remote TestingBot
            driver.Manage().Window.Maximize();
            driver.Url = url;
        }

        [TearDown]
        public void TestCleanUp()
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
