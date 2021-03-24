using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTests.Configuration;
using SeleniumTests.Pages;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SeleniumTests.SpecFlow_TestScenarios.StepDefinitions
{
    [Binding]
    public class AddMovieToWishlistSteps
    {
        private IWebDriver driver;
        protected LogInData userLogInData { get; set; }

        public AddMovieToWishlistSteps(IWebDriver driver)
        {
            this.driver = driver;
        }

        [Given(@"the user is logged in his account")]
        public void GivenTheUserIsLoggedInHisAccount(Table table)
        {
            var loginPage = new LoginPage(driver);
            var homePage = new HomePage(driver);
            userLogInData = table.CreateInstance<LogInData>();
            loginPage.LogInUser(userLogInData.UserName, userLogInData.Password);
            Assert.That(homePage.LogoutBtn.Displayed, Is.True);
        }
        
        [Given(@"the user navigates to the Latest Movies page")]
        public void GivenTheUserNavigatesToTheLatestMoviesPage()
        {
            var homePage = new HomePage(driver);
            var moviesPage = new MoviesPage(driver);
            homePage.LatestMoviesBtn.Click();
            Assert.That(moviesPage.PageTitle.Text, Is.EqualTo("All Movies"));
        }
        
        [When(@"the user selects a movie which is not in his wishlist already")]
        public void WhenTheUserSelectsAMovieWhichIsNotInHisWishlistAlready()
        {
            var moviesPage = new MoviesPage(driver);
            moviesPage.MovieNotInWishlist.Click();
            Assert.That(moviesPage.AddToWishlistBtn.Displayed, Is.True);
        }
        
        [When(@"clicks the Add to Wishlist button")]
        public void WhenClicksTheAddToWishlistButton()
        {
            var moviesPage = new MoviesPage(driver);
            moviesPage.AddToWishlistBtn.Click();
        }
        
        [When(@"the user selects a movie which is in his wishlist already")]
        public void WhenTheUserSelectsAMovieWhichIsInHisWishlistAlready()
            {
            var moviesPage = new MoviesPage(driver);
            moviesPage.MovieInWishlist.Click();
            Assert.That(moviesPage.AddToWishlistBtn.Displayed, Is.True);
        }

        [Then(@"a confirmation message ""(.*)"" should appear")]
        public void ThenAConfirmationMessageShouldAppear(string message)
        {
            var moviesPage = new MoviesPage(driver);           
            Assert.That(moviesPage.MsgAlert.Text, Is.EqualTo(message));           
        }

        [Then(@"the movie can be seen in his wishlist")]
        public void ThenTheMovieCanBeSeenInHisWishlist()
        {
            var moviesPage = new MoviesPage(driver);
            string movieTitle = "Cam";
            moviesPage.WishlistBtn.Click();
            var movie = driver.FindElement(By.ClassName("row"))
                .FindElements(By.TagName("h2"))
                .FirstOrDefault(e => e.Text.Contains(movieTitle));

            Assert.That(movie.Text.Contains(movieTitle), Is.True);

            //clean up - remove the movie from the wishlist
            movie.Click();
            moviesPage.RemoveFromWishlistBtn.Click();
        }
     
        [Then(@"an error message ""(.*)"" appears")]
        public void ThenAnErrorMessageAppears(string message)
        {
            var moviesPage = new MoviesPage(driver);
            Assert.That(moviesPage.MsgAlert.Text, Is.EqualTo(message));
        }

    }
}
