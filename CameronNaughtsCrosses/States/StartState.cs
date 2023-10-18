using CameronNaughtsCrosses.Context;

namespace CameronNaughtsCrosses.States;

public class StartState : IGameState
{
    private readonly IGameState _nextState;

    public StartState(IGameState nextState)
    {
        _nextState = nextState;
    }

    public void ProcessGameState(GameContext context)
    {
        Console.WriteLine("---- Game Start! ----");
        context.State = _nextState;
    }
}