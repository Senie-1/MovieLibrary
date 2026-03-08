using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Models.ViewModels.Movies
{
    public class MovieCreateOrEditViewModel
    {
        public Guid? Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(1900, 2100)]
        public int ReleaseYear { get; set; }

        public string? ImageUrl { get; set; }

        
        public List<Guid> SelectedGenreIds { get; set; } = new();


        public string? ActorNames { get; set; }

    }
}
