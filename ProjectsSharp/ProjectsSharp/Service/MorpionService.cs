using ProjectsSharp.Models;

namespace ProjectsSharp.Service
{
  public class TicTacToeService
{
    public bool MakeMove(TicTacToeGame game, int row, int col)
    {

        if (game.Board[row, col] == '\0' && !game.IsGameOver)
        {
            game.Board[row, col] = game.CurrentPlayer;

            if (CheckWin(game, game.CurrentPlayer))
            {
                game.IsGameOver = true;
                game.Winner = game.CurrentPlayer;
            }
            else if (IsBoardFull(game))
            {
                game.IsGameOver = true; 
            }
            else
            {

                game.CurrentPlayer = game.CurrentPlayer == 'X' ? 'O' : 'X';
            }

            return true;
        }

        return false; 
    }

    private bool CheckWin(TicTacToeGame game, char player)
    {
        for (int i = 0; i < 3; i++)
        {
            if ((game.Board[i, 0] == player && game.Board[i, 1] == player && game.Board[i, 2] == player) ||
                (game.Board[0, i] == player && game.Board[1, i] == player && game.Board[2, i] == player))
            {
                return true;
            }
        }

        if ((game.Board[0, 0] == player && game.Board[1, 1] == player && game.Board[2, 2] == player) ||
            (game.Board[0, 2] == player && game.Board[1, 1] == player && game.Board[2, 0] == player))
        {
            return true;
        }

        return false;
    }

    private bool IsBoardFull(TicTacToeGame game)
    {
        foreach (var cell in game.Board)
        {
            if (cell == '\0') return false;
        }
        return true;
    }
}}