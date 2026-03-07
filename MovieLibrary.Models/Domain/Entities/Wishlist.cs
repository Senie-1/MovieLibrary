using MovieLibrary.Models.Domain.Entities;

public class Wishlist
{
    public int Id { get; set; }

    public Guid MovieId { get; set; }

    public Movie Movie { get; set; }
}
