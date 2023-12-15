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
        // parse instructions to string
        // parse nodes as dictionary<string, dictionary>
        // - parse node contents as dictionary<char, string>, e.g. 'L': "BBB"
        // while loop until we reach ZZZ
        // - navigate through locations using indices
        // - count cycles 
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
}

public class Day08Task2 : Day08Task1
{ }
