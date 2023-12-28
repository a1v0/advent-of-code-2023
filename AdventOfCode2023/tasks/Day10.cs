using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode2023;

public class Day10 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day10Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day10Task2();
}

public class Day10Task1 : BaseTask
{
    public override string Solve()
    {
        int farthestPoint = PipeCircuit.Count / 2;
        return farthestPoint.ToString();
    }

    private List<Pipe> GetPipeCircuit()
    {
        var pipeCircuit = new List<Pipe>() { StartingPoint };
        ExploreCircuit(pipeCircuit);
        return pipeCircuit;
    }

    private Pipe GetStartingPoint()
    {
        (int x, int y) = GetStartingCoordinates();
        char value = GetStartingValue(x, y);
        return new Pipe(value, x, y);
    }

    private char GetStartingValue(int x, int y)
    {
        // this whole method feels ultra clunky
        // needs a refactor, though I can't think,
        // for now, how else to do it.
        char northernValue = GetNorthernValue(x, y),
             southernValue = GetSouthernValue(x, y),
             easternValue = GetEasternValue(x, y),
             westernValue = GetWesternValue(x, y);

        string northernConnectors = "|7F",
               southernConnectors = "|LJ",
               easternConnectors = "-J7",
               westernConnectors = "-LF";

        bool northPointsHere = northernConnectors.Contains(northernValue),
             southPointsHere = southernConnectors.Contains(southernValue),
             eastPointsHere = easternConnectors.Contains(easternValue),
             westPointsHere = westernConnectors.Contains(westernValue);

        return FindStartingValue(northPointsHere, southPointsHere, eastPointsHere, westPointsHere);
    }

    private static char FindStartingValue(bool northPointsHere, bool southPointsHere, bool eastPointsHere, bool westPointsHere)
    {
        if (northPointsHere)
        {
            if (southPointsHere) return '|';
            if (eastPointsHere) return 'L';
            if (westPointsHere) return 'J';
        }
        else if (southPointsHere)
        {
            if (eastPointsHere) return 'F';
            if (westPointsHere) return '7';
        }
        else if (eastPointsHere)
        {
            if (westPointsHere) return '-';
        }

        throw new Exception("Unable to determine starting value.");
    }

    private char GetNorthernValue(int x, int y)
    {
        return GetValue(x, y - 1);
    }
    private char GetSouthernValue(int x, int y)
    {
        return GetValue(x, y + 1);
    }
    private char GetEasternValue(int x, int y)
    {
        return GetValue(x + 1, y);
    }
    private char GetWesternValue(int x, int y)
    {
        return GetValue(x - 1, y);
    }

    private char GetValue(int x, int y)
    {
        int maxX = InputRows[0].Length - 1,
            maxY = InputRows.Length - 1;
        bool isOutOfBoundsX = x < 0 || x > maxX,
             isOutOfBoundsY = y < 0 || y > maxY;

        if (isOutOfBoundsX || isOutOfBoundsY) return '.';

        char value = InputRows[y][x];

        if (value == 'S')
        {
            return StartingPoint.Value;
        }

        return InputRows[y][x];
    }

    private (int x, int y) GetStartingCoordinates()
    {
        int x = 0,
            y = 0;

        for (int i = 0; i < InputRows.Length; ++i)
        {
            string currentRow = InputRows[i];
            bool rowHasS = currentRow.Contains('S');
            if (!rowHasS) continue;

            y = i;
            x = currentRow.IndexOf('S');
            break;
        }

        return (x, y);
    }

    private void ExploreCircuit(List<Pipe> pipeCircuit)
    {
        while (true)
        {
            Pipe currentPipe = pipeCircuit.Last();
            Pipe? previousPipe = pipeCircuit.Count > 1 ? pipeCircuit[^2] : null;
            Pipe nextPipe = GetNextPipe(currentPipe, previousPipe);

            bool isStart = nextPipe.X == StartingPoint.X && nextPipe.Y == StartingPoint.Y;
            if (isStart) break;

            pipeCircuit.Add(nextPipe);
        }
    }

