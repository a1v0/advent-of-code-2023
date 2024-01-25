namespace AdventOfCode2023;

public class Day16 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day16Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day16Task2();
}

public class Day16Task1 : BaseTask
{
    public override string Solve()
    {
        EmitLightBeams();
        int totalEnergisedTiles = SumEnergisedTiles();
        return totalEnergisedTiles.ToString();
    }

    private void EmitLightBeams()
    {
        foreach (LightBeam lightBeam in LightBeams)
        {
            EmitLightBeam(lightBeam);
        }
    }

    private void EmitLightBeam(LightBeam lightBeam)
    {
        while (true) // keeps going until beam's path is exhausted
        {
            bool pathExhausted = IsPathExhausted(lightBeam);
            if (pathExhausted) break;
            // guard clause: check whether current tile has been visited from this direction
            // set approachedFrom property
            // energise tile
            // 
            // if tile isn't '.', then change direction
            // 
            // find next tile:
            // - alter coordinates based on direction
        }
    }

    private int SumEnergisedTiles()
    {
        int total = 0;

        foreach (KeyValuePair<(int, int), Tile> tile in Tiles)
        {
            bool isEnergised = tile.Value.IsEnergised;
            if (isEnergised) ++total;
        }

        return total;
    }

    private List<LightBeam> LightBeams { get; } = new List<LightBeam>() { new LightBeam(0, 0, 2) };

    private Dictionary<(int, int), Tile>? _tiles;
    private Dictionary<(int, int), Tile> Tiles
    {
        get
        {
            _tiles ??= GetTiles();
            return _tiles;
        }
    }

    private Dictionary<(int, int), Tile> GetTiles()
    {
        var tiles = new Dictionary<(int, int), Tile>();

        for (int i = 0; i < InputRows.Length; ++i)
        {
            for (int j = 0; j < InputRows[0].Length; ++j)
            {
                char tileType = InputRows[i][j];
                var coordinates = (j, i);
                var tile = new Tile(tileType);
                tiles.Add(coordinates, tile);
            }
        }

        return tiles;
    }
}

public class Day16Task2 : Day16Task1
{ }

public class LightBeam
{
    public LightBeam(int x, int y, byte direction)
    {
        X = x;
        Y = y;
        Direction = direction;
    }

    public int X { get; set; }
    public int Y { get; set; }

    /// <summary>
    /// Direction is 0-3; 0 = North, 1 = South, 2 = East, 3 = West
    /// </summary>
    public byte Direction { get; set; }
}

public class Tile
{
    public Tile(char type)
    {
        Type = type;
    }

    public char Type { get; }
    public bool IsEnergised { get; set; } = false;

    /// <summary>
    /// The order of the values in this array is North, South, East, West.
    /// </summary>
    public bool[] AlreadyApproachedBy { get; } = { false, false, false, false };
}