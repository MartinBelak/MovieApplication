using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumTests.Configuration;
//using SeleniumTests.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests
{
    public class SeleniumTestMethods : BaseTestFixture
    {		
        [Test]
        public void TestSuccesfulLogin()
        {
            // Fill in Username
            var userName = driver.FindElement(By.Id("UserName"));
            userName.Clear();
            userName.SendKeys("Noro");

            // Fill in Password
            var password = driver.FindElement(By.Id("Password"));
            password.Clear();
            password.SendKeys("Noro");

            driver.FindElement(By.Id("SubmitButton")).Click();

            // Check for logout button 
            Assert.That(driver.FindElement(By.Id("LogoutButton")).Displayed, Is.True);
        }  
        
        [Test]
        public void TestUserRegistration()
        {
            // Generate a random string for username and password
            string Username = TestHelper.Get4CharacterRandomString();
            string Passworld = TestHelper.Get4CharacterRandomString();

            driver.FindElement(By.Id("RegisterButton")).Click();

            // Fill in registration form
            driver.ClearAndSendkeys(By.Id("UserName"), Username);
            driver.ClearAndSendkeys(By.Id("PasswordHash"), Passworld);
            driver.FindElement(By.Id("Gender")).Click();
            driver.ClearAndSendkeys(By.Id("Age"), "32");
            driver.ClearAndSendkeys(By.Id("Nationality"), "Danish");
            driver.FindElement(By.Id("SaveButton")).Click();

            // Wait for login button to be visible
            driver.WaitUntilElementVisible(By.Id("SubmitButton"));

            //check for succesfull register message
            Assert.That(driver.FindElement(By.Id("LoginMessage")).Displayed, Is.True);
        }

        [Test]
        public void AddMovieToWishlist()
        {
            string movieTitle = "Cam";

            // Login user with movies in Wishlist
            driver.ClearAndSendkeys(By.Id("UserName"), "Noro");
            driver.ClearAndSendkeys(By.Id("Password"), "Noro");
            driver.FindElement(By.Id("SubmitButton")).Click();

            // Wait until logout button is visible 
            driver.WaitUntilElementVisible(By.Id("LogoutButton"));        
            
            // Navigate to all movies and add a movie to the wishlist
            driver.FindElement(By.Id("Movies")).Click();
            driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[3]/a/h2")).Click();
            driver.WaitUntilPageLoadedCompletely();
            driver.FindElement(By.Id("AddToWishlist")).Click();

            // Checks for the added movie in the wishlist
            driver.FindElement(By.Id("WishList")).Click();
            var movie = driver.FindElement(By.ClassName("row"))
                .FindElements(By.TagName("h2"))
                .FirstOrDefault(e => e.Text.Contains(movieTitle));

            Assert.That(movie.Text.Contains(movieTitle), Is.True);

            // Test Cleanup - delete the movie from wishlist
            movie.Click();
            driver.WaitUntilElementVisible(By.Id("RemoveFromWishlist"));
            driver.FindElement(By.Id("RemoveFromWishlist")).Click();
        }

        [Test]
        public void RemoveMovieFromWishlist()
        {
            string movieTitle = "Stairs";

            // Login
            driver.LoginUser("Noro", "Noro");

            // Navigate to wishlist and remove a movie
            driver.FindElement(By.Id("WishList")).Click();
            var movie = driver.FindElement(By.ClassName("row"))
                .FindElements(By.TagName("h2"))
                .FirstOrDefault(e => e.Text.Contains(movieTitle));
            movie.Click();
            driver.FindElement(By.Id("RemoveFromWishlist")).Click();

            // Check the wishlist if the movie is removed
            driver.FindElement(By.Id("WishList")).Click();
            Assert.That(driver.PageSource.Contains(movieTitle), Is.False);
        }

        [Test]
        public void SearchForMovie()
        {
            string movieTitle = "Hamilton";

            // Login
            driver.LoginUser("Noro", "Noro");

            // Navigate to All Movies and search for a specific movie
            driver.FindElement(By.Id("Movies")).Click();
            new SelectElement(driver.FindElement(By.Id("Search"))).SelectByText("Title");
            driver.FindElement(By.Id("SearchField")).SendKeys(movieTitle);
            driver.FindElement(By.Id("SearchButton")).Click();

            // Check that only one movie is returned with the correct title
            Assert.That(driver.FindElements(By.XPath("/html/body/div[2]/div/div/div/a/h2")).Single().Text.Contains(movieTitle), Is.True);
        }

        [Test]
        public void SearchForGenre()
        {
            string genre = "Drama";

            // Login
            driver.LoginUser("Noro", "Noro");

            // Navigate to All Movies and search for a specific movie
            driver.FindElement(By.Id("Movies")).Click();
            driver.WaitUntilElementVisible(By.Id("SearchButton"));
            new SelectElement(driver.FindElement(By.Id("Search"))).SelectByText("Genre");
            driver.FindElement(By.Id("SearchField")).SendKeys(genre);
            driver.FindElement(By.Id("SearchButton")).Click();

            // Select a random movie from filtered result
            var listings = driver.FindElements(By.TagName("div"));            
            Random r = new Random();
            int randomValue = r.Next(listings.Count()); //Fucky code, might return number out of desired range
            var randomMovie = listings.ElementAt(randomValue);
            driver.ScrollingToElementAndClick(By.LinkText(randomMovie.Text));  
            
            // Check that selected movie is correct genre
            Assert.That(driver.FindElement(By.Id("Genre")).Text.Contains(genre), Is.True);
        }

        [Test]
        public void SearchForActor()
        {
            string actor = "Nicolas Cage";

            // Login
            driver.LoginUser("Noro", "Noro");

            // Navigate to All Movies and search for a specific movie
            driver.FindElement(By.Id("Movies")).Click();
            driver.WaitUntilElementVisible(By.Id("SearchButton"));
            new SelectElement(driver.FindElement(By.Id("Search"))).SelectByText("Actors");
            driver.FindElement(By.Id("SearchField")).SendKeys(actor);
            driver.FindElement(By.Id("SearchButton")).Click();

            // Select a movie from filtered result
            driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/a/h2")).Click();
            driver.WaitUntilPageLoadedCompletely();

            // Check that selected movie contains author name
            Assert.That(driver.FindElement(By.Id("Actors")).Text.Contains(actor), Is.True);
        }

        [Test]
        public void TestSuccesfulLogout()
        {
            // Login
            driver.LoginUser("Noro", "Noro");
            driver.WaitUntilElementVisible(By.Id("LogoutButton"));

            // Logout
            driver.FindElement(By.Id("LogoutButton")).Click();

            // Check that login button is present on page
            Assert.That(driver.FindElement(By.Id("SubmitButton")).Displayed, Is.True);
        }
    }
}
