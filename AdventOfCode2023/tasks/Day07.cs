namespace AdventOfCode2023;

public class Day07 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day07Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day07Task2();
}

public class Day07Task1 : BaseTask
{
    public override string Solve()
    {
        // parse input into array of custom Hand objects
        // - int bid
        // - string cards
        // - int type (strongest type is 6, weakest is 0)
        // create sorting method to sort the array
        // - if a.type > b.type, a goes last
        // - a.type < b.type, a goes first
        // - a.type == b.type, use some sort of switch statement method to assess which is greater
        // sort array
        // iterate over array to find total score
        // - maybe use an aggregator method, as a learning exercise
        // return as string
    }
}

public class Day07Task2 : Day07Task1
{ }

public class Hand
{
    public Hand(string handInput)
    {

    }

    private readonly int _bid;

    public int Bid
    {
        get
        {
            return _bid;
        }
    }
}