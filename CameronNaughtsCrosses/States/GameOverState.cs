using CameronNaughtsCrosses.Context;

namespace CameronNaughtsCrosses.States;

public class GameOverState : IGameState
{
    public void ProcessGameState(GameContext context)
    {
        Console.WriteLine("-- Game Over! --");

        // Check if player X won
        if (context.HasPlayerMetWinCondition('X'))
        {
            Console.WriteLine("Player X won!");
        }
        // Check if player O won
        else if (context.HasPlayerMetWinCondition('O'))
        {
            Console.WriteLine("Player O won!");
        }
        // If neither player won, it's a draw
        else
        {
            Console.WriteLine("It's a draw!");
        }

        Console.WriteLine("Would you like to play again? (y/n)");

        string? input = null;

        while (input == null)
        {
            input = Console.ReadLine();

            if (input == "y")
            {
                context.State = new StartState(new PlayerOTurnState());
                context.ResetBoard();
            }
            else if (input == "n")
            {
                Console.WriteLine("Thanks for playing!");
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Please enter y or n");
                input = null;
            }
        }
    }
}