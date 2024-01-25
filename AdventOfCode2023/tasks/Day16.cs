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

            Tile currentTile = Tiles[(lightBeam.X, lightBeam.Y)];
            currentTile.Energise();

            bool directionShouldChange = currentTile.Type != '.';
            if (directionShouldChange) ChangeDirection(lightBeam);

            lightBeam.Move();
        }
    }

    private bool IsPathExhausted(LightBeam lightBeam)
    {
        (int, int) coordinates = (lightBeam.X, lightBeam.Y);

        bool coordinatesNotFound = !Tiles.ContainsKey(coordinates);
        if (coordinatesNotFound) return true;

        Tile tile = Tiles[coordinates];
        bool pathExhausted = tile.AlreadyApproachedBy[lightBeam.Direction];
        return pathExhausted;
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

    public void Move()
    {
        switch (Direction)
        {
            case 0:
                MoveNorth();
                break;
            case 1:
                MoveSouth();
                break;
            case 2:
                MoveEast();
                break;
            case 3:
                MoveWest();
                break;
            default:
                throw new Exception("Unknown direction given.");
        }
    }

    private void MoveNorth()
    {
        --Y;
    }

    private void MoveSouth()
    {
        ++Y;
    }

    private void MoveEast()
    {
        ++X;
    }

    private void MoveWest()
    {
        --X;
    }
}

public class Tile
{
    public Tile(char type)
    {
        Type = type;
    }

    public char Type { get; }

    private bool _isEnergised = false;
    public bool IsEnergised
    {
        get
        {
            return _isEnergised;
        }
    }

    public void Energise()
    {
        _isEnergised = true;
    }

    /// <summary>
    /// The order of the values in this array is North, South, East, West.
    /// </summary>
    public bool[] AlreadyApproachedBy { get; } = { false, false, false, false };
}