namespace ProjectsSharp.Controllers;

using Microsoft.AspNetCore.Mvc;
using Models;

public class GestionController : Controller
{
    private readonly GestionsModels _gestionsModels;
    
    public GestionController(GestionsModels gestionsModels)
    {
        _gestionsModels = gestionsModels;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public string GetTache()
    {
        return _gestionsModels.GetTache();
    }
}
