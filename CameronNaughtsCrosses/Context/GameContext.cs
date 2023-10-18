using CameronNaughtsCrosses.States;

namespace CameronNaughtsCrosses.Context;

public class GameContext
{
    private IGameState _state;
    private char[] _board = new char[0];
    public readonly int BoardSize;

    public GameContext(IGameState startState, int boardSize = 9)
    {
        // Board size must be square
        if (boardSize % Math.Sqrt(boardSize) != 0)
        {
            throw new ArgumentException("Board size must be square");
        }

        BoardSize = boardSize;
        _state = startState;

        ResetBoard();
    }

    public IGameState State
    {
        get { return _state; }
        set { _state = value; }
    }

    private char[] Board
    {
        get { return _board; }
    }


    /**
     * Returns error message if index is invalid,
     * otherwise returns null
     */
    private string? ValidateIndexPlacement(int index)
    {
        // Check bounds
        if (!(index >= 0 && index < _board.Length))
        {
            return "That position is out of bounds";
        }

        // Check if index is already taken
        if (_board[index] != ' ')
        {
            return "That position is already taken";
        }

        return null;
    }

    public void SetBoardIndex(int index)
    {
        if (_state is not PlayerTurnState)
        {
            throw new InvalidOperationException("Cannot set board index while game is not in progress");
        }

        string? errorMessage = null;

        while ((errorMessage = ValidateIndexPlacement(index)) != null)
        {
            Console.WriteLine(errorMessage);
            Console.WriteLine($"Enter a number between 1 and {BoardSize}");
            if (int.TryParse(Console.ReadLine(), out var result))
            {
                index = result - 1;
            }
        }

        _board[index] = ((PlayerTurnState)this._state).PlayerSymbol;
    }


    private bool IsGameOver()
    {
        if (this.HasPlayerMetWinCondition('X') || this.HasPlayerMetWinCondition('O'))
        {
            return true;
        }

        return this.Board.All(c => c != ' ');
    }

    public string GetBoardString()
    {
        // Assumes 3x3
        // TODO: Make this scale with board size
        return string.Join("\n", new string[]
        {
            $" {this.Board[0]} | {this.Board[1]} | {this.Board[2]} ",
            "---+---+---",
            $" {this.Board[3]} | {this.Board[4]} | {this.Board[5]} ",
            "---+---+---",
            $" {this.Board[6]} | {this.Board[7]} | {this.Board[8]} "
        });
    }


    public void ResetBoard()
    {
        // Validate can be initialized
        if (this._state is not (StartState or GameOverState))
        {
            throw new InvalidOperationException("Cannot reset board while game is in progress");
        }

        _board = new char[BoardSize];
        for (int i = 0; i <= BoardSize - 1; i++)
        {
            _board[i] = ' ';
        }
    }


    public bool HasPlayerMetWinCondition(char player)
    {
        // Assumes 3x3
        // TODO: Make this scale with board size
        return
            // Horizontal
            (this.Board[0] == player && this.Board[1] == player && this.Board[2] == player) ||
            (this.Board[3] == player && this.Board[4] == player && this.Board[5] == player) ||
            (this.Board[6] == player && this.Board[7] == player && this.Board[8] == player) ||

            // Vertical
            (this.Board[0] == player && this.Board[3] == player && this.Board[6] == player) ||
            (this.Board[1] == player && this.Board[4] == player && this.Board[7] == player) ||
            (this.Board[2] == player && this.Board[5] == player && this.Board[8] == player) ||

            // Diagonal
            (this.Board[0] == player && this.Board[4] == player && this.Board[8] == player) ||
            (this.Board[2] == player && this.Board[4] == player && this.Board[6] == player);
    }


    public void Request()
    {
        if (!(this._state is StartState))
        {
            Console.WriteLine(this.GetBoardString());

            if (this.IsGameOver())
            {
                this.State = new GameOverState();
            }
        }

        _state.ProcessGameState(this);
    }
}