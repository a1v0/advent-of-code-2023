namespace AdventOfCode2023;

public class Day09 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day09Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day09Task2();
}

public class Day09Task1 : BaseTask
{
    public override string Solve()
    {
        // parse input as array of Sequences
        // create list of ints to store next values
        // - iterate through Sequences and find next value
        // - add to list
        // sum list
        // return as string
    }
}

public class Day09Task2 : Day09Task1
{ }

public class Sequence
{
    public Sequence(string input)
    {
        _contents = ParseInput();
        // create class Sequence
        // - contents, array
        // - child (optional), Sequence
        // - next value, recursive method
    }

    private Sequence? Child
    {
        get;
        set;
    }

    private int? _nextValue;
    private int NextValue
    {
        get
        {
            _nextValue ??= GetNextValue();
            return _nextValue;
        }
    }

    private string[] _contents;
    private string[] Contents
    {
        get
        {
            return _contents;
        }
    }
}
