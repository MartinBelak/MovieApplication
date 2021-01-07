using MovieClient.Models;
using MovieClient.Persistency;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MovieClient.Controllers
{
    public class MovieController : Controller
    {
        
        public ActionResult AllMovies()
        {
            string baseUrl = "http://localhost:59076/api/Movies/";
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
            List<MovieModel> Movies;
            string json = "";

            var user = TempData["IsLoggedIn"];
            TempData.Keep();           
            int UserId = int.Parse(user.ToString().Split(',')[1]);
            var ids = AzureDb.Instance.StringOfMovieIds(UserId);
            
            RestRequest request = new RestRequest();
            request.Method = Method.GET;
            IRestResponse response;
            string url = "http://localhost:59076/api/Movies?MovieIdsString=" + ids;

            var client = new RestClient
            {
                BaseUrl = new Uri(url),
            };

            response = client.Execute(request);
            if (response.IsSuccessful)
            {
                var test = JsonDocument.Parse(response.Content);
                json = test.RootElement.ToString();
            }
            else
            {
                json = "Failed";
            }

            //foreach (var id in ids)
            //{
            //    string url = "http://localhost:44328/api/Movies?MovieId=" + id;
            //    try
            //    {
            //        var client = new RestClient
            //        {
            //            BaseUrl = new Uri(url),
            //        };

            //        response = client.Execute(request);
            //        if (response.IsSuccessful)
            //        {
            //            var test = JsonDocument.Parse(response.Content);                 
            //            json = json + "," + test.RootElement.ToString();
            //        }
            //        else
            //        {
            //            json = "Failed";
            //        }
            //    }

            //    catch (Exception ex)
            //    {

            //        throw ex;
            //    }
            //}

            if (json == "Failed")
            {
                Movies = new List<MovieModel>();
            }
            else
            {          
                Movies = JsonConvert.DeserializeObject<List<MovieModel>>("[" + json.TrimStart(',') + "]");
            }

            return View(Movies);
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
                return View("~/Views/Movie/MovieDetails.cshtml", selectedMovie);
            }
            else
            {
                TempData["WishlistMessage"] = "Movie is already in your wishlist.";
                return View("~/Views/Movie/MovieDetails.cshtml", selectedMovie);
            }
        }

        public ActionResult Search(string SearchQuery, string SearchType)
        {
            string baseUrl = "http://localhost:59076/api/Movies/";
            string messageUrl = "Search"+"?SearchQuery="+SearchQuery+"&SearchType="+SearchType;
            string url = baseUrl + messageUrl;
            string json = "";
            List<MovieModel> ResultMovies = new List<MovieModel>();

            RestRequest request = new RestRequest();
            request.Method = Method.POST;

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

            if (json != "Failed")
            {
                ResultMovies = JsonConvert.DeserializeObject<List<MovieModel>>(json);
            }
        
            return View("~/Views/Movie/AllMovies.cshtml", ResultMovies);

        }
        public ActionResult WishListSearch(string SearchRequest)
        {

            List<MovieModel> ResultMovies =  AzureDb.Instance.WishListSearch(SearchRequest);

            return View("~/Views/Movie/AllMovies.cshtml", ResultMovies);

        }
    }
}