using System.Globalization;

namespace MovieLibrary.DTOs
{
    public class WishlistDto
    {
        public int Id { get; set; }
        public string MovieTitle { get; set; }
        public string WatchStatus { get; set; } 
        public string PosterUrl { get; set; }
    }
}
