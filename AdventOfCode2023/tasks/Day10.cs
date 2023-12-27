namespace AdventOfCode2023;

public class Day10 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day10Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day10Task2();
}

public class Day10Task1 : BaseTask
{
    public override string Solve()
    {
        // parse input as a list of pipe sections
        // find coordinates of S, then start parsing
        // look in all four directions to find a direction to go in
        // - switch to find next coordinate
        // - e.g. if current value is L, look in the appropriate direction to find next one
        // add new coordinate to end of list
        // ensure you handle the possibility of an out-of-bounds exception in X and Y
        // add node to list and follow the path
        // return length of list / 2 (figure out whether to round up or down if it's an odd number)
    }
}

public class Day10Task2 : Day10Task1
{ }

class Pipe
{
    public Pipe(string value, int x, int y)
    {
        _value = value;
        _x = x;
        _y = y;
    }

    private readonly int _x;
    public int X
    {
        get
        {
            return _x;
        }
    }

    private readonly int _y;
    public int Y
    {
        get
        {
            return _y;
        }
    }

    private readonly string _value;
    public string Value
    {
        get
        {
            return _value;
        }
    }
}