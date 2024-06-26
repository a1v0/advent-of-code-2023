using System.Net;

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

                straightPath.HeuristicValue = CalculateHeuristicValue(straightPath);
                CityBlocks[(newX, newY)].Visited[(path.Direction, (byte)(path.DistanceTravelledInDirection + 1))] = path.TotalHeatLoss;

                nextRoundOfPaths.Add(straightPath);
            }

            if (canGoAntiClockwise)
            {
                byte newDirection = CruciblePath.ConvertDirection(path.Direction, -1);
                (int newX, int newY) = GetNextCoordinates(path.X, path.Y, newDirection);
                int newHeatLoss = path.TotalHeatLoss + CityBlocks[(newX, newY)].HeatLoss;
                var antiClockwisePath = new CruciblePath(newX, newY, newHeatLoss, newDirection);

                antiClockwisePath.HeuristicValue = CalculateHeuristicValue(antiClockwisePath);
                CityBlocks[(newX, newY)].Visited[(newDirection, 1)] = path.TotalHeatLoss;

                nextRoundOfPaths.Add(antiClockwisePath);
            }

            if (canGoClockwise)
            {
                byte newDirection = CruciblePath.ConvertDirection(path.Direction, 1);
                (int newX, int newY) = GetNextCoordinates(path.X, path.Y, newDirection);
                int newHeatLoss = path.TotalHeatLoss + CityBlocks[(newX, newY)].HeatLoss;
                var clockwisePath = new CruciblePath(newX, newY, newHeatLoss, newDirection);

                clockwisePath.HeuristicValue = CalculateHeuristicValue(clockwisePath);
                CityBlocks[(newX, newY)].Visited[(newDirection, 1)] = path.TotalHeatLoss;

                nextRoundOfPaths.Add(clockwisePath);
            }
        }

        nextRoundOfPaths.Sort();
        return nextRoundOfPaths;
    }

    private double CalculateHeuristicValue(CruciblePath path)
    {
        double distanceFromGoal = GetDistanceFromGoal(path.X, path.Y) + 1; // the +1 prevents this method from return 0 when the distance is 0
        double heuristicValue = distanceFromGoal * path.TotalHeatLoss;
        return heuristicValue;
    }

    private double GetDistanceFromGoal(int x, int y)
    {
        int a = ExitCoordinates.X - x,
            b = ExitCoordinates.Y - y;

        double pythagoras = Math.Pow(a, 2) + Math.Pow(b, 2);
        double c = Math.Sqrt(pythagoras);
        return c;
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
        if (tileHasBeenVisited)
        {
            int lowestHeatLossSoFar = CityBlocks[nextCoordinates].Visited[visitationKey];
            bool currentHeatLossIsLower = path.TotalHeatLoss <= lowestHeatLossSoFar;
            return currentHeatLossIsLower;
        }

        return true;
    }

    private bool CanGoAntiClockwise(CruciblePath path)
    {
        byte newDirection = CruciblePath.ConvertDirection(path.Direction, -1);
        (int, int) nextCoordinates = GetNextCoordinates(path.X, path.Y, newDirection);

        bool coordinatesOutOfBounds = !CityBlocks.ContainsKey(nextCoordinates);
        if (coordinatesOutOfBounds) return false;

        var visitationKey = (newDirection, (byte)1);
        bool tileHasBeenVisited = CityBlocks[nextCoordinates].Visited.ContainsKey(visitationKey);
        if (tileHasBeenVisited)
        {
            int lowestHeatLossSoFar = CityBlocks[nextCoordinates].Visited[visitationKey];
            bool currentHeatLossIsLower = path.TotalHeatLoss <= lowestHeatLossSoFar;
            return currentHeatLossIsLower;
        }

        return true;
    }

    private bool CanGoClockwise(CruciblePath path)
    {
        byte newDirection = CruciblePath.ConvertDirection(path.Direction, 1);
        (int, int) nextCoordinates = GetNextCoordinates(path.X, path.Y, newDirection);

        bool coordinatesOutOfBounds = !CityBlocks.ContainsKey(nextCoordinates);
        if (coordinatesOutOfBounds) return false;

        var visitationKey = (newDirection, (byte)1);
        bool tileHasBeenVisited = CityBlocks[nextCoordinates].Visited.ContainsKey(visitationKey);
        if (tileHasBeenVisited)
        {
            int lowestHeatLossSoFar = CityBlocks[nextCoordinates].Visited[visitationKey];
            bool currentHeatLossIsLower = path.TotalHeatLoss <= lowestHeatLossSoFar;
            return currentHeatLossIsLower;
        }

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
        byte initialHeatLoss = (byte)char.GetNumericValue(InputRows[0][0]);

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
                byte heatLoss = (byte)char.GetNumericValue(InputRows[y][x]);
                var cityBlock = new CityBlock(x, y, heatLoss);
                cityBlocks.Add((x, y), cityBlock);
            }
        }

        return cityBlocks;
    }
}

public class Day17Task2 : Day17Task1
{ }
