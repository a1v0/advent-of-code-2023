namespace AdventOfCode2023;

public class Day03 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day03Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day03Task2();
}

public class Day03Task1 : BaseTask
{
    public override string Solve()
    {
        // parse input into rows
        // loop through rows
        // find all the numbers
        // store numbers in a tuple (or similar)
        // - number, row, start index (and end index, if needed)
        // check up, down, left and right of each number in search of a symbol
        // sum and return
    }

    private string[]? _inputRows;

    protected string[] InputRows
    {
        get
        {
            if (_inputRows is null)
            {
                _inputRows = GetInputRows();
            }
            return _inputRows;
        }
    }

    private string[] GetInputRows()
    {
        return Input.Split('\n');
    }
}

public class Day03Task2 : Day03Task1
{ }
