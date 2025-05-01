using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class ClientCreateFormModel
{
    [Display(Name = "Client Name", Prompt = "Please enter client name.")]
    [Required(ErrorMessage = "Client name is required")]
    public string ClientName { get; set; } = null!;


    [Display(Name = "Contact Person", Prompt = "Please enter contact person.")]
    [Required(ErrorMessage = "Contact person is required.")]
    [RegularExpression(@"^[A-Za-zÅÄÖåäö\s\-']{2,50}$", ErrorMessage = "Please enter a valid name.")]
    public string ContactPerson { get; set; } = null!;

    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email Address", Prompt = "Please enter email address.")]
    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; } = null!;


    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone Number", Prompt = "Please enter phone number.")]
    [Phone(ErrorMessage = "Please enter a valid phone number.")]
    public string? PhoneNumber { get; set; }

}
