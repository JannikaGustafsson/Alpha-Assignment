using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Entities;
using WebApp.Models;
using WebApp.Services;

// ----------------------------------------------------
// This code was developed with assistance from ChatGPT
// ----------------------------------------------------

namespace WebApp.Controllers
{
    public class ProjectsController(ProjectService projectService, ApplicationDbContext context) : Controller
    {
        private readonly ProjectService _projectService = projectService;
        private readonly ApplicationDbContext _context = context;

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
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProjectCreateFormModel model)
        {
            if (ModelState.IsValid)
            {
                var project = new Project
                {
                    Name = model.Name,
                    Description = model.Description,
                    Customer = model.Customer,
                      Status = model.Status
                };

                _context.Projects.Add(project);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");

            }

            return View(model);
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
            if (ModelState.IsValid)
            {
                var project = _context.Projects.Find(model.Id);
                if (project == null)
                    return NotFound();

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

            return View(model);
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
}


