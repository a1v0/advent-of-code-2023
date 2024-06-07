namespace AdventOfCode2023;

public class MachinePart
{
    public MachinePart(string machineData)
    {
        (int x, int m, int a, int s) = ParseMachineData(machineData);
        _x = x;
        _m = m;
        _a = a;
        _s = s;
    }

    private readonly int _x;
    public int X
    {
        get;
    }

    private readonly int _m;
    public int M
    {
        get;
    }

    private readonly int _a;
    public int A
    {
        get;
    }

    private readonly int _s;
    public int S
    {
        get;
    }

    private bool? _accepted;
    public bool? Accepted
    {
        get;
    }

    private bool? _rejected;
    public bool? Rejected
    {
        get;
    }

    public void Accept()
    {
        if (Accepted is not null)
        {
            throw new Exception("Cannot accept or reject MachinePart object if it has already been accepted or rejected.");
        }

        _accepted = true;
        _rejected = false;
    }
}

public class Workflow
{

}