namespace MovieLibrary.DTOs
{
    public class MovieListDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public int Year { get; set; }
        public string ImageUrl { get; set; }
        public double Rating { get; set; }

    }
}
