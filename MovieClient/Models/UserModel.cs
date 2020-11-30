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

        [Display(Name = "UserName"), Required(ErrorMessage = "Fil in the Username")]
        [StringLength(40, MinimumLength = 2), RegularExpression(@"[a-zA-Z]*$")]
        public string UserName { get; set; }

        public bool Gender { get; set; }

        [Display(Name = "Age"), Required(ErrorMessage = "Fill in the Age")]
        [Range(10,110)]
        public int Age { get; set; }

        [Display(Name = "Nationality"), Required(ErrorMessage = "Fill in the Nationality")]
        [RegularExpression(@"[a-zA-Z]*$")]
        public string Nationality { get; set; }

        [Display(Name = "Password"), Required(ErrorMessage = "Fill in the Password"), DataType(DataType.Password)]
        public string PasswordHash { get; set; }
    }
}