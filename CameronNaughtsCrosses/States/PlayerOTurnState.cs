namespace CameronNaughtsCrosses.States;

public class PlayerOTurnState : PlayerTurnState
{
    public override char PlayerSymbol => 'O';

    protected override IGameState GetNextState() => new PlayerXTurnState();
}