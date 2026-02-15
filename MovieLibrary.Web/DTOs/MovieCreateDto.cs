using MovieLibrary.Models.Domain.Entities;

namespace MovieLibrary.DTOs
{
    public class MovieCreateDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Year { get; set; }
        public string PosterUrl { get; set; } = null!;
        public string Review { get; set; } = null!;
        public int Rating { get; set; }

        public ICollection<MovieGenre> Genres { get; set; } 
        public ICollection<MovieActor> Actors { get; set; } 

    }
}
