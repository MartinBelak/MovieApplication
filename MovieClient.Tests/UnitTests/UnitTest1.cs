using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieClient.Controllers;
using MovieClient.Models;
using MovieClient.Persistency;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDatabaseConnection()
        {
            //arrange           
            //act
            var instance = AzureDb.Instance;
            //assert
            Assert.IsNotNull(instance);
        }
        [TestMethod]
        public void TestReceiveMovies()
        {
            //arrange            
            //act
            var movies = new MovieController().AllMovies();
            //assert
            Assert.IsNotNull(movies);
        }
        [TestMethod]
        public void TestMovieDetails()
        {
            //arrange
            MovieModel model = new MovieModel();
            var controller = new MovieController();
            //act
            var MovieDetailsView = controller.MovieDetails(model);
            //assert
            Assert.IsInstanceOfType(MovieDetailsView, typeof(ActionResult));
        }
    }
}
