namespace MovieLibrary.Models.Domain.Entities
{
    public class Actor
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;
        public DateTime? BirthDate { get; set; }

        public ICollection<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }

}
