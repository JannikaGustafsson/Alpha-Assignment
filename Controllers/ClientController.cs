using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class ClientController : Controller
{
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(ClientCreateFormModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        return View();
    }
}
