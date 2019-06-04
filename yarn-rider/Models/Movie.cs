using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace yarn_rider.Models
{
    public class Movie
    {
        public int MovieID { get; set; }
        
        [Display(Name = "Movie Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Movie name is required")]
        public string MovieName { get; set; }
        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date is required")]
        public string Date { get; set; }
        
        public int Rate { get; set; }
        
        [Display(Name = "Poster URL")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Poster url is required")]
        public string PosterURL { get; set; }
        
        [Display(Name = "Video URL")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Stream url is required")]
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
    
    public enum Modifier
    {
        RatingAscending,
        RatingDescending,
        TitleAscending,
        TitleDescending
    }

}