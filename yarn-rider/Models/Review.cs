using System.ComponentModel.DataAnnotations;

namespace yarn_rider.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        
        [Display(Name = "Review Title")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Review title is required")]
        public string Title { get; set; }
        
        [Display(Name = "Content")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Content is required")]
        public string Content { get; set; }
        
        [Display(Name = "Rating")]
        [Range(0, 10, ErrorMessage = "Rating must be between 0 and 10")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "A rate is required")]
        public int Rating { get; set; }

        [Range(1990, 2019, ErrorMessage = "Date must be between 1990 and 2019")]
        public string Date { get; set; }

        // Navigation Properties
        public virtual Movie Movie { get; set; }
        public virtual User User { get; set; }
    }
}