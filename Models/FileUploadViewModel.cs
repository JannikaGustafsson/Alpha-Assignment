using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class FileUploadViewModel
{
    [Display(Name = "File upload")]
    [Required(ErrorMessage = "Please select a file to upload.")]
    public IFormFile File { get; set; } = null!;

}
