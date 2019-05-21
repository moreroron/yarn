using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using yarn.Controllers;

namespace yarn.Models
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }
        [ForeignKey("ApplicationUser")]
        public int UserID { get; set; }
        [ForeignKey("MovieModels")]
        public int MovieID { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }

        public virtual MovieModels Movie { get; set; }
        public virtual ApplicationUser User { get; set; }
    }

    public class ReviewDbContext : DbContext
    {
        public DbSet<Review> Reviews { get; set; }
    }
}