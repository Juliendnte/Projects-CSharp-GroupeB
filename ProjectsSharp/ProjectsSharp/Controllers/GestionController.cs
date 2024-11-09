namespace ProjectsSharp.Controllers;

using Microsoft.AspNetCore.Mvc;
using Models;
using System.Text.Json;

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
    public IActionResult  GetTache()
    {
        try
        {
            var tache = _gestionsModels.GetTache();
            return Ok(tache);
        }
        catch (Exception)
        {
            var err = new { message = "Error getting your calendar tache" }; 
            return StatusCode(500, err);
        }
    }
}