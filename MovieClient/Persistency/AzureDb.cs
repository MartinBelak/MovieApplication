using MovieClient.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MovieClient.Persistency
{
    public class AzureDb
    {
        private SqlConnectionStringBuilder _builder;
        private static AzureDb instance = null;
        private static readonly object padlock = new object();

        private AzureDb()
        {
            this._builder = new SqlConnectionStringBuilder();
            _builder.ConnectionString = @"Server = tcp:specialservername.database.windows.net,1433; Initial Catalog = MovieAppDB; User Id = AdminLogin; Password = AdminPassword123";
        }

        public static AzureDb Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new AzureDb();
                    }
                    return instance;
                }
            }
        }

        public UserModel LoginUser(UserModel user)
        {
            var userName = user.UserName;
            var password = user.PasswordHash;
            UserModel result = new UserModel();

            try
            {
                using (SqlConnection conn = new SqlConnection(_builder.ConnectionString))
                {                  
                    SqlCommand cmd = new SqlCommand("dbo.AuthenticateLogin", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("pUserName", userName);
                    cmd.Parameters.AddWithValue("@pPassword", password);
                    //cmd.Connection = conn;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        result.UserId = int.Parse(reader["UserId"].ToString());
                        result.UserName = reader["UserName"].ToString();                      
                        result.Gender = (bool)reader["Gender"];                       
                        result.Age = int.Parse(reader["Age"].ToString());
                        result.Nationality = reader["Nationality"].ToString();
                    }
                    else
                    {
                        conn.Close();
                        result.UserId = 0;
                    }
                    conn.Close();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return result;           
        }

        public int RegisterUser (UserModel user)
        {
            var userName = user.UserName;
            var gender = user.Gender;
            var age = user.Age;
            var nationality = user.Nationality;
            var password = user.PasswordHash;
            var response = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(_builder.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.AddUser", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("pUserName", userName);
                        cmd.Parameters.AddWithValue("pGender", BitConverter.GetBytes(gender));
                        cmd.Parameters.AddWithValue("pAge", age);
                        cmd.Parameters.AddWithValue("pNationality", nationality);
                        cmd.Parameters.AddWithValue("pPassword", password);
                        cmd.Parameters.Add("@responseMessage", SqlDbType.Char, 500);
                        cmd.Parameters["@responseMessage"].Direction = ParameterDirection.Output;
                        cmd.Connection = conn;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        response = int.Parse(cmd.Parameters["@responseMessage"].Value.ToString());
                        conn.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return response;
        }

        public List<int> MoviesForUserFromWishList()
        {
            List<int> MoviesIds = new List<int>();

            string input = "40,30,10,25.";
            // Split on one or more non-digit characters.
            string[] numbers = Regex.Split(input, @"\D+");
            foreach (string value in numbers)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    int i = int.Parse(value);
                    Console.WriteLine("Number: {0}", i);
                }
            }

            return MoviesIds;
        }

        public void AddToWishList(MovieModel movie, int userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_builder.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.AddToWishlist", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@pUserId", userId);
                        cmd.Parameters.AddWithValue("@pMovieId", movie.MovieId.ToString());
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            //return response; // might change to return something?
        }

        public void RemoveFromWishList(MovieModel movie, int userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_builder.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.RemoveFromWishlist", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@pUserId", userId);
                        cmd.Parameters.AddWithValue("@pMovieId", movie.MovieId.ToString());
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }           
        }

        public string[] CheckForMovieIds(int userId)
        {
            string queryString = "SELECT MovieIdList FROM dbo.[Wishlist] WHERE UserId = @id";
            string outputString = "";

            try
            {
                using (SqlConnection conn = new SqlConnection(_builder.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(queryString, conn))
                    {
                        conn.Open();
                        cmd.Parameters.AddWithValue("@id", userId);
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Read();
                        if (reader.HasRows)
                        {
                            outputString = reader["MovieIdList"].ToString();
                        }
                        conn.Close();
                    }
                }
            }                           
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }

            //returns string array of movie Ids
            string[] movieIds = Regex.Split(outputString, @"\D+");
            return movieIds;
        }

        public string StringOfMovieIds(int userId)
        {
            string queryString = "SELECT MovieIdList FROM dbo.[Wishlist] WHERE UserId = @id";
            string outputString = "";

            try
            {
                using (SqlConnection conn = new SqlConnection(_builder.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(queryString, conn))
                    {
                        conn.Open();
                        cmd.Parameters.AddWithValue("@id", userId);
                        SqlDataReader reader = cmd.ExecuteReader();
                        reader.Read();
                        if (reader.HasRows)
                        {
                            outputString = reader["MovieIdList"].ToString();
                        }
                        conn.Close();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }

            return outputString;
        }

        public List<MovieModel> WishListSearch(string SearchRequest)
        {
            List<MovieModel> Movies = new List<MovieModel>();

            using (SqlConnection conn = new SqlConnection(_builder.ConnectionString))
            {
                conn.Open();
                var query = "Select * from dbo.[Wishlist] where Title like '%" + SearchRequest + "%'";

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

            return Movies;
        }
    }
}