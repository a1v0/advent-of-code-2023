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

public class Day02Task2 : Day01Task1 { }

public class Game
{
    public Game(string input)
    {
        string rowIncipit = "Game ";
        Input = input.Substring(rowIncipit.Length);
        // create ParseInput which returns a tuple (game name, subsets)
    }

    private string Input
    {
        get;
    }

    private readonly Dictionary<string, int>[] _subsets;

    private readonly int _gameID;

    private (int gameID, Dictionary<string, int>[] subsets) ParseInput()
    {
        int gameID = GetGameID();
        Dictionary<string, int>[] subsets = GetSubsets();
    }

    private Dictionary<string, int>[] GetSubsets()
    {
        int spaceAfterColonIndex = Input.IndexOf(':') + 1;
        string subsetsInOneString = Input[(spaceAfterColonIndex + 1)..];
        string[] subsetsStrings = subsetsInOneString.Split("; ");

        var subsets = new Dictionary<string, int>[subsetsStrings.Length];

        for (int i = 0; i < subsets.Length; ++i)
        {
            Dictionary<string, int> subset = GetSubset(subsetsStrings[i]);
            subsets[i] = subset;
        }

        return subsets;
    }

    private static Dictionary<string, int> GetSubset(string subsetString)
    {
        var subset = new Dictionary<string, int>();
        string[] cubesStrings = subsetString.Split(", ");

        foreach (string cubeString in cubesStrings)
        {
            string[] cubeData = cubeString.Split(' ');
            string colour = cubeData[1];
            int quantity = int.Parse(cubeData[0]);

            subset.Add(colour, quantity);
        }

        return subset;
    }

    private int GetGameID()
    {
        int colonIndex = Input.IndexOf(':');
        string gameID = Input[..colonIndex];
        return int.Parse(gameID);
    }
}