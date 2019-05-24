using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace yarn.Models
{
    public class Movie
    {
        public int MovieID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Rate { get; set; }
        public string PosterURL { get; set; }
        public string MovieURL { get; set; }

        // navigation properties
        public int ReviewID { get; set; }
        public int UserID { get; set; }
        
        public virtual List<Review> Reviews { get; set; }
        public virtual List<ApplicationUser> Users { get; set; }
    }
}