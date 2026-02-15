namespace MovieLibrary.Models.Domain.Entities
{
    public class MovieActor
    {
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; } = null!;

        public Guid ActorId { get; set; }
        public Actor Actor { get; set; } = null!;
    }

}
