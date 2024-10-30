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
        Console.WriteLine(_gestionsModels.GetTache());
        return View();
    }
}
