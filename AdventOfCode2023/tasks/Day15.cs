namespace AdventOfCode2023;

public class Day15 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day15Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day15Task2();
}

public class Day15Task1 : BaseTask
{
    public override string Solve()
    {
        // parse input as array of strings
        // loop through each string and run HASH on it
        // - initialise current at 0
        // - loop through each char
        //   - (int)char casts it as its ASCII value
        // - add ascii to current
        // - multiply by 17
        // - divide by 256 and return remainder
        // 
        // summarise and return as string
    }
}

public class Day15Task2 : Day15Task1
{ }
