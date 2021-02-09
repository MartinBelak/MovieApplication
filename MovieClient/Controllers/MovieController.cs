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
        public List<MovieModel> AllMoviesModel()
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
            return Movies;
        }


        public ActionResult AllMovies()
        {
            var Movies = AllMoviesModel();

            return View(Movies);
        }

        public ActionResult MovieDetails(MovieModel movie)
        {
            TempData.Remove("WishlistMessage");
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

        public ActionResult RemoveFromWishList(MovieModel selectedMovie)
        {
            var selectedUser = TempData["IsLoggedIn"];
            int selectedUserId = int.Parse(selectedUser.ToString().Split(',')[1]);

            var movieIds = AzureDb.Instance.CheckForMovieIds(selectedUserId);           
            AzureDb.Instance.RemoveFromWishList(selectedMovie, selectedUserId);

            TempData["WishlistMessage"] = "Movie was removed from your wishlist.";
            return View("~/Views/Movie/MovieDetails.cshtml", selectedMovie);        
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

        public string GetMovieFromTVMaze(string MovieName)
        {
            string baseUrl = "http://api.tvmaze.com/singlesearch/shows?q=";
            string messageUrl = MovieName;
            string url = baseUrl + messageUrl;
            var json ="" ;
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

            
            return json;
                  
        }

        public ActionResult RecommendedMovies()
        {
            //Extract UserId
            var user = TempData["IsLoggedIn"];
            TempData.Keep();
            int UserId = 0;

            if (user != null)
            {
                UserId = int.Parse(user.ToString().Split(',').ElementAt(1));
            }

            Dictionary<MovieModel, float> RecommendedMoves = new Dictionary<MovieModel, float>();
            var Movies = AllMoviesModel();

            //in this loop points are awarded for the two categories
            //maximum available points is 50 for each category forming 100 points total
            foreach (var movie in Movies)
            {
                float rating = 0;
                List<string> MovieGenres = new List<string>();
                var TVMazeMovie = GetMovieFromTVMaze(movie.Title);

                //if requested movie was not found in TVMaze Database
                if (TVMazeMovie == "Failed")
                {
                    rating = 0;
                    
                }
                else
                {
                    //Gather genres for the movie
                    var JsonMovieArray = JObject.Parse(TVMazeMovie);
                    var ratingValue = JsonMovieArray["rating"]["average"].ToString();
                    var Genres = JsonMovieArray["genres"];
                    var GenresCount =Genres.Count();
                    if (GenresCount!=0)
                    {
                        foreach (var genre in Genres)
                        {
                            MovieGenres.Add(genre.ToString().ToLower());
                        }
                        
                    }
                    //Gather rating for the movie
                    if (ratingValue == "")
                    {
                        rating = 0;
                    }
                    else
                    {
                        rating = float.Parse(ratingValue);
                    }
                }
                
                //calculating points for rating the formula being P% * X = Y
                //P being percentage
                //X Maximum points
                float P = rating / 10;
                float X = 50;
                float Points = P * X;

                var preferences = AzureDb.Instance.GetUserById(UserId);
                var UserPrefArray = preferences.Split(' ');

                var ResultGenres = MovieGenres.Intersect(UserPrefArray);
                //calculating points for genres formula: Y/X = P%
                //Y Being the total of user preferences, X the genres movie has 
                
                X = UserPrefArray.Count();
                
                var Y = ResultGenres.Count();
                float Percentage =Y/X ;
                float GenrePonts = Percentage * 50;
                
                Points = Points + GenrePonts;
               
                RecommendedMoves.Add(movie, Points);
            }
            var sortedDict = from entry in RecommendedMoves orderby entry.Value descending select entry;

            return View("~/Views/Movie/RecommendedMovies.cshtml",sortedDict);
        }
    }
}