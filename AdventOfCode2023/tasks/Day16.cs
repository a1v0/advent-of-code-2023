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
        // create Path class:
        // - direction
        // - current position (int x, int y)
        // 
        // parse input as dictionary:
        // - key: tuple of coordinates (x, y)
        // - value: Tile
        // 
        // create List of paths
        // create one blank path starting at 0,0, heading east
        // loop over all paths (loop length will continue to grow over time)
        // 
        // while loop to keep going until path is exhausted:
        // if current tile's approachFromNorth property is true, break loop 
        // set tile's relevant "approachedFrom" property
        // set current tile's energised property to true
        // if direction is east, add 1 to current x
        // if new tile is /, change direction accordingly
        // if tile is a splitter and perpendicular to current direction, go in one direction and create a new path that goes in the other
        // 
        // loop through dictionary of tiles and count the amount of energised ones
        // stringify and return
    }
}

public class Day16Task2 : Day16Task1
{ }

public class Path
{

}

public class Tile
{
    public Tile(string type)
    {
        Type = type;
    }

    public string Type { get; }
    public bool IsEnergised { get; set; } = false;

    /// <summary>
    /// The order of the values in this array is North, South, East, West.
    /// </summary>
    public bool[] AlreadyApproachedBy { get; } = { false, false, false, false };
}