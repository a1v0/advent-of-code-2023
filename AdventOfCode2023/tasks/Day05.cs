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
        long[] locationNumbers = GetLocationNumbers();
        long smallestLocationNumber = locationNumbers.Min();
        return smallestLocationNumber.ToString();
    }

    private long[] GetLocationNumbers()
    {
        long[] locationNumbers = new long[Seeds.Length];

        for (int i = 0; i < Seeds.Length; ++i)
        {
            long currentSeed = Seeds[i];
            locationNumbers[i] = GetLocationNumber(currentSeed);
        }

        return locationNumbers;
    }

    private long GetLocationNumber(long seed)
    {
        long currentTranslation = seed;

        foreach (AlmanacMap map in Maps)
        {
            long newTranslation = TranslateValue(currentTranslation, map);
            currentTranslation = newTranslation;
        }

        return currentTranslation;
    }

    private static long TranslateValue(long sourceValue, AlmanacMap map)
    {
        long translatedValue = sourceValue;

        foreach ((long sourceStart, long destinationStart, long length) range in map.Ranges)
        {
            if (sourceValue < range.sourceStart) break;

            bool numberWithinRange = IsNumberWithinRange(sourceValue, range);
            if (!numberWithinRange) continue;

            long positionInRange = Math.Abs(range.sourceStart - sourceValue);
            translatedValue = range.destinationStart + positionInRange;
            break;
        }

        return translatedValue;
    }

    private static bool IsNumberWithinRange(long value, (long sourceStart, long, long length) range)
    {
        long min = range.sourceStart,
             max = min + range.length - 1;

        if (value < min) return false;
        if (value > max) return false;
        return true;
    }

    private object[]? _maps;

    private object[] Maps
    {
        get
        {
            _maps ??= GetMaps();
            return _maps;
        }
    }

    private object[] GetMaps()
    {
        // I honestly don't know whether using "object" here is a bodge or best practice.
        // It feels like a bodge but I can't think of a better, non-clunky way to do it.
        var maps = new object[UnprocessedMaps.Length];

        for (int i = 0; i < maps.Length; ++i)
        {
            maps[i] = new AlmanacMap(UnprocessedMaps[i]);
        }

        return maps;
    }

    private string[]? _unprocessedMaps;

    private string[] UnprocessedMaps
    {
        get
        {
            string[] maps = Input.Split("\n\n");
            string[] mapsWithoutTitles = RemoveTitles(maps[1..]);
            _unprocessedMaps ??= mapsWithoutTitles; // 0 would be the seeds
            return _unprocessedMaps;
        }
    }

    private static string[] RemoveTitles(string[] mapsWithTitle)
    {
        string[] mapsWithoutTitle = new string[mapsWithTitle.Length];

        for (int i = 0; i < mapsWithTitle.Length; ++i)
        {
            string[] splitMap = mapsWithTitle[i].Split(":\n");
            mapsWithoutTitle[i] = splitMap[1];
        }

        return mapsWithoutTitle;
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
{
    // 
    // parse seeds as tuples (start, length)
    // loop over seed ranges
    // - parse each seed within range as before
    // - store location numbers on a per-range basis
    // - find lowest location per range and store in an array
    // return lowest location from lowest-location array
    // 
    // quite a lot needs updating, especially since the data type of Seeds is changing
    // I'll do my best not to cause too much repetition
    // 

    private (int, int)[]? _seedRanges;
    private (int start, int length)[] SeedRanges
    {
        get
        {
            _seedRanges ??= ParseSeedRanges();
            return _seedRanges;
        }
    }

    private (int, int)[] ParseSeedRanges()
    {
        string seedsRow = InputRows[0];
        string[] splitRow = seedsRow.Split(' ');
        (int, int)[] seedRanges = ExtractSeedRanges(splitRow);
        return seedRanges;
        // remove seed row's title
        // split by " "
        // for loop where i+=2
        // generate tuple
    }
}

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

        return (source, destination, length); // NOTE: this is a different order to what is given by the input
    }
}