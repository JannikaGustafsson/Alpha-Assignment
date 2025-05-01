using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class RegisterUserModel
{
    [Display(Name = "First Name", Prompt = "Enter your first name")]
    [Required(ErrorMessage = "Please enter your first name.")]
    [StringLength(50, ErrorMessage = "Maximum 100 characters allowed.")]
    [RegularExpression(@"^[A-Za-zÅÄÖåäö\s\-']{2,50}$", ErrorMessage = "Please enter a valid name")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Last Name", Prompt = "Enter your last name")]
    [Required(ErrorMessage = "Please enter your last name.")]
    [StringLength(50, ErrorMessage = "Maximum 50 characters allowed.")]
    [RegularExpression(@"^[A-Za-zÅÄÖåäö\s\-']{2,50}$", ErrorMessage = "Please enter a valid name")]
    public string LastName { get; set; } = null!;

   
    [Display(Name = "Email", Prompt = "Enter your email address")]
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Please enter your email.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [StringLength(100, ErrorMessage = "Maximum 100 characters allowed.")]
    public string Email { get; set; } = null!;

    [Display(Name = "Password", Prompt = "Enter your password")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Please enter your password.")]
    [RegularExpression("^(?=.*[A-ZÅÄÖ])(?=.*[a-zåäö])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-zÅÄÖåäö\\d@$!%*?&]{8,}$", ErrorMessage = "Please enter a strong password.")]
    public string Password { get; set; } = null!;

    [Display(Name = "Confirm Password", Prompt = "Confirm your password")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Please confirm your password.")]
    [Compare(nameof(Password), ErrorMessage = "Your password do not match!")]
    public string ConfirmPassword { get; set; } = null!;

}
