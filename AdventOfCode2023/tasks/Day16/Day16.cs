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

    protected void EmitLightBeams()
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

    protected int SumEnergisedTiles()
    {
        int total = 0;

        foreach (KeyValuePair<(int, int), Tile> tile in Tiles)
        {
            bool isEnergised = tile.Value.IsEnergised;
            if (isEnergised) ++total;
        }

        return total;
    }

    protected List<LightBeam> LightBeams { get; set; } = new List<LightBeam>() { new LightBeam(0, 0, 2) };

    protected Dictionary<(int, int), Tile>? _tiles;
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
        foreach (LightBeam entryPoint in EntryPoints)
        {
            _tiles = null; // setting it to null forces the getter to run GetTiles(). Not sure whether this is best practice

            LightBeams = new List<LightBeam>() { entryPoint };

            EmitLightBeams();

            int totalEnergisedTiles = SumEnergisedTiles();
            EnergisedTotals.Add(totalEnergisedTiles);
        }
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
