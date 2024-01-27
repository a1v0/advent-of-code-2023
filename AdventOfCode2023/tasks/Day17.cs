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
        // parse input as dictionary of CityBlocks, where key is tuple of coordinates
        // create list of paths
        // - create two starter paths, one going right, one going down
        // 
        // heuristic: Pythagorean distance from goal * current TotalHeatLoss value?
        // 
        // while loop that runs until the coordinates of the first item in the list are those of the end point
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
        // 
        // retrieve TotalHeatLoss of winner
        // stringify and return
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
    // - int X: current coordinate
    // - int Y: current coordinate
    // - byte Direction
    // - byte DistanceTravelledInDirection
    // - int TotalHeatLoss
    // - int HeuristicValue
}