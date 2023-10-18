using CameronNaughtsCrosses.Context;
using CameronNaughtsCrosses.States;

namespace CameronNaughtsCrosses;

internal static class Program
{
    public static void Main(string[] args)
    {
        GameContext game = new GameContext(new StartState(new PlayerOTurnState()));

        while (game.State is not GameOverState)
        {
            game.Request();
        }
    }
}