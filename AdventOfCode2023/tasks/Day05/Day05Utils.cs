namespace AdventOfCode2023;

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