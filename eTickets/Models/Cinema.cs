using System.ComponentModel.DataAnnotations;
using eTickets.Data.Base;

namespace eTickets.Models;

public class Cinema : IEntityBase
{
    [Key]
    public int Id { get; set; }

    [Display(Name = "Cinema Logo")]
    [Required(ErrorMessage = "{0} is required")]
    public string? Logo { get; set; }

    [Display(Name = "Cinema Name")]
    [Required(ErrorMessage = "{0} is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} must be between 3 and 50 chars")]
    public string? Name { get; set; }

    [Display(Name = "Cinema Description")]
    [Required(ErrorMessage = "{0} is required")]
    public string? Description { get; set; }
    
    // Relationships
    public List<Movie>? Movies { get; set; }
}