    private Pipe GetNextPipe(Pipe currentPipe, Pipe? previousPipe)
    {
        (int x, int y) neighbour1,
                  neighbour2;

        switch (currentPipe.Value)
        {
            // no need to handle out-of-bounds exceptions here because we can assume the input is accurate
            case '|':
                neighbour1 = (currentPipe.X, currentPipe.Y - 1);
                neighbour2 = (currentPipe.X, currentPipe.Y + 1);
                break;
            case '-':
                neighbour1 = (currentPipe.X - 1, currentPipe.Y);
                neighbour2 = (currentPipe.X + 1, currentPipe.Y);
                break;
            case 'L':
                neighbour1 = (currentPipe.X, currentPipe.Y - 1);
                neighbour2 = (currentPipe.X + 1, currentPipe.Y);
                break;
            case 'J':
                neighbour1 = (currentPipe.X, currentPipe.Y - 1);
                neighbour2 = (currentPipe.X - 1, currentPipe.Y);
                break;
            case '7':
                neighbour1 = (currentPipe.X - 1, currentPipe.Y);
                neighbour2 = (currentPipe.X, currentPipe.Y + 1);
                break;
            case 'F':
                neighbour1 = (currentPipe.X + 1, currentPipe.Y);
                neighbour2 = (currentPipe.X, currentPipe.Y + 1);
                break;
            default:
                throw new Exception("Unable to identify neighbours.");
        }

        int nextX = neighbour1.x,
            nextY = neighbour1.y;

        if (previousPipe is not null)
        {
            bool neighbour1IsPrevious = neighbour1.x == previousPipe.X && neighbour1.y == previousPipe.Y;
            if (neighbour1IsPrevious)
            {
                nextX = neighbour2.x;
                nextY = neighbour2.y;
            }
        }

        char nextValue = GetValue(nextX, nextY);
        return new Pipe(nextValue, nextX, nextY);
    }

    private List<Pipe>? _pipeCircuit;
    protected List<Pipe> PipeCircuit
    {
        get
        {
            _pipeCircuit ??= GetPipeCircuit();
            return _pipeCircuit;
        }
    }

    private Pipe? _startingPoint;
    private Pipe StartingPoint
    {
        get
        {
            _startingPoint ??= GetStartingPoint();
            return _startingPoint;
        }
    }
}

public class Day10Task2 : Day10Task1
{
    public override string Solve()
    {
        CountInternalPipes();
        return InternalCount.ToString();
    }

    private void CountInternalPipes()
    {
        for (int i = 0; i < CircuitMap.GetLength(0); ++i)
        {
            ReadRow(i);
        }
    }

    private void ReadRow(int row)
    {
        var toggle = new Toggle();
        int? previousValue = null;

        for (int column = 0; column < CircuitMap.GetLength(1); ++column)
        {
            int? currentValue = CircuitMap[row, column];
            bool isInternal = toggle.State > 0;

            if (currentValue is null)
            {
                previousValue = null;
                if (!isInternal) continue;
                ++InternalCount;
            }

            else if (currentValue == '|')
            {

                // - toggle state
                // - set previous value to current value
            }

            else if (currentValue == '-')
            {
                // - set previous value to current value
            }

            else
            {
                // if corner piece (F7JL):
                // - find way to keep the state of a horizontal section
                //   - a separate toggle could do it: -1 by default, 1 when open
                //   - a separate variable would be needed to keep track of suitable end values (i.e. a value that wouldn't warrant a change of open/closed state)
                // - if previous value is not null, check whether it's adjacent to current (this bit might not even be necessary)
                //   - toggle state if not adjacent
                // - set previous value to current value
            }
        }
    }

    private int _internalCount = 0;
    private int InternalCount
    {
        get
        {
            return _internalCount;
        }
        set
        {
            _internalCount = value;
        }
    }

    private int?[,]? _circuitMap;
    private int?[,] CircuitMap
    {
        get
        {
            _circuitMap ??= GetCircuitMap();
            return _circuitMap;
        }
    }

    private int?[,] GetCircuitMap()
    {
        int rowLength = InputRows[0].Length,
            columnHeight = InputRows.Length;

        int?[,] circuitMap = new int?[columnHeight, rowLength];

        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        //
        // this bit might not be necessary:
        // the values are probably all null by default
        // see if you get away with deleting it once the task is complete
        for (int i = 0; i < circuitMap.GetLength(0); ++i)
        {
            for (int j = 0; j < circuitMap.GetLength(1); ++j)
            {
                circuitMap[i, j] = null;
            }
        }

        PopulateCircuitMap(circuitMap);

        return circuitMap;
    }

    private void PopulateCircuitMap(int?[,] circuitMap)
    {
        for (int i = 0; i < PipeCircuit.Count; ++i)
        {
            int x = PipeCircuit[i].X,
                y = PipeCircuit[i].Y;

            circuitMap[y, x] = i;
        }
    }
}

public class Pipe
{
    public Pipe(char value, int x, int y)
    {
        _value = value;
        _x = x;
        _y = y;
    }

    private readonly int _x;
    public int X
    {
        get
        {
            return _x;
        }
    }

    private readonly int _y;
    public int Y
    {
        get
        {
            return _y;
        }
    }

    private readonly char _value;
    public char Value
    {
        get
        {
            return _value;
        }
    }
}

/// <summary>
/// Default state is -1 (closed/off); the opposite state is 1 (open/on). Use the `Change` method to switch between the two states.
/// </summary>
public class Toggle
{
    public Toggle()
    {
        _state = -1;
    }

    private int _state;
    public int State
    {
        get
        {
            return _state;
        }
    }

    public void Change()
    {
        _state *= -1;
    }
}