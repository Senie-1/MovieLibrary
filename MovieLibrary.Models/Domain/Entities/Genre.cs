namespace MovieLibrary.Models.Domain.Entities
{
    public class Genre
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public ICollection<MovieGenre> MovieGenres { get; set; } = new List<MovieGenre>();
    }

}
