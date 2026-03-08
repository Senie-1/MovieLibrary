using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Models.ViewModels.Movies
{
    public class MovieViewModel
    {
        public Guid Id { get; set; }

        public int Rating { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int ReleaseYear { get; set; }


        public string? ImageUrl { get; set; }

        public List<string> Genres { get; set; } = new();

        public List<string> Actors { get; set; } = new();

        public int ReviewsCount { get; set; }

        public double AverageReviewRating { get; set; }
    }
}
