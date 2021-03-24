using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.Pages
{
    class WishlistPage
    {
        private readonly IWebDriver driver;

        public WishlistPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement HomeBtn => driver.FindElement(ByHomeButton);
        public IWebElement LatestMoviesBtn => driver.FindElement(ByLatestMoviesButton);
        public IWebElement WishlistBtn => driver.FindElement(ByWishlistButton);
        public IWebElement RecommendedMoviesBtn => driver.FindElement(ByRecommendedMoviesButton);
        public IWebElement LoginMsg => driver.FindElement(ByLoginMessage);
        public IWebElement PageTitle => driver.FindElement(ByPageTitle);
        public IWebElement RemoveFromWishlistBtn => driver.FindElement(ByRemoveFromWishlistButton);


        private By ByHomeButton => By.Id("Home");
        private By ByLatestMoviesButton => By.Id("Movies");
        private By ByWishlistButton => By.Id("WishList");
        private By ByRecommendedMoviesButton => By.Id("RecommendedMovies");
        private By ByLoginMessage => By.Id("loginMsg");
        private By ByPageTitle => By.Id("title");
        private By ByRemoveFromWishlistButton => By.Id("RemoveFromWishlist");
    }
}
