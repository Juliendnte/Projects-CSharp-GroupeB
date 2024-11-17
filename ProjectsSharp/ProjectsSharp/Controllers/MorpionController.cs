using Microsoft.AspNetCore.Mvc;
using ProjectsSharp.Models;
using ProjectsSharp.Service;

namespace ProjectsSharp.Controllers
{
  [ApiController]
[Route("api/[controller]")]
public class TicTacToeController : Controller
{
    private readonly TicTacToeService _ticTacToeService;
    private static TicTacToeGame CurrentGame { get; set; } = new TicTacToeGame();
    

    public TicTacToeController(TicTacToeService ticTacToeService)
    {
        _ticTacToeService = ticTacToeService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(CurrentGame); 
    }

    [HttpGet("move")]
    public IActionResult MakeMove(int row, int col)
    {
        _ticTacToeService.MakeMove(CurrentGame, row, col);
        return PartialView("_GameBoard", CurrentGame);
        
    }
    
    [HttpGet("restart")]
    public IActionResult restartGame()
    {
        CurrentGame = new TicTacToeGame();
        return PartialView("_GameBoard", CurrentGame);
    }
}}
