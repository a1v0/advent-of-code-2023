namespace AdventOfCode2023;

public class Day05 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day05Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day05Task2();
}

public class Day05Task1 : BaseTask
{
    public override string Solve()
    {
        // parse input into individual maps (could be a separate class)
        // maps are arrays of tuples (source, translation, length)
        // maps are sorted by source number
        // loop over seed numbers, then:
        // loop over map contents
        // - if seed number >= source number, then check whether seed number is within range
        // - if not, move on to next one
        // - if seed number < source, or if you make it through the map without finding a match, then return seed number unmapped
        // - if you find a match, find index of match within range, then add to translation. return match
        // plug this value into the next map
        // put final number into array of location numbers
        // find smallest location number, stringify, return
    }
}

public class Day05Task2 : Day05Task1
{ }
