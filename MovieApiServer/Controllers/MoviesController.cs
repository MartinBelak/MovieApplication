using MoviesAPIServer.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Script.Serialization;

namespace MoviesAPIServer.Controllers
{
    public class MoviesController : ApiController
    {
        public string  connectionString = ConfigurationManager.ConnectionStrings["MainConnectionString"].ToString();

        [HttpGet]
        public string AllMovies()
        {
           
            List<MovieModel> Movies = new List<MovieModel>();
                       
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var query = "Select * from [dbo].Movie";

                SqlCommand comand = new SqlCommand(query, conn);
                SqlDataReader reader = comand.ExecuteReader();
                int Count = 0;
                while (reader.Read())
                {
                    var Movie = new MovieModel();
                    Movie.MovieId = int.Parse(reader["MovieId"].ToString());
                    Movie.Title = reader["Title"].ToString();
                    Movie.Year = int.Parse(reader["Year"].ToString());
                    Movie.Genre = reader["Genre"].ToString();
                    Movie.Duration = int.Parse(reader["Duration"].ToString());
                    Movie.Country = reader["Country"].ToString();
                    Movie.Language = reader["Language"].ToString();
                    Movie.Director = reader["Director"].ToString();
                    Movie.ProductionCompany = reader["ProductionCompany"].ToString();
                    Movie.Actors = reader["Actors"].ToString();
                    Movie.Description = reader["Description"].ToString();
                    Count++;

                    if (Count>99)
                    {
                        break;
                    }
                    
                    Movies.Add(Movie);
                }

                conn.Close();
            }

            var Json = new JavaScriptSerializer().Serialize(Movies);        
            return Json;
        }

        [HttpGet]
        public string MovieById(int MovieId)
        {
            var Movie = new MovieModel();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var query = "Select * from [dbo].Movie Where MovieId = " + MovieId;

                SqlCommand comand = new SqlCommand(query, conn);
                SqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {               
                    Movie.MovieId = int.Parse(reader["MovieId"].ToString());
                    Movie.Title = reader["Title"].ToString();
                    Movie.Year = int.Parse(reader["Year"].ToString());
                    Movie.Genre = reader["Genre"].ToString();
                    Movie.Duration = int.Parse(reader["Duration"].ToString());
                    Movie.Country = reader["Country"].ToString();
                    Movie.Language = reader["Language"].ToString();
                    Movie.Director = reader["Director"].ToString();
                    Movie.ProductionCompany = reader["ProductionCompany"].ToString();
                    Movie.Actors = reader["Actors"].ToString();
                    Movie.Description = reader["Description"].ToString();
                }

                conn.Close();
            }

            var Json = new JavaScriptSerializer().Serialize(Movie);
            return Json;
        }

        [HttpGet]
        public string MoviesByIdString(string MovieIdsString)
        {
            var Movie = new MovieModel();
            string MovieList = "";

            if (MovieIdsString == null)
            {
                MovieList = "";
            }
            else
            {
                string[] movieIds = Regex.Split(MovieIdsString, @"\D+");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    foreach (var id in movieIds)
                    {
                        var query = "Select * from [dbo].Movie Where MovieId = " + id;

                        SqlCommand comand = new SqlCommand(query, conn);
                        SqlDataReader reader = comand.ExecuteReader();
                        while (reader.Read())
                        {
                            Movie.MovieId = int.Parse(reader["MovieId"].ToString());
                            Movie.Title = reader["Title"].ToString();
                            Movie.Year = int.Parse(reader["Year"].ToString());
                            Movie.Genre = reader["Genre"].ToString();
                            Movie.Duration = int.Parse(reader["Duration"].ToString());
                            Movie.Country = reader["Country"].ToString();
                            Movie.Language = reader["Language"].ToString();
                            Movie.Director = reader["Director"].ToString();
                            Movie.ProductionCompany = reader["ProductionCompany"].ToString();
                            Movie.Actors = reader["Actors"].ToString();
                            Movie.Description = reader["Description"].ToString();

                            var Json = new JavaScriptSerializer().Serialize(Movie);
                            MovieList += ", " + Json;
                        }
                        reader.Close();
                    }
                    conn.Close();
                }
            }
            
            return MovieList;
        }

        [HttpPost]
        public string Search(string SearchQuery)
        {

            List<MovieModel> Movies = new List<MovieModel>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var query = "Select * from [dbo].Movie where Title like '%"+SearchQuery+"%'";

                SqlCommand comand = new SqlCommand(query, conn);
                SqlDataReader reader = comand.ExecuteReader();
                int Count = 0;
                while (reader.Read())
                {
                    var Movie = new MovieModel();
                    Movie.MovieId = int.Parse(reader["MovieId"].ToString());
                    Movie.Title = reader["Title"].ToString();
                    Movie.Year = int.Parse(reader["Year"].ToString());
                    Movie.Genre = reader["Genre"].ToString();
                    Movie.Duration = int.Parse(reader["Duration"].ToString());
                    Movie.Country = reader["Country"].ToString();
                    Movie.Language = reader["Language"].ToString();
                    Movie.Director = reader["Director"].ToString();
                    Movie.ProductionCompany = reader["ProductionCompany"].ToString();
                    Movie.Actors = reader["Actors"].ToString();
                    Movie.Description = reader["Description"].ToString();
                    Count++;

                    if (Count > 99)
                    {
                        break;
                    }

                    Movies.Add(Movie);
                }

                conn.Close();
            }

            var Json = new JavaScriptSerializer().Serialize(Movies);
            return Json;           
        }
    }
}
