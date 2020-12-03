using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClient.Models
{
    public class WishlistModel
    {
        public int Id { get; set; }
        public string MovieIdList { get; set; }
        
        public int UserId { get; set; }
        public virtual MovieModel MovieModel { get; set; } // ? 
    }
}