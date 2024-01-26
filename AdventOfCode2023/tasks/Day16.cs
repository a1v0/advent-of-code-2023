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
        for (int i = 0; i < LightBeams.Count; ++i)
        {
            LightBeam lightBeam = LightBeams[i];
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
            currentTile.DirectionsUsed[lightBeam.Direction] = true;
            currentTile.Energise();

            bool directionShouldChange = currentTile.Type != '.';
            if (directionShouldChange) ChangeDirection(lightBeam, currentTile.Type);

            lightBeam.Move();
        }
    }

    private void ChangeDirection(LightBeam lightBeam, char tileType)
    {
        string splitters = "-|";
        string diagonals = "/\\";

        bool isSplitter = splitters.Contains(tileType);
        bool isDiagonal = diagonals.Contains(tileType);

        if (isSplitter)
        {
            ChangeDirectionSplitter(lightBeam, tileType);
        }
        else if (isDiagonal)
        {
            ChangeDirectionDiagonal(lightBeam, tileType);
        }
        else
        {
            throw new Exception($"Tile type {tileType} not recognised.");
        }
    }

    private void ChangeDirectionSplitter(LightBeam lightBeam, char tileType)
    {
        bool noNeedToSplit = lightBeam.IsNoNeedToSplit(tileType);
        if (noNeedToSplit) return;

        LightBeam secondLightBeam = lightBeam.GetSplitBeam();

        if (tileType == '-')
        {
            lightBeam.Direction = 2;
            secondLightBeam.Direction = 3;
        }
        else if (tileType == '|')
        {
            lightBeam.Direction = 0;
            secondLightBeam.Direction = 1;
        }

        secondLightBeam.Move();
        LightBeams.Add(secondLightBeam);
    }

    private static void ChangeDirectionDiagonal(LightBeam lightBeam, char tileType)
    {
        // making a massive `switch` is probably awful, but all other methods seem equally inelegant...
        (char, byte) typeDirectionCombo = (tileType, lightBeam.Direction);

        switch (typeDirectionCombo)
        {
            case ('\\', 0):
                lightBeam.Direction = 3;
                break;
            case ('\\', 1):
                lightBeam.Direction = 2;
                break;
            case ('\\', 2):
                lightBeam.Direction = 1;
                break;
            case ('\\', 3):
                lightBeam.Direction = 0;
                break;
            case ('/', 0):
                lightBeam.Direction = 2;
                break;
            case ('/', 1):
                lightBeam.Direction = 3;
                break;
            case ('/', 2):
                lightBeam.Direction = 0;
                break;
            case ('/', 3):
                lightBeam.Direction = 1;
                break;
            default:
                throw new Exception($"Invalid combination of tile and direction: ({tileType}, {lightBeam.Direction}).");
        }
    }

    private bool IsPathExhausted(LightBeam lightBeam)
    {
        (int, int) coordinates = (lightBeam.X, lightBeam.Y);

        bool coordinatesNotFound = !Tiles.ContainsKey(coordinates);
        if (coordinatesNotFound) return true;

        Tile tile = Tiles[coordinates];
        bool pathExhausted = tile.DirectionsUsed[lightBeam.Direction];
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
{
    public override string Solve()
    {
        EmitLightBeamsFromAllEntryPoints();
        int highestEnergisedTotal = EnergisedTotals.Max();
        return highestEnergisedTotal.ToString();
    }

    private void EmitLightBeamsFromAllEntryPoints()
    {
        // loop over all entry points
        // reset board every time
        // add total to list of totals
    }

    private List<int> EnergisedTotals { get; } = new List<int>();

    private List<LightBeam>? _entryPoints;
    private List<LightBeam> EntryPoints
    {
        get
        {
            _entryPoints ??= GetAllEntryPoints();
            return _entryPoints;
        }
    }

    private List<LightBeam> GetAllEntryPoints()
    {
        var entryPoints = new List<LightBeam>();

        List<LightBeam> entryPointsFromNorth = GetEntryPointsNorth();
        entryPoints.AddRange(entryPointsFromNorth);

        List<LightBeam> entryPointsFromSouth = GetEntryPointsSouth();
        entryPoints.AddRange(entryPointsFromSouth);

        List<LightBeam> entryPointsFromEast = GetEntryPointsEast();
        entryPoints.AddRange(entryPointsFromEast);

        List<LightBeam> entryPointsFromWest = GetEntryPointsWest();
        entryPoints.AddRange(entryPointsFromWest);

        return entryPoints;
    }

    private List<LightBeam> GetEntryPointsNorth()
    {
        var entryPoints = new List<LightBeam>();
        const byte direction = 1;

        for (int x = 0; x < InputRows[0].Length; ++x)
        {
            const int y = 0;
            var entryPoint = new LightBeam(x, y, direction);
            entryPoints.Add(entryPoint);
        }

        return entryPoints;
    }

    private List<LightBeam> GetEntryPointsSouth()
    {
        var entryPoints = new List<LightBeam>();
        const byte direction = 0;

        for (int x = 0; x < InputRows[0].Length; ++x)
        {
            int y = InputRows.Length - 1;
            var entryPoint = new LightBeam(x, y, direction);
            entryPoints.Add(entryPoint);
        }

        return entryPoints;
    }

    private List<LightBeam> GetEntryPointsEast()
    {
        var entryPoints = new List<LightBeam>();
        const byte direction = 3;

        for (int y = 0; y < InputRows.Length; ++y)
        {
            int x = InputRows[0].Length - 1;

            var entryPoint = new LightBeam(x, y, direction);
            entryPoints.Add(entryPoint);
        }

        return entryPoints;
    }

    private List<LightBeam> GetEntryPointsWest()
    {
        var entryPoints = new List<LightBeam>();
        const byte direction = 2;

        for (int y = 0; y < InputRows.Length; ++y)
        {
            const int x = 0;

            var entryPoint = new LightBeam(x, y, direction);
            entryPoints.Add(entryPoint);
        }

        return entryPoints;
    }
}

public class LightBeam
{
    public LightBeam(int x, int y, byte direction)
    {
        X = x;
        Y = y;
        Direction = direction;
    }

    public bool IsNoNeedToSplit(char tileType)
    {
        if (tileType == '-') return Direction >= 2;
        return Direction <= 1;

    }

    public LightBeam GetSplitBeam()
    {
        return new LightBeam(X, Y, Direction);
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
    public bool[] DirectionsUsed { get; } = { false, false, false, false };
}