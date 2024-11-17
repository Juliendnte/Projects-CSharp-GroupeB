using ProjectsSharp.Service;

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
    public IActionResult GetTache()
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

    [HttpPatch("UpdateTache/{id}")]
    public IActionResult UpdateTache([FromRoute] int id, [FromBody] Tache tache)
    {
        try
        {
            var updated = _gestionsModels.UpdateTache(id, tache);
            return Ok(updated);
        }
        catch (Exception ex)
        {
            var err = new { message = "Error updating your calendar tache", error = ex.Message };
            return StatusCode(500, err);
        }
    }

    [HttpDelete("DeleteTache/{id}")]
    public IActionResult DeleteTache([FromRoute] int id)
    {
        try
        {
            return Ok(_gestionsModels.DeleteTache(id));
        }
        catch (Exception ex)
        {
            var err = new { message = "Error deleting your calendar tache", error = ex.Message };
            return StatusCode(500, err);
        }
    }

    [HttpPost("CreateTache")]
    public IActionResult CreateTache([FromBody] Tache tache)
    {
        try
        {
            return Ok(_gestionsModels.CreateTache(tache));
        }
        catch (Exception ex)
        {
            var err = new { message = "Error creating your calendar tache", error = ex.Message };
            return StatusCode(500, err);
        }
    }
}