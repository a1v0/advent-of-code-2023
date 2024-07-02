namespace AdventOfCode2023;

public class Day21 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day21Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day21Task2();
}

public class Day21Task1 : BaseTask
{
    public override string Solve()
    {
        // create GardenPlot class
        // - list of neighbours
        // - coordinates?
        // parse input as GardenPlots
        // create global list of "active" plots (use a datatype that can contain only unique values, similar to Set in JavaScript)
        // loop through active plots, add each plot's neighbours to new list of active plots and deactivate current plot
        // do this X times and return total active plots
    }
}

public class Day21Task2 : Day21Task1
{ }
