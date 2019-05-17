using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using yarn.Controllers;

namespace yarn.Models
{
    public class ReviewModels
    {
        /*needs to be edited*/
        //public ApplicationUser User { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public MovieModels Movie { get; set; }
        
    }
}