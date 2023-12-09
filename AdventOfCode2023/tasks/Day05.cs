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

    protected long GetLocationNumber(long seed)
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
    public override string Solve()
    {
        long[] lowestLocationNumbers = GetLowestLocationNumbers();
        long smallestLocationNumber = lowestLocationNumbers.Min();
        return smallestLocationNumber.ToString();
    }

    private long[] GetLowestLocationNumbers()
    {
        long[] lowestLocationNumbers = new long[SeedRanges.Length];

        for (int i = 0; i < SeedRanges.Length; ++i)
        {
            long[] locationNumbers = GetLocationNumbers(i);
            lowestLocationNumbers[i] = locationNumbers.Min();
            Console.WriteLine($"Seed range {i} processed.\n");
        }

        return lowestLocationNumbers;
    }

    private long[] GetLocationNumbers(int seedRangeIndex)
    {
        (long rangeStart, long rangeLength) = SeedRanges[seedRangeIndex];
        long maxSeedNumber = rangeStart + rangeLength - 1;
        var locationNumbers = new List<long>();

        for (long i = rangeStart; i <= maxSeedNumber; ++i)
        {
            long currentSeed = i;
            long locationNumber = GetLocationNumber(currentSeed);
            locationNumbers.Add(locationNumber);

            if (i % 1000000 == 0)
            {
                Console.WriteLine($"Seed {i}/{maxSeedNumber} processed.");
            }
        }

        return locationNumbers.ToArray();
    }

    private (long, long)[]? _seedRanges;
    private (long start, long length)[] SeedRanges
    {
        get
        {
            _seedRanges ??= ParseSeedRanges();
            return _seedRanges;
        }
    }

    private (long, long)[] ParseSeedRanges()
    {
        string seedsRow = InputRows[0];
        string[] splitRow = seedsRow.Split(' ');
        (long, long)[] seedRanges = ExtractSeedRanges(splitRow[1..]);
        return seedRanges;
    }

    private static (long, long)[] ExtractSeedRanges(string[] seedNumbers)
    {
        (long, long)[] seedRanges = new (long, long)[seedNumbers.Length / 2];

        for (int i = 0, j = 0; i < seedRanges.Length; ++i, j += 2)
        {
            long rangeStart = long.Parse(seedNumbers[j]);
            long rangeLength = long.Parse(seedNumbers[j + 1]);
            seedRanges[i] = (rangeStart, rangeLength);
        }

        return seedRanges;
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