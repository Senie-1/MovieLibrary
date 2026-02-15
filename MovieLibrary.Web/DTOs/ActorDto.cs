namespace MovieLibrary.DTOs
{
    public class ActorDto
    {
        public Guid Id { get; set; }    
        public string Name { get; set; } = null!;
        public string PhotoUrl { get; set; } = null!;
    }
}
