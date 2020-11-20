using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieClient.Movies1
{
    public class MovieModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public int Duration { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
        public string Director { get; set; }
        public string ProductionCompany { get; set; }
        public string Actors { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }

    }
}