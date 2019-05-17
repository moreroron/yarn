using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace yarn.Models
{
    public class MovieModels
    {
        [Key]
        public int MovieID { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public int Rate { get; set; }
        //public LinkedList<ReviewModels> Reviews { get; set; }
    }

    public class MovieDbContext : DbContext
    {
        public DbSet<MovieModels> Movies { get; set; }
    }
}