using System.ComponentModel.DataAnnotations;

namespace TextChat.ViewModels;

public class RegisterViewModel
{
    [Required]
    public string Username{ get; set; }
    [DataType(DataType.Password)]
    [Required]
    public string Password { get; set; }
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    [Required]
    public string ConfirmPassword { get; set; }
}