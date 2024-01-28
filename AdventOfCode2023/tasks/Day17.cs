namespace AdventOfCode2023;

public class Day17 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day17Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day17Task2();
}

public class Day17Task1 : BaseTask
{
    public override string Solve()
    {
        CruciblePath PathWithLowestHeatLoss = GetPathWithLowestHeatLoss();
        int lowestHeatLoss = PathWithLowestHeatLoss.TotalHeatLoss;
        return lowestHeatLoss.ToString();
    }

    private CruciblePath GetPathWithLowestHeatLoss()
    {
        while (true)
        {
            bool exitCondition = CruciblePaths[0].X == ExitCoordinates.X && CruciblePaths[0].Y == ExitCoordinates.Y;
            if (exitCondition) break;

            List<CruciblePath> nextRoundOfPaths = GetNextRoundOfPaths();
            CruciblePaths = nextRoundOfPaths;
        }

        return CruciblePaths[0];
    }

    private List<CruciblePath> GetNextRoundOfPaths()
    {
        var nextRoundOfPaths = new List<CruciblePath>();

        foreach (CruciblePath path in CruciblePaths)
        {
            bool canGoStraight = CanGoStraight(path),
                 canGoAntiClockwise = CanGoAntiClockwise(path),
                 canGoClockwise = CanGoClockwise(path);

            if (canGoStraight)
            {
                (int newX, int newY) = GetNextCoordinates(path.X, path.Y, path.Direction);
                int newHeatLoss = path.TotalHeatLoss + CityBlocks[(newX, newY)].HeatLoss;
                var straightPath = new CruciblePath(newX, newY, newHeatLoss, path.Direction, (byte)(path.DistanceTravelledInDirection + 1));
                nextRoundOfPaths.Add(straightPath);
            }

            if (canGoAntiClockwise)
            {
                byte newDirection = CruciblePath.ConvertDirection(path.Direction, -1);
                (int newX, int newY) = GetNextCoordinates(path.X, path.Y, newDirection);
                int newHeatLoss = path.TotalHeatLoss + CityBlocks[(newX, newY)].HeatLoss;
                var antiClockwisePath = new CruciblePath(newX, newY, newHeatLoss, newDirection);
                nextRoundOfPaths.Add(antiClockwisePath);
            }

            if (canGoClockwise)
            {
                byte newDirection = CruciblePath.ConvertDirection(path.Direction, 1);
                (int newX, int newY) = GetNextCoordinates(path.X, path.Y, newDirection);
                int newHeatLoss = path.TotalHeatLoss + CityBlocks[(newX, newY)].HeatLoss;
                var clockwisePath = new CruciblePath(newX, newY, newHeatLoss, newDirection);
                nextRoundOfPaths.Add(clockwisePath);
            }
        }
        // loop through all current paths
        // - create new paths based on directions you're currently allowed to go in
        //   - blocks where Visited == True to be ignored       <= keep an eye on this. This bit only works if the heuristic is any good
        //     - this is too simple an approach
        //     - I won't be able to explore all four directions from a specific tile, meaning not all avenues will have been explored
        //     - I'll need to implement a visitation system like on Day 16:
        //       - if I've already visited via my current direction AND have the same amount of steps in the same direction as before, then don't continue
        // - update heat loss totals accordingly
        // - calculate heuristic value
        // - set CityBlock to visited
        // - add new path to list of updated paths
        // 
        // sort updated paths list according to heuristic
        // overwrite main paths list with updated list and loop again
        // 
        // heuristic: Pythagorean distance from goal * current TotalHeatLoss value?
    }

    private bool CanGoStraight(CruciblePath path)
    {
        bool mustChangeDirection = path.DistanceTravelledInDirection >= 3;
        if (mustChangeDirection) return false;

        (int, int) nextCoordinates = GetNextCoordinates(path.X, path.Y, path.Direction);

        bool coordinatesOutOfBounds = !CityBlocks.ContainsKey(nextCoordinates);
        if (coordinatesOutOfBounds) return false;

        var visitationKey = (path.Direction, (byte)(path.DistanceTravelledInDirection + 1));
        bool tileHasBeenVisited = CityBlocks[nextCoordinates].Visited.ContainsKey(visitationKey);
        if (tileHasBeenVisited) return false;

        return true;
    }

    private bool CanGoAntiClockwise(CruciblePath path)
    {
        byte newDirection = CruciblePath.ConvertDirection(path.Direction, -1);
        (int, int) nextCoordinates = GetNextCoordinates(path.X, path.Y, newDirection);

        bool coordinatesOutOfBounds = !CityBlocks.ContainsKey(nextCoordinates);
        if (coordinatesOutOfBounds) return false;

        var visitationKey = (path.Direction, (byte)1);
        bool tileHasBeenVisited = CityBlocks[nextCoordinates].Visited.ContainsKey(visitationKey);
        if (tileHasBeenVisited) return false;

        return true;
    }

