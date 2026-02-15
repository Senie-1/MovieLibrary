using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Models.ViewModels.Actors
{
    public class ActorCreateOrEditViewModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = null!;

        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }
    }
}
