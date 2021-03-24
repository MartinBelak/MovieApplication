using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTests.Configuration;
using SeleniumTests.Pages;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SeleniumTests.SpecFlow_TestScenarios.StepDefinitions
{
    [Binding]
    public class LoginUserSteps
    {
        private IWebDriver driver;
        protected LogInData userLogInData { get; set; }

        public LoginUserSteps(IWebDriver driver)
        {
            this.driver = driver;
        }

        [Given(@"the user navigates to the webpage")]
        public void GivenTheUserNavigatesToTheWebpage()
        {
            var loginPage = new LoginPage(driver);
            Assert.That(loginPage.LogInBtn.Displayed, Is.True);
        }
        
        [When(@"the user enters correct login information")]
        public void WhenTheUserEntersCorrectLoginInformation(Table table)
        {
            var loginPage = new LoginPage(driver);
            userLogInData = table.CreateInstance<LogInData>();
            loginPage.LogInUser(userLogInData.UserName, userLogInData.Password);
        }
        
        [When(@"the user enters incorrect login information")]
        public void WhenTheUserEntersIncorrectLoginInformation(Table table)
        {
            var loginPage = new LoginPage(driver);
            userLogInData = table.CreateInstance<LogInData>();
            loginPage.LogInUser(userLogInData.UserName, userLogInData.Password);
        }
        
        [Then(@"the user should be successfully logged in and navigated to main page")]
        public void ThenTheUserShouldBeSuccessfullyLoggedInAndNavigatedToMainPage()
        {
            var homePage = new HomePage(driver);
            Assert.That(homePage.LoginMsg.Displayed, Is.True);
            Assert.That(homePage.LogoutBtn.Displayed, Is.True);
        }
        
        [Then(@"an error message ""(.*)"" should appear")]
        public void ThenAnErrorMessageShouldAppear(string errorMessage)
        {
            var loginPage = new LoginPage(driver);
            Assert.That(loginPage.LoginMsg.Text, Is.EqualTo(errorMessage));
        }
    }
}
