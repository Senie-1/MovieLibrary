namespace MovieLibrary.Models
{
    using Microsoft.AspNetCore.Identity;

    public class Review
    {
        public int Id { get; set; }

        public string Content { get; set; } = null!;
        public int Rating { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        public string UserId { get; set; } = null!;
        public IdentityUser User { get; set; } = null!;
    }

}
