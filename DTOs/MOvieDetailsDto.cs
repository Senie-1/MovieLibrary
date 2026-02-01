namespace MovieLibrary.DTOs
{
    public class MovieDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string ImageUrl { get; set; }

        public List<string> Genres { get; set; }
        public List<string> Actors { get; set; }
        public List<ReviewDto> Reviews { get; set; }
    }
}
