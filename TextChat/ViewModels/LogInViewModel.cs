using System.ComponentModel.DataAnnotations;

namespace TextChat.ViewModels;

public class LogInViewModel
{
    public string Username { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
}