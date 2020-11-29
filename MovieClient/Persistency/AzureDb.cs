using MovieClient.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
                    cmd.Connection = conn;
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

        public void RegisterUser (UserModel user)
        {
            UserModel result = new UserModel();
            var userName = user.UserName;
            var gender = user.Gender;
            var age = user.Age;
            var nationality = user.Nationality;
            var password = user.PasswordHash;

            try
            {
                using (SqlConnection conn = new SqlConnection(_builder.ConnectionString))
                {   
                    SqlCommand cmd = new SqlCommand("dbo.AddUser", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("pUserName", userName);
                    cmd.Parameters.AddWithValue("pGender", BitConverter.GetBytes(gender));
                    cmd.Parameters.AddWithValue("pAge",age);
                    cmd.Parameters.AddWithValue("pNationality", nationality);
                    cmd.Parameters.AddWithValue("pPassword", password);
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.ExecuteNonQuery();                 
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }       
        }

        public List<int> MoviesForUserFromWishList()
        {
            List<int> MoviesIds = new List<int>();

            return MoviesIds;
        }
    }
}