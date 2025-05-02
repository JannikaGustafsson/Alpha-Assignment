using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Entities;
using WebApp.Models;
using WebApp.Services;

// ----------------------------------------------------
// This code was developed with assistance from ChatGPT
// ----------------------------------------------------

namespace WebApp.Controllers;

[Authorize]
public class ProjectsController(ProjectService projectService, ApplicationDbContext context, IWebHostEnvironment env) : Controller
{
    private readonly ProjectService _projectService = projectService;
    private readonly ApplicationDbContext _context = context;
    private readonly IWebHostEnvironment _env = env;

    public IActionResult Index(string status ="All")
    {
        ViewData["CurrentStatus"] = status;
        var projects = _projectService.GetProjects();
        if (status != "All")
        {
            projects = projects.Where(p => p.Status == status).ToList();
        }

        return RedirectToAction("Index", "Home", new { status });
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new ProjectCreateFormModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProjectCreateFormModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        // Sätt standardväg till bild
        string filePath = "/Images/Icons/A Icon.svg";

        if (model.File != null && model.File.Length > 0)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "Images/Icons");
            Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(model.File.FileName)}";
            var fullPath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await model.File.CopyToAsync(stream);
            }

            filePath = "/Images/Icons/" + fileName;
        }

        var project = new Project
        {
            Name = model.Name,
            Customer = model.Customer,
            Description = model.Description,
            Status = model.Status,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Budget = model.Budget,
            IconPath = filePath
        };

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }


    [HttpGet]
    public IActionResult Edit(int id)
    {
        var project = _context.Projects.FirstOrDefault(p => p.Id == id);

        if (project == null)
            return NotFound();

        var model = new ProjectCreateFormModel
        {
            Id = project.Id,
            ExistingIconPath = project.IconPath,
            Name = project.Name,
            Customer = project.Customer,
            Description = project.Description!,
            Status = project.Status,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            Budget = project.Budget
        };

        return View(model);
    }
    [HttpPost]
    public IActionResult Edit(ProjectCreateFormModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var project = _context.Projects.Find(model.Id);
        if (project == null)
            return NotFound();

        if (model.File != null && model.File.Length > 0)
        {
            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(model.File.FileName)}";
            var uploadPath = Path.Combine(_env.WebRootPath, "Images/Icons");
            Directory.CreateDirectory(uploadPath);

            var filePath = Path.Combine(uploadPath, fileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            model.File.CopyTo(stream);

            project.IconPath = "/Images/Icons/" + fileName;
        }
        else if (!string.IsNullOrEmpty(model.ExistingIconPath))
        {
            project.IconPath = model.ExistingIconPath;
        }

        project.Name = model.Name;
        project.Customer = model.Customer;
        project.Description = model.Description;
        project.Status = model.Status;
        project.StartDate = model.StartDate;
        project.EndDate = model.EndDate;
        project.Budget = model.Budget;

        _context.SaveChanges();
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var project = _context.Projects.FirstOrDefault(p => p.Id == id);
        if (project == null)
            return NotFound();

        _context.Projects.Remove(project);
        _context.SaveChanges();

        return RedirectToAction("Index", "Home");
    }
}


