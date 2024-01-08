namespace AdventOfCode2023;

public class Day14 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day14Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day14Task2();
}

public class Day14Task1 : BaseTask
{
    public override string Solve()
    {
        // parse input into columns
        // split columns by "#"
        // turn each block char array and sort such that O and . are separate
        // join by "#"
        // 
        // summarise each column
        // - loop through column's contents and add
        // sum all columns
        // return
        // 
    }
}

public class Day14Task2 : Day14Task1
{ }
