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
        int sumOfGameIDs = possibleGames.Sum();
        return sumOfGameIDs.ToString();
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
        var minimumRequirements = new Dictionary<string, int>() {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 }
        };

        foreach (Dictionary<string, int> subset in game.Subsets)
        {
            foreach (KeyValuePair<string, int> pair in minimumRequirements)
            {
                string key = pair.Key;
                int value = pair.Value;

                bool hasKey = subset.ContainsKey(key);
                if (!hasKey) continue;

                bool hasTooManyCubes = subset[key] > value;
                if (hasTooManyCubes) return 0;
            }
        }

        return game.GameID;
    }

    protected Game[] ParseInput()
    {
        var games = new Game[InputRows.Length];

        for (int i = 0; i < games.Length; ++i)
        {
            string currentRow = InputRows[i];
            games[i] = new Game(currentRow);
        }

        return games;
    }
}

public class Day02Task2 : Day02Task1
{
    public override string Solve()
    {
        Game[] games = ParseInput();
        int[] gamePowers = GetGamePowers(games);
        int sumOfGamePowers = gamePowers.Sum();
        return sumOfGamePowers.ToString();
    }

    private static int[] GetGamePowers(Game[] games)
    {
        int[] gamePowers = new int[games.Length];

        for (int i = 0; i < games.Length; ++i)
        {
            Game currentGame = games[i];
            int power = GetGamePower(currentGame);
            gamePowers[i] = power;
        }

        return gamePowers;
    }

    private static int GetGamePower(Game game)
    {
        var minimumValues = new Dictionary<string, int>(){
            { "red", 0 },
            { "green", 0 },
            { "blue", 0 },
        };

        foreach (Dictionary<string, int> subset in game.Subsets)
        {
            foreach (KeyValuePair<string, int> pair in subset)
            {
                bool higherThanMinimum = pair.Value > minimumValues[pair.Key];
                if (higherThanMinimum)
                {
                    minimumValues[pair.Key] = pair.Value;
                }
            }
        }

        int power = MultiplyValues(minimumValues);
        return power;
    }

    private static int MultiplyValues(Dictionary<string, int> values)
    {
        int red = values["red"],
        green = values["green"],
        blue = values["blue"];

        return red * green * blue;
    }
}
