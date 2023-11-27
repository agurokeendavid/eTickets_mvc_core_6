using System.ComponentModel.DataAnnotations;

namespace eTickets.Data.ViewModels;

public class LoginViewModel
{
    [Display(Name = "Email Address")]
    [Required(ErrorMessage = "{0} is required")]
    public string EmailAddress { get; set; }
    
    [Display(Name = "Password")]
    [Required(ErrorMessage = "{0} is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}