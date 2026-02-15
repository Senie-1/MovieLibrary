using Microsoft.AspNetCore.Identity;

namespace MovieLibrary.Models.Domain.Entities
{
    public class Review
    {
        public Guid Id { get; set; }

        public string Content { get; set; } = null!;
        public int Rating { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Guid MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        public string UserId { get; set; } = null!;
        public IdentityUser User { get; set; } = null!;
    }

}
