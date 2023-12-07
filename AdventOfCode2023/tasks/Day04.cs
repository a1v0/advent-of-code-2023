namespace AdventOfCode2023;

public class Day04 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day04Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day04Task2();
}

public class Day04Task1 : BaseTask
{
    public override string Solve()
    {
        // split into list of winning numbers and actual numbers
        // - array of tuples?
        // loop over winning numbers per day to find amount of winning numbers
        // calculate points based on amount of winning numbers
        // sum all points
        //
        //
    }
}

public class Day04Task2 : Day04Task1
{ }
