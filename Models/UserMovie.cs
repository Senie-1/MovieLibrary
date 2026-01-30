using Microsoft.AspNetCore.Identity;
using MovieLibrary.Models.Enums;

namespace MovieLibrary.Models
{
    public class UserMovie
    {
        public int Id { get; set; }

    
        public string UserId { get; set; }= null!;
        public IdentityUser User { get; set; } = null!;

        public WatchStatus WatchStatus { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

       
    }

}
