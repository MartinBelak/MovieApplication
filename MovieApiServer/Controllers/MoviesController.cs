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

            JsonSerializer js = new JsonSerializer();
            var Json = new JavaScriptSerializer().Serialize(Movies);
          
            return Json;
        }

        [HttpGet]
        public string MovieById(int MovieId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                conn.Close();
            }
                return "";
        }
    }
}
