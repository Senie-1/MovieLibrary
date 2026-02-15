using System;
using System.ComponentModel.DataAnnotations;

namespace MovieLibrary.Models.ViewModels.Genres
{
    public class GenreCreateOrEditViewModel
    {
        public Guid? Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;
    }
}
