using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.Pages
{
    public class MoviesPage
    {
        private readonly IWebDriver driver;

        public MoviesPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement HomeBtn => driver.FindElement(ByHomeButton);
        public IWebElement LatestMoviesBtn => driver.FindElement(ByLatestMoviesButton);
        public IWebElement WishlistBtn => driver.FindElement(ByWishlistButton);
        public IWebElement RecommendedMoviesBtn => driver.FindElement(ByRecommendedMoviesButton);
        public IWebElement MsgAlert => driver.FindElement(ByMessageAlert);
        public IWebElement PageTitle => driver.FindElement(ByPageTitle);
        public IWebElement AddToWishlistBtn => driver.FindElement(ByAddToWishlistButton);
        public IWebElement RemoveFromWishlistBtn => driver.FindElement(ByRemoveFromWishlistButton);
        public IWebElement MovieNotInWishlist => driver.FindElement(ByMovieNotInWishlist);
        public IWebElement MovieInWishlist => driver.FindElement(ByMovieInWishlist);
        public IWebElement SearchField => driver.FindElement(BySearchField);
        public IWebElement SearchBtn => driver.FindElement(BySearchBtn);
        public IWebElement SearchMode => driver.FindElement(BySearchMode);
        public IWebElement SearchMovie => driver.FindElements(BySearchMovie).Single();
        public IWebElement Genre => driver.FindElement(ByGenre);
        public IWebElement Actors => driver.FindElement(ByActors);

        public void SearchCategory(string searchCategory)
        {
            new SelectElement(SearchMode).SelectByText(searchCategory);
        }

        public IWebDriver SelectRandomMovie()
        {
            var listings = driver.FindElements(By.TagName("div"));
            Random r = new Random();
            int randomValue = r.Next(listings.Count());
            var randomMovie = listings.ElementAt(randomValue);
            driver.ScrollingToElementAndClick(By.LinkText(randomMovie.Text));
            return driver;
        }

        private By ByHomeButton => By.Id("Home");
        private By ByLatestMoviesButton => By.Id("Movies");
        private By ByWishlistButton => By.Id("WishList");
        private By ByRecommendedMoviesButton => By.Id("RecommendedMovies");
        private By ByMessageAlert => By.Id("messageAlert");
        private By ByPageTitle => By.Id("title");
        private By ByAddToWishlistButton => By.Id("AddToWishlist");
        private By ByRemoveFromWishlistButton => By.Id("RemoveFromWishlist");
        private By ByMovieNotInWishlist => By.XPath("/html/body/div[2]/div/div/div[3]/a/h2");
        private By ByMovieInWishlist => By.XPath("/html/body/div[2]/div/div/div[1]/a/h2");
        private By BySearchField => By.Id("SearchField");
        private By BySearchBtn => By.Id("SearchButton");
        private By BySearchMode => By.Id("Search");
        private By BySearchMovie => By.XPath("/html/body/div[2]/div/div/div/a/h2");
        private By ByGenre => By.Id("Genre");
        private By ByActors => By.Id("Actors");
    }
}
