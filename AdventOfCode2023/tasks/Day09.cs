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
        // create class Sequence
        // - contents, array
        // - child (optional), Sequence
        // - next value, recursive method
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
