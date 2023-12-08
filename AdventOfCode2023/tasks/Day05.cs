using System.Collections;

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
    }

    private readonly string _mapInput;
    private string MapInput
    {
        get
        {
            return _mapInput;
        }
    }

    private (long, long, long)[]? _ranges;
    public (long, long, long)[] Ranges
    {
        get
        {
            _ranges ??= GetRanges();
            return _ranges;
        }
    }

    private (long, long, long)[] GetRanges()
    {
        string[] mapRows = MapInput.Split('\n');
        var ranges = new (long, long, long)[mapRows.Length];

        for (int i = 0; i < ranges.Length; ++i)
        {
            string currentRow = mapRows[i];
            ranges[i] = ParseRange(currentRow);
        }

        Array.Sort(ranges);

        return ranges;
    }

    private static (long, long, long) ParseRange(string row)
    {
        string[] rowContents = row.Split(' ');
        long destination = long.Parse(rowContents[0]),
             source = long.Parse(rowContents[1]),
             length = long.Parse(rowContents[2]);

        return (destination, source, length);
    }
}