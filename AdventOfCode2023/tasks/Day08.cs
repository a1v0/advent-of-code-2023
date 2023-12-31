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
    }

    protected virtual int CountSteps()
    {
        int stepsRequired = 0;
        string currentLocation = "AAA";

        while (currentLocation is not "ZZZ")
        {
            char direction = Instructions[InstructionIndex];
            string nextLocation = Directory[currentLocation][direction];
            currentLocation = nextLocation;
            ++stepsRequired;
            ++InstructionIndex;
        }

        return stepsRequired;
    }

    private int _instructionIndex = 0;
    protected int InstructionIndex
    {
        get
        {
            return _instructionIndex;
        }
        set
        {
            int length = Instructions.Length;

            if (value < length)
            {
                _instructionIndex = value;
                return;
            }

            int difference = value - length;
            _instructionIndex = difference;
        }
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

    private static Dictionary<char, string> ParseNeighbours(string entry)
    {
        // I could use a regex here but thought it might be easier to do it this way
        string left = entry.Substring(7, 3),
        right = entry.Substring(12, 3);

        var neighbours = new Dictionary<char, string>();
        neighbours['L'] = left;
        neighbours['R'] = right;

        return neighbours;
    }

    private static string ParseLocation(string entry)
    {
        return entry[..3];
    }

    private Dictionary<string, Dictionary<char, string>>? _directory;
    protected Dictionary<string, Dictionary<char, string>> Directory
    {
        get
        {
            _directory ??= ParseDirectory();
            return _directory;
        }
    }

    private string? _instructions;
    protected string Instructions
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
{
    protected override int CountSteps()
    {
        int stepsRequired = 0;

        while (!AllLocationsEndInZ())
        {
            UpdateAllLocations();
            ++stepsRequired;
            ++InstructionIndex;

            if (stepsRequired % 1000 == 0)
            {
                Console.WriteLine($"{stepsRequired} moves processed.");
            }
        }

        return stepsRequired;
    }

    private void UpdateAllLocations()
    {
        char direction = Instructions[InstructionIndex];

        for (int i = 0; i < CurrentLocations.Length; ++i)
        {
            string currentLocation = CurrentLocations[i];
            string nextLocation = Directory[currentLocation][direction];
            CurrentLocations[i] = nextLocation;
        }
    }

    private bool AllLocationsEndInZ()
    {
        foreach (string location in CurrentLocations)
        {
            bool endsInZ = location[2] == 'Z';
            if (!endsInZ) return false;
        }

        return true;
    }

    private string[]? _currentLocations;
    private string[] CurrentLocations
    {
        get
        {
            _currentLocations ??= InitialiseCurrentLocations();
            return _currentLocations;
        }
    }

    private string[] InitialiseCurrentLocations()
    {
        var locations = new List<string>();
        foreach (string location in Directory.Keys)
        {
            bool endsInA = location[2] == 'A';
            if (endsInA) locations.Add(location);
        }
        return locations.ToArray();
    }
}
