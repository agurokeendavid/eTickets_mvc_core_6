using eTickets.Models;

namespace eTickets.ViewModels;

public class NewMovieDropdownsViewModel
{
    public List<Producer> Producers { get; set; } = new();
    public List<Cinema> Cinemas { get; set; } = new();
    public List<Actor> Actors { get; set; } = new();
}