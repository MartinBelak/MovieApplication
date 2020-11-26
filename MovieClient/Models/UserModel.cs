using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieClient.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Please Enter Username..")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Select the Gender...")]
        [Display(Name = "Gender")]
        public bool Gender { get; set; }

        [Required(ErrorMessage = "Select the Age...")]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Select the Nationality...")]
        [Display(Name = "Nationality")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "Please Enter Password...")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string PasswordHash { get; set; }
    }
}