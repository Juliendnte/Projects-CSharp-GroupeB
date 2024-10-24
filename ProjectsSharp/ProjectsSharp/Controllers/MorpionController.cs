namespace ProjectsSharp.Controllers;

using Microsoft.AspNetCore.Mvc;
using Models;

[ApiController]
[Route("api/[controller]")]
public class TicTacToeController : Controller
{
    private readonly TicTacToeService _service;

    public TicTacToeController(TicTacToeService Morpionservice)
    {
        _service = Morpionservice;
    }

    public IActionResult Index()
    {
        return View();
    } 
    
    [HttpGet("new")]
    public IActionResult StartNewGame()
    {
        var game = _service.StartNewGame();
        return Ok(game);
    }

    [HttpPost("move")]
    public ActionResult<JsonResult> MakeMove([FromBody] MoveRequest request)
    {
        var game = request.Game;
        if (_service.MakeMove(game, request.Row, request.Col))
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