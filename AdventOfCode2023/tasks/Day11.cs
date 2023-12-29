namespace AdventOfCode2023;

public class Day11 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day11Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day11Task2();
}

public class Day11Task1 : BaseTask
{
    public override string Solve()
    {
        // parse input into list of lists of int?s
        // - '.' is null
        // - galaxies get a unique number
        // work right-to-left and bottom-to-top to expand universe:
        // - do columns first
        // - loop through each index of each row and check whether there's a galaxy
        // - if no galaxy, insert a column at that location in each row <== THIS WHOLE THING IS VERY INEFFICIENT BUT I'M UNSURE IF THERE'S A BETTER WAY TO DO IT
        // - then rows
        // - if row contains only '.', insert a copy of that row below
        // then turn list into simple array of nullable Galaxies
        // - Galaxy has X,Y properties and a number
        // 
        // make dictionary with keys of tuples (galaxyA, galaxyB), where A is always the smaller galaxy number
        // - value is int?
        // go through all galaxies one by one and create keys for each pair, setting value to null <== AGAIN, THIS FEELS INEFFICIENT
        // iterate over dictionary
        // set value to calculated distance between galaxies
        // - this is always a simple calculation of change in Y plus change in X
    }
}

public class Day11Task2 : Day11Task1
{ }
