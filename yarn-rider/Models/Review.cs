namespace yarn_rider.Models
{
    public class Review
    {
        public int ReviewID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public string Date { get; set; }

        // Navigation Properties
        public virtual Movie Movie { get; set; }
        public virtual User User { get; set; }
    }
}