namespace AdventOfCode2023;

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