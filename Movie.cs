namespace MovieLibrary.Models.ViewModels
{
    public class Movie
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string GenreName { get; set; }
        public int ReleaseYear { get; set; }
        public string Actors { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; }
    }
}