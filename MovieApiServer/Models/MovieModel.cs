using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesAPIServer.Models
{
    public class MovieModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public DateTime Duration { get; set; }
        public int Rating { get; set; }
        public string Cast { get; set; }
        public DateTime Released { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Plot { get; set; }
    }
}