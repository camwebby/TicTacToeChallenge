using CameronNaughtsCrosses.Context;
using CameronNaughtsCrosses.States;
using NUnit.Framework;

namespace CameronNaughtsCrosses.UnitTests;

[TestFixture]
public class GameContextTest
{
    GameContext _gameContext;

    [SetUp]
    public void SetUp()
    {
        _gameContext = new GameContext(new StartState(new PlayerOTurnState()));
    }

    [Test]
    public void GameContext_State_Is_StartState()
    {
        Assert.That(_gameContext.State, Is.InstanceOf<StartState>());
    }

    [Test]
    public void GameContext_State_Is_GameOverState_After_GameOverState()
    {
        _gameContext.State = new GameOverState();
        Assert.That(_gameContext.State, Is.InstanceOf<GameOverState>());
    }


    [TestCase(new[] { 0, 1, 2 }, TestName = "Should_Win_When_Horizontal_Line")]
    [TestCase(new[] { 0, 3, 6 }, TestName = "Should_Win_When_Vertical_Line")]
    [TestCase(new[] { 0, 4, 8 }, TestName = "Should_Win_When_Diagonal_Line")]
    public void PlayerO_Should_Win_When_Line_Formed(int[] indices)
    {
        foreach (var index in indices)
        {
            _gameContext.State = new PlayerOTurnState();
            _gameContext.SetBoardIndex(index);
        }

        Assert.That(_gameContext.HasPlayerMetWinCondition('O'), Is.True);
    }


    [TestCase]
    public void Should_Draw_When_Board_Full()
    {
        IGameState playerOTurnState = new PlayerOTurnState();
        IGameState playerXTurnState = new PlayerXTurnState();

        _gameContext.State = playerOTurnState;
        _gameContext.SetBoardIndex(0);

        _gameContext.State = playerXTurnState;
        _gameContext.SetBoardIndex(1);

        _gameContext.State = playerOTurnState;
        _gameContext.SetBoardIndex(2);

        _gameContext.State = playerXTurnState;
        _gameContext.SetBoardIndex(6);

        _gameContext.State = playerOTurnState;
        _gameContext.SetBoardIndex(4);

        _gameContext.State = playerXTurnState;
        _gameContext.SetBoardIndex(5);

        _gameContext.State = playerOTurnState;
        _gameContext.SetBoardIndex(3);

        _gameContext.State = playerXTurnState;
        _gameContext.SetBoardIndex(8);

        _gameContext.State = playerOTurnState;
        _gameContext.SetBoardIndex(7);

        Console.WriteLine(_gameContext.GetBoardString());

        Assert.That(_gameContext.HasPlayerMetWinCondition('O'), Is.False);
        Assert.That(_gameContext.HasPlayerMetWinCondition('X'), Is.False);
    }


    private static IEnumerable<IGameState> NotInPlayStates()
    {
        yield return new StartState(new PlayerOTurnState());
        yield return new GameOverState();
    }

    private static IEnumerable<IGameState> PlayStates()
    {
        yield return new PlayerOTurnState();
        yield return new PlayerXTurnState();
    }


    [Test, TestCaseSource(nameof(PlayStates))]
    public void Should_Not_allow_reset_board_when_game_over(IGameState gameState)
    {
        _gameContext.State = gameState;
        Assert.Throws<InvalidOperationException>(() => _gameContext.ResetBoard());
    }


    [Test, TestCaseSource(nameof(NotInPlayStates))]
    public void Should_Throw_SetIndex_Not_In_Play(IGameState gameState)
    {
        _gameContext.State = gameState;
        Assert.Throws<InvalidOperationException>(() => _gameContext.SetBoardIndex(0));
    }
}