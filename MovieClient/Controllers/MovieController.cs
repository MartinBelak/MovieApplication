using MovieClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
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

            string baseUrl = "https://localhost:44328/api/Movies/";
            string messageUrl = "AllMovies";
            string url = baseUrl + messageUrl;
            string json = "";
            

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

            
            MovieModel[] Movies = JsonConvert.DeserializeObject<MovieModel[]>(json);

            return View(Movies);
        }
    }
}