namespace AdventOfCode2023;

public class Day13 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day13Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day13Task2();
}

public class Day13Task1 : BaseTask
{
    public override string Solve()
    {
        int summarisedPatterns = SummariseAshPatterns();
        return summarisedPatterns.ToString();
        // create class of AshPattern
        // - Rows
        // - RowsAboveMirror
        // - Columns
        // - ColumnsLeftOfMirror
        // - Summary
        // parse input into rows (string[])
        // parse columns into an array of strings, too, so we don't need to use nested loops
        // checker method:
        // - loop through rows or columns (i = 0, j = Length - 1)
        // - if rows[i] == rows[j]
        //   - call method to check that i + 1 == j - 1, etc.
        //   - if it's a valid mirror
        //     - find midpoint between i and j
        //     - round up to nearest whole number to give amount of columns to left
        //     - return
        // - if rows[i] != rows[j], check rows[j - 1]
        //   - ++i and keep going
        // throw error if nothing found
    }

    private AshPattern[]? _ashPatterns;
    private AshPattern[] AshPatterns
    {
        get
        {
            _ashPatterns ??= GetAshPatterns();
            return _ashPatterns;
        }
    }

    private AshPattern[] GetAshPatterns()
    {
        string[] splitInput = Input.Split("\n\n");
        var ashPatterns = new AshPattern[splitInput.Length];

        for (int i = 0; i < ashPatterns.Length; ++i)
        {
            ashPatterns[i] = new AshPattern(splitInput[i]);
        }

        return ashPatterns;
    }
}

public class Day13Task2 : Day13Task1
{ }

public class AshPattern
{
    public AshPattern(string input)
    {
        Input = input;
    }

    private string Input { get; }

    private int? _summary;
    private int Summary
    {
        get
        {
            _summary ??= GetSummary();
            return _summary;
        }
    }
}