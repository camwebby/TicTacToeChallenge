namespace CameronNaughtsCrosses.States;

public class PlayerXTurnState : PlayerTurnState
{
    public override char PlayerSymbol => 'X';

    protected override IGameState GetNextState() => new PlayerOTurnState();
}