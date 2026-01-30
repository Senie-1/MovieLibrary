using Microsoft.AspNetCore.Identity;
using MovieLibrary.Enum;
namespace MovieLibrary.Models
{
    public class UserMovie
    {
        public int Id { get; set; }

    
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

       

        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public WatchStatus Status { get; set; }
    }

}
