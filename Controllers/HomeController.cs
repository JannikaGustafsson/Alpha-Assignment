using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApp.Services;
using WebApp.Models;

namespace WebApp.Controllers;

[Authorize]
public class HomeController(ProjectService projectService) : Controller
{
    private readonly ProjectService _projectService = projectService;

    public IActionResult Index(string status = "All")
    {
        ViewData["Title"] = "Home";
        ViewData["CurrentStatus"] = status;

        var projects = _projectService.GetProjects();

        if (status != "All")
        {
            projects = projects.Where(p => p.Status == status).ToList();
        }

        return View(projects);
    }
}

