using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTests.Pages;
using System;
using TechTalk.SpecFlow;

namespace SeleniumTests.SpecFlow_TestScenarios.StepDefinitions
{
    [Binding]
    public class RegisterUserSteps
    {
        private IWebDriver driver;

        public RegisterUserSteps(IWebDriver driver)
        {
            this.driver = driver;
        }

        [Given(@"clicks the register button")]
        public void GivenClicksTheRegisterButton()
        {
            var loginPage = new LoginPage(driver);
            var registerPage = new RegisterUserPage(driver);
            loginPage.RegisterBtn.Click();
            Assert.That(registerPage.SaveBtn.Displayed, Is.True);
        }
        
        [When(@"the user submits the correct information")]
        public void WhenTheUserSubmitsTheCorrectInformation()
        {
            var registerPage = new RegisterUserPage(driver);
            string userName = TestHelper.Get4CharacterRandomString();
            string password = TestHelper.Get4CharacterRandomString();
            registerPage.RegisterUser(userName, password, "female", "45", "Danish");
        }
        
        [When(@"saves the registration form")]
        public void WhenSavesTheRegistrationForm()
        {
            var registerPage = new RegisterUserPage(driver);
            registerPage.SaveBtn.Click();
        }
        
        [Then(@"the user is navigated to the login page")]
        public void ThenTheUserIsNavigatedToTheLoginPage()
        {
            var loginPage = new LoginPage(driver);
            Assert.That(loginPage.LogInBtn.Displayed, Is.True);
        }
        
        [Then(@"a confirmation message ""(.*)"" appears")]
        public void ThenAConfirmationMessageAppears(string message)
        {
            var loginPage = new LoginPage(driver);
            Assert.That(loginPage.LoginMsg.Text, Is.EqualTo(message));
        }
    }
}
