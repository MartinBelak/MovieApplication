using MovieClient.Movies1;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieClient.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult AllMovies()
        {

            string baseUrl = @"https://localhost:44328/api/Movies/";
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
                    json =response.Content;
                }
                else
                {
                      json= "Failed";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            


            return View(Movies);
        }
    }
}