using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClient.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool Gender { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public string PasswordHash { get; set; }
    }
}