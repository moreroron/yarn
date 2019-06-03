using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace yarn_rider.Models
{
    public partial class User
    {
        public int UserID { get; set; }
        
        public string EmailID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public Country Country { get; set; }
        public bool IsEmailVerified { get; set; }
        public System.Guid ActivationCode { get; set; }
        
        // navigation properties
        public int ReviewID { get; set; }
        public int MovieID { get; set; }
        
        public virtual List<Review> Reviews { get; set; }
        public virtual List<Movie> Movies { get; set; }
    }
    public enum Countries
    {
        Comedy,
        Horror,
        Action,
        Thriller, 
        Animation
    }
}