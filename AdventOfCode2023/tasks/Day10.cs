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
        var pipeCircuit = new List<Pipe>();
        Pipe startingPoint = GetStartingPoint();
        pipeCircuit.Add(startingPoint);

        ExploreCircuit(pipeCircuit);

        return pipeCircuit;
    }

    private void ExploreCircuit(List<Pipe> pipeCircuit)
    {
        Pipe startingPoint = pipeCircuit.First();
        while (true)
        {
            Pipe nextPipe = GetNextPipe();
            // look in all four directions to find a direction to go in
            // - switch to find next coordinate
            // - e.g. if current value is L, look in the appropriate direction to find next one
            // add new coordinate to end of list
            // ensure you handle the possibility of an out-of-bounds exception in X and Y
            // add node to list and follow the path

            bool isStart = nextPipe.X == startingPoint.X && nextPipe.Y == startingPoint.Y;
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
}

public class Day10Task2 : Day10Task1
{ }

class Pipe
{
    public Pipe(string value, int x, int y)
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

    private readonly string _value;
    public string Value
    {
        get
        {
            return _value;
        }
    }
}