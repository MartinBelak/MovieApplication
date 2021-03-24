using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTests.Configuration;
using SeleniumTests.Pages;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace SeleniumTests.SpecFlow_TestScenarios.StepDefinitions
{
    [Binding]
    public class RemoveMovieFromWishlistSteps
    {
        private IWebDriver driver;      

        public RemoveMovieFromWishlistSteps(IWebDriver driver)
        {
            this.driver = driver;
        }

        [Given(@"the user navigates to the Wishlist page")]
        public void GivenTheUserNavigatesToTheWishlistPage()
        {
            var homePage = new HomePage(driver);
            var wishlistPage = new WishlistPage(driver);
            homePage.WishlistBtn.Click();
            Assert.That(wishlistPage.PageTitle.Text.Contains("WishList"), Is.True);
        }

        [When(@"the user selects a movie (.*) from his wishlist")]
        public void WhenTheUserSelectsAMovieFromHisWishlist(string movieTitle)
        {          
            var movie = driver.FindElement(By.ClassName("row"))
                .FindElements(By.TagName("h2"))
                .FirstOrDefault(e => e.Text.Contains(movieTitle));
            movie.Click();           
        }      
        
        [When(@"clicks the Remove from Wishlist button")]
        public void WhenClicksTheRemoveFromWishlistButton()
        {
            var wishlistPage = new WishlistPage(driver);
            wishlistPage.RemoveFromWishlistBtn.Click();
        }

        [Then(@"the movie (.*) is removed from his wishlist")]
        public void ThenTheMovieIsRemovedFromHisWishlist(string movieTitle)
        {
            var wishlistPage = new WishlistPage(driver);
            wishlistPage.WishlistBtn.Click();
            Assert.That(driver.PageSource.Contains(movieTitle), Is.False);
        }       
    }
}
