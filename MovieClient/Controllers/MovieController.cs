using MovieClient.Models;
using MovieClient.Persistency;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MovieClient.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult AllMovies()
        {

            string baseUrl = "http://localhost:44328/api/Movies/";
            string messageUrl = "AllMovies";
            string url = baseUrl + messageUrl;
            string json = "";
            List<MovieModel> Movies = new List<MovieModel>();



            RestRequest request = new RestRequest();
            request.Method = Method.GET;

            IRestResponse response;
            try
            {
                var client = new RestClient
                {
                    BaseUrl = new Uri(url),

                };

                response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    var test = JsonDocument.Parse(response.Content);
                    json = test.RootElement.ToString();
                    //response.con
                    //json = response.Content.Replace("\\","");
                    //json = json.Replace(@"\","");
                    //json = json.Replace("\\", "");
                }
                else
                {
                    json = "Failed";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            if (json == "Failed")
            {
                Movies = new List<MovieModel>();
            }
            else
            {
                Movies = JsonConvert.DeserializeObject<List<MovieModel>>(json);
            }


            return View(Movies);
        }

        public ActionResult MovieDetails(MovieModel movie)
        {

            return View(movie);
        }

        public ActionResult WishList()
        {
            var user = TempData["IsLoggedIn"];
            TempData.Keep();
            
            string UserId = user.ToString().Split(',')[1];

            var ids = AzureDb.Instance.MoviesForUserFromWishList();



            return View();
        }

        public ActionResult AddToWishList(MovieModel selectedMovie)
        {
            var selectedUser = TempData["IsLoggedIn"];
            int selectedUserId = int.Parse(selectedUser.ToString().Split(',')[1]);

            var movieIds = AzureDb.Instance.CheckForMovieIds(selectedUserId);
            if (!movieIds.Contains(selectedMovie.MovieId.ToString()))
            {
                AzureDb.Instance.AddToWishList(selectedMovie, selectedUserId);
                TempData["WishlistMessage"] = "Movie added to wishlist.";
                return View("~/Views/Movie/MovieDetails.cshtml");
            }
            else
            {
                TempData["WishlistMessage"] = "Movie is already in your wishlist.";
                return View("~/Views/Movie/MovieDetails.cshtml");
            }
        }
    }
}