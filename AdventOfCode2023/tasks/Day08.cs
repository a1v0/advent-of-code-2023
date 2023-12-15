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

    private int CountSteps()
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
    private int InstructionIndex
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
{
    // extract logic fro 'while' to helper methods to make it easier to track current locations
    // create property to store current locations
    // create method to validate final letters of all locations
    // add console log, in case it takes ages to run
    // the rest should be the same, methinks. I don't foresee any problems (famous last words...)
}
