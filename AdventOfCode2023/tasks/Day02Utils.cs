namespace AdventOfCode2023;

public class Game
{
    public Game(string input)
    {
        string rowIncipit = "Game ";
        Input = input[rowIncipit.Length..];
        (int gameID, Dictionary<string, int>[] subsets) = ParseInput();
        _gameID = gameID;
        _subsets = subsets;
    }

    private string Input
    {
        get;
    }

    private readonly int _gameID;
    public int GameID
    {
        get
        {
            return _gameID;
        }
    }

    private readonly Dictionary<string, int>[] _subsets;
    public Dictionary<string, int>[] Subsets
    {
        get
        {
            return _subsets;
        }
    }

    private (int gameID, Dictionary<string, int>[] subsets) ParseInput()
    {
        int gameID = GetGameID();
        Dictionary<string, int>[] subsets = GetSubsets();
        return (gameID, subsets);
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