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

    private (int, int, int, int) ParseMachineData(string rawMachineData)
    {
        string machineData = rawMachineData
        .Replace("{", "")
        .Replace("}", "");

        string[] dataPoints = machineData.Split(',');

        return (
            GetXmasValue(dataPoints[0]),
            GetXmasValue(dataPoints[1]),
            GetXmasValue(dataPoints[2]),
            GetXmasValue(dataPoints[3])
        );
    }

    private static int GetXmasValue(string dataPoint)
    {
        string[] keyValue = dataPoint.Split('=');
        return int.Parse(keyValue[1]);
    }

    private readonly int _x;
    public int X
    {
        get
        {
            return _x;
        }
    }

    private readonly int _m;
    public int M
    {
        get
        {
            return _m;
        }
    }

    private readonly int _a;
    public int A
    {
        get
        {
            return _a;
        }
    }

    private readonly int _s;
    public int S
    {
        get
        {
            return _s;
        }
    }

    private bool? _accepted;
    public bool? Accepted
    {
        get
        {
            return _accepted;
        }
    }

    private bool? _rejected;
    public bool? Rejected
    {
        get
        {
            return _rejected;
        }
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

    public void Reject()
    {
        if (Rejected != null)
        {
            throw new Exception("Cannot accept or reject MachinePart object if it has already been accepted or rejected.");
        }

        _rejected = true;
        _accepted = false;
    }

    public int SumXmasValues()
    {
        return X + M + A + S;
    }
}
