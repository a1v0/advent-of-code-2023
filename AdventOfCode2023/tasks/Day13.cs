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
        // create class of AshPattern
        // - Rows
        // - RowsAboveMirror
        // - Columns
        // - ColumnsLeftOfMirror
        // - Summary
        // parse input to array of AshPatterns
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
        // sum array and return
    }
}

public class Day13Task2 : Day13Task1
{ }