    private static (int, int) GetNextCoordinates(int x, int y, byte direction)
    {
        switch (direction)
        {
            case 0:
                return (x, y - 1);
            case 1:
                return (x, y + 1);
            case 2:
                return (x + 1, y);
            case 3:
                return (x - 1, y);
            default:
                throw new Exception("Invalid direction given.");
        }
    }

    private List<CruciblePath>? _cruciblePaths;
    private List<CruciblePath> CruciblePaths
    {
        get
        {
            _cruciblePaths ??= GetDefaultCruciblePaths();
            return _cruciblePaths;
        }

        set
        {
            _cruciblePaths = value;
        }
    }

    private List<CruciblePath> GetDefaultCruciblePaths()
    {
        var defaultPaths = new List<CruciblePath>();
        byte initialHeatLoss = (byte)InputRows[0][0];

        var pathToEast = new CruciblePath(0, 0, initialHeatLoss, 2);
        defaultPaths.Add(pathToEast);

        var pathToSouth = new CruciblePath(0, 0, initialHeatLoss, 1);
        defaultPaths.Add(pathToSouth);

        return defaultPaths;
    }

    private (int, int)? _exitCoordinates;
    private (int X, int Y) ExitCoordinates
    {
        get
        {
            _exitCoordinates ??= GetExitCoordinates();
            return ((int, int))_exitCoordinates;
        }
    }

    private (int, int) GetExitCoordinates()
    {
        int x = InputRows[0].Length - 1,
            y = InputRows.Length - 1;

        return (x, y);
    }

    private Dictionary<(int, int), CityBlock>? _cityBlocks;
    private Dictionary<(int, int), CityBlock> CityBlocks
    {
        get
        {
            _cityBlocks ??= GetCityBlocks();
            return _cityBlocks;
        }
    }

    private Dictionary<(int, int), CityBlock> GetCityBlocks()
    {
        var cityBlocks = new Dictionary<(int, int), CityBlock>();

        for (int y = 0; y < InputRows.Length; ++y)
        {
            for (int x = 0; x < InputRows[0].Length; ++x)
            {
                byte heatLoss = (byte)InputRows[y][x];
                var cityBlock = new CityBlock(x, y, heatLoss);
                cityBlocks.Add((x, y), cityBlock);
            }
        }

        return cityBlocks;
    }
}

public class Day17Task2 : Day17Task1
{ }

public class CityBlock
{
    public CityBlock(int x, int y, byte heatLoss)
    {
        X = x;
        Y = y;
        HeatLoss = heatLoss;
    }

    private int X { get; }
    private int Y { get; }

    public byte HeatLoss { get; }

    /// <summary>
    /// Stores the direction of the visitation as well as the DistanceTravelledInDirection at the time of the visitation
    /// </summary>
    public Dictionary<(byte, byte), bool> Visited { get; } = new();
}

public class CruciblePath
{
    public CruciblePath(int x, int y, int totalHeatLoss, byte direction, byte distanceTravelledInDirection = 1)
    {
        X = x;
        Y = y;
        TotalHeatLoss = totalHeatLoss;
        _direction = direction;
        DistanceTravelledInDirection = distanceTravelledInDirection;
    }

    public int X { get; }
    public int Y { get; }

    private readonly byte _direction;

    /// <summary>
    /// Directions can be 0, 1, 2 or 3. These numbers represent North, South, East and West, respectively.
    /// </summary>
    public byte Direction
    {
        get
        {
            return _direction;
        }
    }

    public byte DistanceTravelledInDirection { get; set; }

    public int TotalHeatLoss { get; }

    public int HeuristicValue { get; set; } = 0;

    /// <summary>
    /// If directionOfRotation < 0, returns the value of the next direction anticlockwise. If > 0, it will return the clockwise value.
    /// </summary>
    public static byte ConvertDirection(byte currentDirection, sbyte directionOfRotation)
    {
        if (directionOfRotation == 0) throw new Exception("Direction of rotation must be greater than or less than 0.");

        bool goingClockwise = directionOfRotation > 0;

        if (goingClockwise)
        {
            switch (currentDirection)
            {
                case 0:
                    return 2;
                case 1:
                    return 3;
                case 2:
                    return 1;
                case 3:
                    return 0;
                default:
                    throw new Exception("Invalid direction given.");
            }
        }

        switch (currentDirection)
        {
            case 0:
                return 3;
            case 1:
                return 2;
            case 2:
                return 0;
            case 3:
                return 1;
            default:
                throw new Exception("Invalid direction given.");
        }
    }
}