using MovieClient.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
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
                    var test = JsonDocument.Parse(response.Content);
                    json = test.RootElement.ToString();
                    //response.con
                    //json = response.Content.Replace("\\","");
                    //json = json.Replace(@"\","");
                    //json = json.Replace("\\", "");
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

            
            List<MovieModel> Movies = JsonConvert.DeserializeObject<List<MovieModel>>(json);

            return View(Movies);
        }
    }
}