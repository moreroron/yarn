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
        [Range(1990, 2019, ErrorMessage = "Date must be between 1990 and 2019")]
        public string Date { get; set; }
        
        [Range(0, 10, ErrorMessage = "Rating must be between 0 and 10")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Rate is required")]
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