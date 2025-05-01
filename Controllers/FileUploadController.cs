using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class FileUploadController(IWebHostEnvironment env) : Controller
{
    private readonly IWebHostEnvironment _env = env;

    [HttpGet]
    public IActionResult Upload()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Upload(FileUploadViewModel model)
    {
        if (!ModelState.IsValid || model.File == null || model.File.Length == 0)
        {
            ModelState.AddModelError("File", "Please select a file to upload.");
            return View(model);
        }

        var uploadFolder = Path.Combine(_env.WebRootPath, "uploads");

        Directory.CreateDirectory(uploadFolder);

        var filePath = Path.Combine(uploadFolder, $"{Guid.NewGuid()}_{ Path.GetFileName(model.File.FileName)}");

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await model.File.CopyToAsync(stream);
        }

        ViewBag.Message = "File was uploaded successully.";
  

        return View();
    }
}

