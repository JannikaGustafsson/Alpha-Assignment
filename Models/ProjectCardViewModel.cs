namespace WebApp.Models;

public class ProjectCardViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Company { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string IconPath { get; set; } = "/images/Icons/A Icon.svg";
    public string Status { get; set; } = "Started";
    
}
