using BoDi;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTests.Configuration;
using SeleniumTests.Configuration.SpecFlow_Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SeleniumTests.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        protected IWebDriver webDriver;
        protected string Url = "http://localhost:50260/"; //http://localhost:50069/ , http://localhost:50260/
        private Driver driver;
        private readonly IObjectContainer objectContainer;
        protected string userName;
        protected string password;

        public Hooks(IObjectContainer objectContainer, Driver driver)
        {
            this.objectContainer = objectContainer;
            this.driver = driver;
        }

        /// <summary>
        /// logic that has to run before executing each scenario
        /// </summary>
        [BeforeScenario]
        public void BeforeScenario()
        {
            webDriver = driver.Init();
            objectContainer.RegisterInstanceAs<IWebDriver>(webDriver);
            webDriver.Manage().Window.Maximize();
            webDriver.Navigate().GoToUrl(Url);
        }

        /// <summary>
        /// logic to execute after each scenario
        /// </summary>
        [AfterScenario]
        public void AfterScenario()
        {
            driver.CleanUp();
        }
    }
}
