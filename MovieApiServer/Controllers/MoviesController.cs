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
        [HttpGet]
        public string AllMovies()
        {
            var Movie = new MovieModel();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MainConnectionString"].ToString()))
            {
                conn.Open();
                var query = "Select * from [dbo].Movie";

                SqlCommand comand = new SqlCommand(query, conn);
                SqlDataReader reader = comand.ExecuteReader();
                while (reader.Read())
                {
                    Movie.Title = reader["Title"].ToString();
                    Movie.Plot = reader["Plot"].ToString();
                }

                conn.Close();

            }

            JsonSerializer js = new JsonSerializer();
            var Json = new JavaScriptSerializer().Serialize(Movie);

           
            return Json;
        }
    }
}
