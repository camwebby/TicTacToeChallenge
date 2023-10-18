using CameronNaughtsCrosses.Context;

namespace CameronNaughtsCrosses.States;


public interface IGameState
{
    void ProcessGameState(GameContext context);
}