namespace ProjectsSharp.Models
{
public class TicTacToeGame
{
    public char[,] Board { get; set; } = new char[3, 3]; 
    public char CurrentPlayer { get; set; } = 'X'; 
    public char CurrentPlayer2 { get; set; } = 'O';
    public bool IsGameOver { get; set; } = false;
    public char Winner { get; set; } = ' '; 
}
}