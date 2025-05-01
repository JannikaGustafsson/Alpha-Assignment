using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers;

[Authorize]
public class HomeController : Controller
{
    public IActionResult Index()
    {      

        ViewData["Title"] = "Home";        

        return View();
    }
}
