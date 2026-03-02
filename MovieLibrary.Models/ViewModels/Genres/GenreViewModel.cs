

namespace MovieLibrary.Models.ViewModels.Genres
{
    public class GenreViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public List<GenreViewModel> Genres { get; set; }
    }
}
