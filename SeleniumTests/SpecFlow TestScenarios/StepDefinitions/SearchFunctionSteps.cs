using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumTests.Pages;
using System;
using TechTalk.SpecFlow;

namespace SeleniumTests.SpecFlow_TestScenarios.StepDefinitions
{
    [Binding]
    public class SearchFunctionSteps
    {
        private IWebDriver driver;

        public SearchFunctionSteps(IWebDriver driver)
        {
            this.driver = driver;
        }

        [When(@"the user enters (.*) in the search field")]
        public void WhenTheUserEntersInTheSearchField(string searchString)
        {
            var moviesPage = new MoviesPage(driver);
            moviesPage.SearchField.SendKeys(searchString);
        }
        
        [When(@"selects (.*) from a dropdown menu")]
        public void WhenSelectsFromADropdownMenu(string searchCategory)
        {
            var moviesPage = new MoviesPage(driver);
            moviesPage.SearchCategory(searchCategory);
        }
        
        [When(@"presses the Search! button")]
        public void WhenPressesTheSearchButton()
        {
            var moviesPage = new MoviesPage(driver);
            moviesPage.SearchBtn.Click();
        }
        
        [Then(@"a movie (.*) should be displayed")]
        public void ThenAMovieShouldBeDisplayed(string movieTitle)
        {
            var moviesPage = new MoviesPage(driver);           
            Assert.That(moviesPage.SearchMovie.Text.Contains(movieTitle), Is.True);
        }     

        [Then(@"movies with the (.*) (.*) should be displayed")]
        public void ThenMoviesWithTheShouldBeDisplayed(string category, string categoryValue)
        {
            var moviesPage = new MoviesPage(driver);           
            if (category == "genre")
            {
                moviesPage.SelectRandomMovie();
                Assert.That(moviesPage.Genre.Text.Contains(categoryValue), Is.True);
            }
            else
            {
                moviesPage.MovieInWishlist.Click();
                Assert.That(moviesPage.Actors.Text.Contains(categoryValue), Is.True);
            }
        }
    }
}
