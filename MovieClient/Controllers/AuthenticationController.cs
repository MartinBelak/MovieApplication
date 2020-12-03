using MovieClient.Models;
using MovieClient.Persistency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieClient.Controllers
{
    public class AuthenticationController : Controller
    {

        public ActionResult Login(UserModel model)
        {
            var UserData = AzureDb.Instance.LoginUser(model);
            if (UserData.UserId == 0)
            {
                TempData["IsLoggedIn"] = null;
                TempData["LogginMessage"] = "The username or password is not correct.";

                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
                TempData["IsLoggedIn"] = UserData.UserName + "," + UserData.UserId;

                return Redirect("~/Home/Index");
            }
        }
        public ActionResult RegisterUserPage()
        {
            ViewBag.Message = "Create new user";
            return View();
        }

        public ActionResult RegisterUser(UserModel model)
        {
            var response = AzureDb.Instance.RegisterUser(model);

            if (response == 1)
            {
                TempData["LogginMessage"] = "Registration Succesfull :)";
                return View("~/Views/Home/Index.cshtml");

            }
            else if (response == 2)
            {
                TempData["RegMessage"] = "This username already exists :(";
                return View("RegisterUserPage");
            }
            else
            {
                TempData["RegMessage"] = "Something went wrong :(";
                return View("RegisterUserPage");
            }
        }
    }
}