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

        throw new Exception("Unable to work out starting value.");
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
        return GetValue(x - 1, y);
    }
    private char GetWesternValue(int x, int y)
    {
        return GetValue(x + 1, y);
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
            Pipe nextPipe = GetNextPipe();
            // use current pipe's value to figure out where the next coordinate is
            // - we'll need access to the previous pipe to make sure we don't go in the wrong direction
            // add new coordinate to end of list
            // ensure you handle the possibility of an out-of-bounds exception in X and Y
            // add node to list and follow the path

            bool isStart = nextPipe.X == StartingPoint.X && nextPipe.Y == StartingPoint.Y;
            if (isStart) break;

            pipeCircuit.Add(nextPipe);
        }
    }

    private List<Pipe>? _pipeCircuit;
    private List<Pipe> PipeCircuit
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
{ }

class Pipe
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