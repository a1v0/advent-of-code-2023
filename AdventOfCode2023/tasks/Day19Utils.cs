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

    private (int, int, int, int) ParseMachineData(string machineData)
    {
        string machineData = machineData
          .Replace("{", "")
          .Replace("}", "");

        string[] dataPoints = machineData.Split(',');

        return Tuple.Create(
            GetXmasValue(dataPoints[0]),
            GetXmasValue(dataPoints[1]),
            GetXmasValue(dataPoints[2]),
            GetXmasValue(dataPoints[3]) // This is clunky, but so was anything else I could think of
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

    public void Reject()
    {
        if (Reject is not null)
        {
            throw new Exception("Cannot accept or reject MachinePart object if it has already been accepted or rejected.");
        }

        _rejected = true;
        _accepted = false;
    }
}

public class Workflow
{
    public Workflow(string rawWorkflow)
    {
        SetWorkflowProperties(rawWorkflow);
    }

    private void SetWorkflowProperties(string rawWorkflow)
    {
        // sample input: px{a<2006:qkq,m>2090:A,rfg}
        // extract and set name
        // remove brackets, split and set instructions
    }

    private readonly string _name;
    public string Name
    {
        get;
    }

    private readonly string[] _instructions;
    private string[] Instructions
    {
        get;
    }

    public string GetNextInstruction(MachinePart machinePart)
    {
        // loop through instructions and return when a match is found
    }
}
