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

        [Display(Name = "UserName"), Required(ErrorMessage = "Please Enter Username..")]
        [StringLength(40, MinimumLength = 2), RegularExpression(@"^[A-Z]+[a-zA-Z]*$")]
        public string UserName { get; set; }

        [Display(Name = "Gender"), Required(ErrorMessage = "Select the Gender...")]
        public bool Gender { get; set; }

        [Display(Name = "Age"), Required(ErrorMessage = "Select the Age...")]
        [Range(10,110)]
        public int Age { get; set; }

        [Display(Name = "Nationality"), Required(ErrorMessage = "Select the Nationality...")]
        [RegularExpression(@"[A-Z]+[a-zA-Z]*$")]
        public string Nationality { get; set; }

        [Display(Name = "Password"), Required(ErrorMessage = "Please Enter Password..."), DataType(DataType.Password)]
        public string PasswordHash { get; set; }
    }
}