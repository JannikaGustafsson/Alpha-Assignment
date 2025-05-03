namespace WebApp.Entities;

public class Project
{
    // ----------------------------------------------------
    // This code was developed with assistance from ChatGPT
    // ----------------------------------------------------
    public string? IconPath { get; set; } = "/images/icons/A Icon.svg";


    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Customer { get; set; }
    public string Status { get; set; } = "Started";
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? Budget { get; set; }

}
