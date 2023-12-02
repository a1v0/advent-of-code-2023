namespace AdventOfCode2023;

public class Day02 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day01Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day01Task2();
}

public class Day02Task1 : BaseTask
{
    public override string Solve()
    {
        // - parse input as rows
        // - parse rows as games
        // - parse games as subsets
        // - parse subsets as dictionary or similar
        // - loop through each game to see whether it fits the desired pattern
    }
}

public class Day02Task2 : Day01Task1
{
}