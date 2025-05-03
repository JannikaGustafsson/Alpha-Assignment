using WebApp.Data;
using WebApp.Models;

namespace WebApp.Services;
// ----------------------------------------------------
// This code was developed with assistance from ChatGPT
// ----------------------------------------------------

public class ProjectService(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    public List<ProjectCardViewModel> GetProjects()
    {
        var projects = _context.Projects.ToList();

        return projects.Select(p => new ProjectCardViewModel
        {
            Id = p.Id,
            Title = p.Name,
            Company = p.Customer ?? "",
            Description = p.Description ?? "",
            IconPath = "/images/icons/A Icon.svg",
            Status = p.Status ?? "Started"
        }).ToList();
    }
}

