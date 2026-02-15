using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Models.ViewModels.Actors
{
    public class ActorViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public int MoviesCount { get; set; }
    }
}
