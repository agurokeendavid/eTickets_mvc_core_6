using System.ComponentModel.DataAnnotations;
using eTickets.Data;

namespace eTickets.ViewModels;

public class NewMovieViewModel
{
    public int Id { get; set; }
    
    [Display(Name = "Movie name")]
    [Required(ErrorMessage = "{0} is required")]
    public string? Name { get; set; }
    
    [Display(Name = "Movie Description")]
    [Required(ErrorMessage = "{0} is required")]
    public string? Description { get; set; }
    
    [Display(Name = "Price")]
    [Required(ErrorMessage = "{0} is required")]
    public double Price { get; set; }
    
    [Display(Name = "Movie Poster Url")]
    [Required(ErrorMessage = "{0} is required")]
    public string? ImageURL { get; set; }
    
    [Display(Name = "Start Date")]
    [Required(ErrorMessage = "{0} is required")]
    public DateTime StartDate { get; set; }
    
    [Display(Name = "End Date")]
    [Required(ErrorMessage = "{0} is required")]
    public DateTime EndDate { get; set; }
    
    [Display(Name = "Movie Category")]
    [Required(ErrorMessage = "{0} is required")]
    public MovieCategory MovieCategory { get; set; }
    
    [Display(Name = "Actor(s)")]
    [Required(ErrorMessage = "{0} is required")]
    public List<int> ActorIds { get; set; }
    
    [Display(Name = "Cinema")]
    [Required(ErrorMessage = "{0} is required")]
    public int CinemaId { get; set; }
    
    [Display(Name = "Producer")]
    [Required(ErrorMessage = "{0} is required")]
    public int ProducerId { get; set; }
}