using Microsoft.AspNetCore.Identity;
using MovieLibrary.Models.Domain.Entities;
using MovieLibrary.Models.Domain.Enums;

namespace MovieLibrary.Models.Domain.Entities
{
    public class UserMovie
    {
        public Guid Id { get; set; }

    
        public string UserId { get; set; }= null!;
        public IdentityUser User { get; set; } = null!;

        public WatchStatus WatchStatus { get; set; }

        public Guid MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

       
    }

}
