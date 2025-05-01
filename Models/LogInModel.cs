using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class LogInModel
{
    [Display(Name = "Email", Prompt = "Enter your email address")]
    [Required(ErrorMessage = "Please enter your email.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = null!;

    [Display(Name = "Password", Prompt = "Enter your password")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Please enter your password.")]
    [RegularExpression("^(?=.*[A-ZÅÄÖ])(?=.*[a-zåäö])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-zÅÄÖåäö\\d@$!%*?&]{8,}$", ErrorMessage = "Please enter a strong password.")]
    public string Password { get; set; } = null!;
}
