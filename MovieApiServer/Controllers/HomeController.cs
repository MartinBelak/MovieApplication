﻿using MoviesAPIServer.Models;
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
           

            return View();
        }
    }
}
