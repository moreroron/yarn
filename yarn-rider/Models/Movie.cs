using System;
using System.Collections.Generic;

namespace yarn_rider.Models
{
    public class Movie
    {
        public int MovieID { get; set; }
        public string MovieName { get; set; }
        public string Date { get; set; }
        public int Rate { get; set; }
        public string PosterURL { get; set; }
        public string MovieURL { get; set; }
        public Genre Genre { get; set; }

        // navigation properties
        public int ReviewID { get; set; }
        public int UserID { get; set; }
        
        public virtual List<Review> Reviews { get; set; }
        public virtual List<User> Users { get; set; }
    }

    public enum Genre
    {
        Comedy,
        Horror,
        Action,
        Thriller, 
        Animation
    }
}