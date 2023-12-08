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

    private long[]? _seeds;
    private long[] Seeds
    {
        get
        {
            _seeds ??= ParseSeeds();
            return _seeds;
        }
    }

    private long[] ParseSeeds()
    {
        string seedsRow = InputRows[0];
        string[] splitRow = seedsRow.Split(' ');
        long[] seeds = ExtractSeeds(splitRow);
        return seeds;
    }

    private static long[] ExtractSeeds(string[] row)
    {
        var seeds = new List<long>();
        for (int i = 1; i < row.Length; ++i)
        {
            long seed = long.Parse(row[i]);
            seeds.Add(seed);
        }
        return seeds.ToArray();
    }
}

public class Day05Task2 : Day05Task1
{ }

public class AlmanacMap
{
    public AlmanacMap(string mapInput)
    {
        _mapInput = mapInput;
        // parse input into rows
        // parse rows tuples
        // add to array of tuples
        // sort array
        // store in a public property
    }

    private readonly string _mapInput;

    private (int, int, int)[]? _ranges;
    private (int, int, int)[] Ranges
    {
        get
        {
            _ranges ??= GetRanges();
            return _ranges;
        }
    }
}