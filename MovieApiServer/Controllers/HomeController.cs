using MoviesAPIServer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace MoviesAPIServer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var Movie = new MovieModel();
            string connectionString = @"Server=tcp:uniqeservername.database.windows.net,1433;Initial Catalog=MoviesDB;User Id=AdminLogin;Password=AdminPassword123;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var query = "Select * from [dbo].Movie";
                              
                SqlCommand comand = new SqlCommand(query, conn);
                SqlDataReader reader = comand.ExecuteReader();
                while(reader.Read())
                {
                    Movie.Title = reader["Title"].ToString();
                    Movie.Plot = reader["Plot"].ToString(); 
                }


                conn.Close();

                ViewBag.Title = "Home Page";

            }

            return View();
        }
    }
}
