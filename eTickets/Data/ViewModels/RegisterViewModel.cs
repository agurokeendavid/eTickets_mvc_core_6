using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace eTickets.Data.ViewModels;

public class RegisterViewModel
{
    [Display(Name = "Full name")]
    [Required(ErrorMessage = "{0} is required")]
    public string FullName { get; set; }
    
    [Display(Name = "Email Address")]
    [Required(ErrorMessage = "{0} is required")]
    [DataType(DataType.EmailAddress)]
    public string EmailAddress { get; set; }
    
    [Display(Name = "Password")]
    [Required(ErrorMessage = "{0} is required")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Display(Name = "Confirm Password")]
    [Required(ErrorMessage = "{0} is required")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }
}