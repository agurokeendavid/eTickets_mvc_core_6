using System.ComponentModel.DataAnnotations;

namespace eTickets.Models;

public class Actor
{
    [Key]
    public int Id { get; set; }
    
    [Display(Name = "Profile Picture")]
    [Required(ErrorMessage = "{0} is required")]
    public string? ProfilePictureURL { get; set; }
    
    [Display(Name = "Full Name")]
    [Required(ErrorMessage = "{0} is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} must be between 3 and 50 chars")]
    public string? FullName { get; set; }
    
    [Display(Name = "Biography")]
    [Required(ErrorMessage = "{0} is required")]
    public string? Bio { get; set; }
    
    // Relationships
    public List<Actor_Movie>? Actors_Movies { get; set; }
}