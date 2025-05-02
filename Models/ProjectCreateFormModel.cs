using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

//This code was developed with assistance from ChatGPT

public class ProjectCreateFormModel
{


    [Display(Name = "File upload")]
    [Required(ErrorMessage = "Please select a file to upload.")]
    public IFormFile File { get; set; } = null!;

    public string? ExistingIconPath { get; set; }
    public int Id { get; set; }

    [Display(Name = "Project Name", Prompt = "Please enter project name")]
    [Required(ErrorMessage = "Project name is required")]
    public string Name { get; set; } = null!;

    [Display(Name = "Description", Prompt = "Please enter project description")]
    [Required(ErrorMessage = "Description is required.")]
    [StringLength(500, ErrorMessage = "Description can be up to 500 characters")]
    public string Description { get; set; } = null!;

    [Display(Name = "Customer", Prompt = "Please enter customer name")]
    [Required(ErrorMessage = "Customer is required.")]
    public string Customer { get; set; } = null!;

    [Display(Name = "Status", Prompt = "Please select project status")]
    [Required(ErrorMessage = "Status is required.")]
    public string Status { get; set; } = "Started";

    [DataType(DataType.Date)]
    [Display(Name = "Start Date")]
    public DateTime? StartDate { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "End Date")]
    public DateTime? EndDate { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid budget")]
    public decimal? Budget { get; set; }
}
