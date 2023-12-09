namespace AdventOfCode2023;

public class Day06 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day06Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day06Task2();
}

public class Day06Task1 : BaseTask
{
    public override string Solve()
    {
        // create list property to store quantity of beaten records per race
        // create Race class with properties of Time and Record
        // parse input as races
        // loop through races
        // loop through seconds in each race and find numbers that'd beat the record
        // multiple quantities, stringify and return
    }
}

public class Day06Task2 : Day06Task1
{ }
