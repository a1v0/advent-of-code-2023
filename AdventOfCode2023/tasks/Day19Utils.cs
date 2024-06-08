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
        if (Reject != null)
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
        _name = GetWorkflowName(rawWorkflow);
        _instructions = GetInstructions(rawWorkflow);
    }

    private string GetWorkflowName(string rawWorkflow)
    {
        int firstBracketIndex = rawWorkflow.IndexOf('{');
        string name = rawWorkflow.Substring(0, firstBracketIndex);
        return name;
    }

    private WorkflowInstruction[] GetInstructions(string rawWorkflow)
    {
        int firstBracketIndex = rawWorkflow.IndexOf('{'),
            lastBracketIndex = rawWorkflow.IndexOf('}');
        int lengthOfInstructions = lastBracketIndex - firstBracketIndex - 1;

        string allInstructions = rawWorkflow.Substring(firstBracketIndex + 1, lengthOfInstructions);
        // 
        // 
        // 
        // 
        // 
        // 
        // 
        // 
        Console.WriteLine("!!!! DELETE ME: " + allInstructions);
        // 
        // 
        // 
        // 
        // 
        // 
        // 
        // 
        // 
        string[] instructions = allInstructions.Split(',');
        return GetInstructions(instructions);
    }

    private WorkflowInstruction[] GetInstructions(string[] instructions)
    {
        List<WorkflowInstruction> workflowInstructions = new();

        foreach (string instruction of instructions)
        {
            WorkflowInstruction workflowInstruction = new(instruction);
            workflowInstructions.Add(workflowInstruction);
        }

        return workflowInstructions.ToArray();
    }

    private string _name;
    public string Name
    {
        get;
    }

    private WorkflowInstruction[] _instructions;
    private WorkflowInstruction[] Instructions
    {
        get;
    }

    public string GetNextCommand(MachinePart machinePart)
    {
        // loop through instructions and return command when a match is found
    }
}

public class WorkflowInstruction
{
    public WorkflowInstruction(string instruction)
    {
        // sample input: a<2006:qkq, OR m>2090:A, OR rfg
        // 
        // IsDefault: bool to say whether there's any condition attached or to unconditionally follow the instruction
        // XmasKey: char of x, m, a or s.
        // Operation: char of '<' or '>'
        // Comparison: int of value behind operator
        // NextCommand: string of data behind colon
        //
        // check for colon
        // - if no colon, IsDefault = true and return
        _isDefault = CheckIsDefault(instruction);
        _nextCommand = GetNextCommand(instruction);

        if (!_isDefault)
        {
            _xmasKey = GetXmasKey(instruction);
            _operation = GetOperation(instruction);
            _comparison = GetComparison(instruction);
        }
    }

    private bool CheckIsDefault(string instruction)
    {
        // if an instruction doesn't contain a colon, it's the default and final instruction in a workflow
        return !instruction.Contains(':');
    }

    private string GetNextCommand(instruction)
    {
        if (IsDefault) return instruction;

        int colonIndex = instruction.IndexOf(':');
        return instruction.Substring(colonIndex + 1);
    }

    private char GetXmasKey(instruction)
    {
        return instruction[0];
    }

    private readonly bool _isDefault;
    public bool IsDefault
    {
        get;
    }

    private readonly char? _xmasKey;
    public char? XmasKey
    {
        get;
    }

    private readonly char? _operation;
    public char? Operation
    {
        get;
    }

    private readonly int? _comparison;
    public int? Comparison
    {
        get;
    }

    private readonly string _nextCommand;
    public string NextCommand
    {
        get;
    }
}
