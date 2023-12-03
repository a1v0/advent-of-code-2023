namespace AdventOfCode2023;

public class Day02 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day02Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day02Task2();
}

public class Day02Task1 : BaseTask
{
    public override string Solve()
    {
        // - parse input as rows
        // - parse rows as games
        //   - Game is a new class
        // - parse games as array of subsets
        // - parse subsets as dictionary or tuple
        // - loop through each game to see whether it fits the desired pattern
    }

    private string[] GetInputRows()
    {
        return Input.Split('\n');
    }
}

public class Day02Task2 : Day01Task1
{ }
public class Game
{
    public Game(string input)
    {
        // create ParseInput which returns a tuple (game name, subsets)
    }

    private readonly Dictionary<string, int>[] _subsets;

    private readonly int _gameID;
}