namespace ProjectsSharp.Controllers;

using Microsoft.AspNetCore.Mvc;
using Models;
using Service;

[ApiController]
[Route("api/[controller]")]
public class TicTacToeController : Controller
{
    private readonly TicTacToeService _morpionService;

    public TicTacToeController(TicTacToeService morpionservice)
    {
        _morpionService = morpionservice;
    }

    public IActionResult Index()
    {
        return View();
    } 
    
    [HttpGet("new")]
    public IActionResult StartNewGame()
    {
        var game = new TicTacToeGame();
        return Ok(game);
    }

    [HttpPost("move")]
    public ActionResult<JsonResult> MakeMove([FromBody] MoveRequest request)
    {
        var game = request.Game;
        if (_morpionService.MakeMove(game, request.Row, request.Col))
        {
            return Ok(game);
        }
        return BadRequest("Invalid move");
    }
}

public class MoveRequest
{
    public TicTacToeGame Game { get; set; }
    public int Row { get; set; }
    public int Col { get; set; }
}