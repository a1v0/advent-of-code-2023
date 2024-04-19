namespace AdventOfCode2023;

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
    public Dictionary<(byte, byte), int> Visited { get; } = new();
}

public class CruciblePath : IComparable
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

    public double HeuristicValue { get; set; } = 0;

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

    public int CompareTo(object? comparison)
    {
        if (comparison is null) return 1;

        CruciblePath comparisonPath = (CruciblePath)comparison;
        double comparisonResult = HeuristicValue - comparisonPath.HeuristicValue;

        if (comparisonResult < 0) return -1;
        return 1;
    }
}