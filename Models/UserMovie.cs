using Microsoft.AspNetCore.Identity;
using MovieLibrary.Enum;
namespace MovieLibrary.Models
{
    public class UserMovie
    {
        public int Id { get; set; }

    
        public string UserId { get; set; }= null!;
        public IdentityUser User { get; set; } = null!;



        public int MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        public WatchStatus Status { get; set; }
    }

}
