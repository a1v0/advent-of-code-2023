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
            // heuristic: Pythagorean distance from goal * current TotalHeatLoss value?
            // 
            // create new, empty list of paths to house updated paths
            // loop through all current paths
            // - create new paths based on directions you're currently allowed to go in
            //   - blocks where Visited == True to be ignored       <= keep an eye on this. This bit only works if the heuristic is any good
            // - update heat loss totals accordingly
            // - calculate heuristic value
            // - set CityBlock to visited
            // - add new path to list of updated paths
            // 
            // sort updated paths list according to heuristic
            // overwrite main paths list with updated list and loop again
        }

        return CruciblePaths[0];
    }

    private List<CruciblePath> CruciblePaths { get; set; } = new List<CruciblePath>() { new(0, 0, 2), new(0, 0, 1) };

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

    private bool _visited = false;
    public bool Visited
    {
        get
        {
            return _visited;
        }
    }

    public void Visit()
    {
        _visited = true;
    }
}

public class CruciblePath
{
    public CruciblePath(int x, int y, byte direction, byte distanceTravelledInDirection = 1)
    {
        X = x;
        Y = y;
        _direction = direction;
        DistanceTravelledInDirection = distanceTravelledInDirection;
    }

    public int X { get; }
    public int Y { get; }

    private byte _direction;

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

    public int TotalHeatLoss { get; set; } = 0;

    public int HeuristicValue { get; set; } = 0;
}