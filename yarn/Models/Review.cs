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
        public int ReviewID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }

        // Navigation Properties
        public virtual Movie Movie { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}