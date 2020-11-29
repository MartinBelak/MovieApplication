using MovieClient.Models;
using MovieClient.Persistency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieClient.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {                   
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Login(UserModel model)
        {
            var UserData = AzureDb.Instance.LoginUser(model);
            if (UserData.UserId == 0)
            {
                TempData["IsLoggedIn"] = null;
                TempData["LogginMessage"] = "No user with such Id in Our Database or there is error in connection string";
                return View("./Index");
            }
            else
            {
                TempData["IsLoggedIn"] = UserData.UserName + "," + UserData.UserId;
                return View("./Index", UserData);
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Logout()
        {
            TempData["IsLoggedIn"] = null;            
            return View("./Index");
        }

        public ActionResult RegisterUser(UserModel model)
        {
            AzureDb.Instance.RegisterUser(model);
            
            return View("./Index");
        }
    }
}