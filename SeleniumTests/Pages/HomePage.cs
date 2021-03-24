using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.Pages
{
    public class HomePage
    {
        private readonly IWebDriver driver;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement LogoutBtn => driver.FindElement(ByLogoutButton);
        public IWebElement HomeBtn => driver.FindElement(ByHomeButton);
        public IWebElement LatestMoviesBtn => driver.FindElement(ByLatestMoviesButton);
        public IWebElement WishlistBtn => driver.FindElement(ByWishlistButton);
        public IWebElement RecommendedMoviesBtn => driver.FindElement(ByRecommendedMoviesButton);
        public IWebElement LoginMsg => driver.FindElement(ByLoginMessage);

        private By ByLogoutButton => By.Id("LogoutButton");
        private By ByHomeButton => By.Id("Home");
        private By ByLatestMoviesButton => By.Id("Movies");
        private By ByWishlistButton => By.Id("WishList");
        private By ByRecommendedMoviesButton => By.Id("RecommendedMovies");
        private By ByLoginMessage => By.Id("loginMsg");
        

    }
}
