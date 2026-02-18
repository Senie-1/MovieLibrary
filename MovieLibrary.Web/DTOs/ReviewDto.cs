namespace MovieLibrary.DTOs
{
    public class ReviewDto
    {
        public string UserName { get; set; } = null!;
        public int Rating { get; set; } 
        public string Comment { get; set; } = null!;
        public DateTime CreatedAt { get; set; } 
        public Guid Id { get; set; }
    }
}
