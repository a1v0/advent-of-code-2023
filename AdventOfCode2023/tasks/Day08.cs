namespace AdventOfCode2023;

public class Day08 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day08Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day08Task2();
}

public class Day08Task1 : BaseTask
{
    public override string Solve()
    {
        int stepsRequired = CountSteps();
        return stepsRequired.ToString();
        // parse nodes as dictionary<string, dictionary>
        // - parse node contents as dictionary<char, string>, e.g. 'L': "BBB"
        // while loop until we reach ZZZ
        // - navigate through locations using indices
        // - count cycles 
    }

    private Dictionary<string, Dictionary<char, string>> ParseDirectory()
    {
        string[] entries = InputRows[2..];
        var directory = new Dictionary<string, Dictionary<char, string>>();

        foreach (string entry in entries)
        {
            string location = ParseLocation(entry);
            var neighbours = ParseNeighbours(entry);
            directory.Add(location, neighbours);
        }

        return directory;
    }

    private Dictionary<string, Dictionary<char, string>>? _directory;
    private Dictionary<string, Dictionary<char, string>> Directory
    {
        get
        {
            _directory ??= ParseDirectory();
            return _directory;
        }
    }

    private string? _instructions;
    private string Instructions
    {
        get
        {
            _instructions ??= ParseInstructions();
            return _instructions;
        }
    }

    private string ParseInstructions()
    {
        return InputRows[0];
    }
}

public class Day08Task2 : Day08Task1
{ }
