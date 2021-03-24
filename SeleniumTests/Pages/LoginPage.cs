using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement UserName => driver.FindElement(ByUserName);
        public IWebElement Password => driver.FindElement(ByPassword);
        public IWebElement LogInBtn => driver.FindElement(ByLoginButton);
        public IWebElement LoginMsg => driver.FindElement(ByLoginMessage);
        public IWebElement RegisterBtn => driver.FindElement(ByRegisterButton);

        public void LogInUser(string userName, string password)
        {
            UserName.SendKeys(userName);
            Password.SendKeys(password);
            LogInBtn.Click();
        }
     
        // Private Element Identifiers
        private By ByUserName => By.Id("UserName");
        private By ByPassword => By.Id("Password");
        private By ByLoginButton => By.Id("SubmitButton");
        private By ByLoginMessage => By.Id("LoginMessage");
        private By ByRegisterButton => By.Id("RegisterButton");
    }
}
