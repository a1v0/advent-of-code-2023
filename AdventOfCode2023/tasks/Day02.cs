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
        Game[] games = ParseInput();
        int[] possibleGames = GetPossibleGames(games);
        // - parse input as rows
        // - parse rows as games
        //   - Game is a new class
        // - parse games as array of subsets
        // - parse subsets as dictionary or tuple
        // - loop through each game to see whether it fits the desired pattern
        return "";
    }

    private int[] GetPossibleGames(Game[] games)
    {
        var possibleGames = new List<int>();
        foreach (Game game in games)
        {
            int gameID = GetValidGameID(game);
            possibleGames.Add(gameID);
        }
        return possibleGames.ToArray();
    }

    private int GetValidGameID(Game game)
    {
        var minimumRequirements = new Dictionary<string, int>();
        minimumRequirements.Add("red", 12);
        minimumRequirements.Add("green", 13);
        minimumRequirements.Add("blue", 14);

        foreach (Dictionary<string, int> subset in game.Subsets)
        {
            foreach (KeyValuePair<string, int> pair in minimumRequirements)
            {
                string key = pair.Key;
                int value = pair.Value;

                bool hasKey = subset.ContainsKey(key);
                if (!hasKey) return 0;

                bool hasTooManyCubes = subset[key] > value;
                if (hasTooManyCubes) return 0;
            }
        }

        return game.GameID;
    }

    private Game[] ParseInput()
    {
        string[] inputRows = GetInputRows();
        var games = new Game[inputRows.Length];

        for (int i = 0; i < games.Length; ++i)
        {
            string currentRow = inputRows[i];
            games[i] = new Game(currentRow);
        }

        return games;
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