namespace MovieLibrary.Models.Domain.Entities
{
    public class Movie
    {

        public Guid Id { get; set; }
        public int Rating { get; set; }

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int ReleaseYear { get; set; }
        public string? PosterUrl { get; set; }

        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public string? ImageUrl { get; set; }


        public ICollection<UserMovie> UserMovies { get; set; } = new List<UserMovie>();

    }
}
