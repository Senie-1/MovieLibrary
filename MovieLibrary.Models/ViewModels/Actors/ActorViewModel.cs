
namespace MovieLibrary.Models.ViewModels.Actors
{
    public class ActorViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public int MoviesCount { get; set; }
    }
}
