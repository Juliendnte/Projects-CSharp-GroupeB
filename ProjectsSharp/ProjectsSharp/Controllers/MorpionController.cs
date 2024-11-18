using Microsoft.AspNetCore.Mvc;
using ProjectsSharp.Models;
using ProjectsSharp.Service;

namespace ProjectsSharp.Controllers
{
  [ApiController]
[Route("api/[controller]")]
public class TicTacToeController : Controller
{
    private readonly TicTacToeModel _ticTacToeModel;
    private static TicTacToeGame CurrentGame { get; set; } = new TicTacToeGame();
    

    public TicTacToeController(TicTacToeModel ticTacToeModel)
    {
        _ticTacToeModel = ticTacToeModel;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(CurrentGame); 
    }

    [HttpGet("move")]
    public IActionResult MakeMove(int row, int col)
    {
        _ticTacToeModel.MakeMove(CurrentGame, row, col);
        return PartialView("_GameBoard", CurrentGame);
        
    }
    
    [HttpGet("restart")]
    public IActionResult restartGame()
    {
        CurrentGame = new TicTacToeGame();
        return PartialView("_GameBoard", CurrentGame);
    }
}}
