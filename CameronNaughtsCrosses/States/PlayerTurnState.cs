using CameronNaughtsCrosses.Context;

namespace CameronNaughtsCrosses.States;

public abstract class PlayerTurnState : IGameState
{
    public abstract char PlayerSymbol { get; }

    public void ProcessGameState(GameContext context)
    {
        Console.WriteLine($"-- Player {PlayerSymbol}'s turn --");

        Console.WriteLine($"Enter a number between 1 and {context.BoardSize}");

        int index = -1;
        if (int.TryParse(Console.ReadLine(), out int result))
        {
            index = result - 1;
        }

        context.SetBoardIndex(index);

        context.State = GetNextState();
    }

    protected abstract IGameState GetNextState();
}