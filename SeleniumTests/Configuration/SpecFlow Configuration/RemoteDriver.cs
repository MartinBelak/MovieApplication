﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.Configuration.SpecFlow_Configuration
{
    public class RemoteDriver
    {
        public IWebDriver GetDriverInstance()
        {
            return GetChromeWithCapabilities();
        }

        private IWebDriver GetChromeWithCapabilities()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddAdditionalCapability(CapabilityType.Version, "latest", true);
            options.AddAdditionalCapability(CapabilityType.Platform, "WIN10", true);
            options.AddAdditionalCapability("name", TestContext.CurrentContext.Test.Name, true);
            options.AddAdditionalCapability("screenshot", true, true);
            options.AddAdditionalCapability("screen-resolution", "1920x1080", true);
            options.AddAdditionalCapability("maxduration", "900", true);
            options.AddAdditionalCapability("key", "40b8966a5560cc990cf5db5abca012fd", true);
            options.AddAdditionalCapability("secret", "50b5af130420f08f89662efa10919a23", true);

            // http://localhost:4445/ , https://hub.testingbot.com/wd/hub
            var driver = new RemoteWebDriver(new Uri("http://localhost:4445/wd/hub"), options.ToCapabilities(), TimeSpan.FromSeconds(600));
            Console.WriteLine("TestingBotSessionID=" + ((RemoteWebDriver)driver).SessionId.ToString());
            return driver;
        }

        private IWebDriver GetFirefoxWithCapabilities()
        {
            var capabilities = new FirefoxOptions();
            capabilities.AddAdditionalCapability(CapabilityType.Version, "latest", true);
            capabilities.AddAdditionalCapability(CapabilityType.Platform, "WIN10", true);
            capabilities.AddAdditionalCapability("name", TestContext.CurrentContext.Test.Name, true);
            capabilities.AddAdditionalCapability("screenshot", true, true);
            capabilities.AddAdditionalCapability("screen-resolution", "1920x1080", true);
            capabilities.AddAdditionalCapability("maxduration", "900", true);
            capabilities.AddAdditionalCapability("key", "40b8966a5560cc990cf5db5abca012fd", true);
            capabilities.AddAdditionalCapability("secret", "50b5af130420f08f89662efa10919a23", true);

            var driver = new RemoteWebDriver(new Uri("http://hub.testingbot.com/wd/hub"), capabilities.ToCapabilities(),
            TimeSpan.FromSeconds(1000));
            Console.WriteLine("TestingBotSessionID=" + ((RemoteWebDriver)driver).SessionId.ToString());
            return driver;
        }
    }
}
