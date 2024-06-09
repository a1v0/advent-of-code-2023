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

public class Workflow
{
    public Workflow(string rawWorkflow)
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
        string[] instructions = allInstructions.Split(',');
        return GetInstructions(instructions);
    }

    private WorkflowInstruction[] GetInstructions(string[] instructions)
    {
        List<WorkflowInstruction> workflowInstructions = new();

        foreach (string instruction in instructions)
        {
            WorkflowInstruction workflowInstruction = new(instruction);
            workflowInstructions.Add(workflowInstruction);
        }

        return workflowInstructions.ToArray();
    }

    private string _name;
    public string Name
    {
        get
        {
            return _name;
        }
    }

    private WorkflowInstruction[] _instructions;
    public WorkflowInstruction[] Instructions
    {
        get
        {
            return _instructions;
        }
    }

    public string GetNextCommand(MachinePart machinePart)
    {
        foreach (WorkflowInstruction instruction in Instructions)
        {
            if (instruction.IsDefault) return instruction.NextCommand;

            int xmasValue = GetXmasValue((char)instruction.XmasKey, machinePart);
            bool comparisonIsTrue = IsComparisonTrue(instruction, xmasValue);

            if (comparisonIsTrue) return instruction.NextCommand;
        }

        throw new Exception("Invalid Workflow Instruction.");
    }

    private int GetXmasValue(char xmasKey, MachinePart machinePart)
    {
        switch (xmasKey)
        {
            case 'x':
                return machinePart.X;
            case 'm':
                return machinePart.M;
            case 'a':
                return machinePart.A;
            case 's':
                return machinePart.S;
            default:
                throw new Exception("Invalid XMAS key given.");
        }
    }

    private bool IsComparisonTrue(WorkflowInstruction instruction, int xmasValue)
    {
        switch (instruction.Operation)
        {
            case '<':
                return xmasValue < instruction.Comparison;
            case '>':
                return xmasValue > instruction.Comparison;
            default:
                throw new Exception("Invalid workflow operator given.");
        }
    }
}

public class WorkflowInstruction
{
    public WorkflowInstruction(string instruction)
    {
        _isDefault = CheckIsDefault(instruction);
        _nextCommand = GetNextCommand(instruction);
        _isFinal = GetIsFinal(instruction);

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

    private string GetNextCommand(string instruction)
    {
        if (IsDefault) return instruction;

        int colonIndex = instruction.IndexOf(':');
        return instruction.Substring(colonIndex + 1);
    }

    private bool GetIsFinal(string instruction)
    {
        return NextCommand == "R" || NextCommand == "A";
    }

    private char GetXmasKey(string instruction)
    {
        return instruction[0];
    }

    private char GetOperation(string instruction)
    {
        return instruction[1];
    }

    private int GetComparison(string instruction)
    {
        int colonIndex = instruction.IndexOf(':');
        int lengthOfComparison = colonIndex - 2; // subtracting the XmasKey and operator

        string comparison = instruction.Substring(2, lengthOfComparison);

        return int.Parse(comparison);
    }

    private readonly bool _isDefault;
    public bool IsDefault
    {
        get
        {
            return _isDefault;
        }
    }

    private readonly bool _isFinal;
    public bool IsFinal
    {
        get
        {
            return _isFinal;
        }
    }

    private readonly char? _xmasKey;
    public char? XmasKey
    {
        get
        {
            return _xmasKey;
        }
    }

    private readonly char? _operation;
    public char? Operation
    {
        get
        {
            return _operation;
        }
    }

    private readonly int? _comparison;
    public int? Comparison
    {
        get
        {
            return _comparison;
        }
    }

    private readonly string _nextCommand;
    public string NextCommand
    {
        get
        {
            return _nextCommand;
        }
    }
}

public class XmasRange
{
    public XmasRange()
    {
        _minX = 1;
        _maxX = 4000;
        _minM = 1;
        _maxM = 4000;
        _minA = 1;
        _maxA = 4000;
        _minS = 1;
        _maxS = 4000;
    }

    public long GetTotalCombinations()
    {
        int rangeX = GetRange(MinX, MaxX),
            rangeM = GetRange(MinM, MaxM),
            rangeA = GetRange(MinA, MaxA),
            rangeS = GetRange(MinS, MaxS);

        return rangeX * rangeM * rangeA * rangeS;
    }

    private static int GetRange(int min, int max)
    {
        return max - min + 1;
    }

    private int _minX;
    public int MinX
    {
        get
        {
            return _minX;
        }
        set
        {
            _minX = value;
        }
    }

    private int _minM;
    public int MinM
    {
        get
        {
            return _minM;
        }
        set
        {
            _minM = value;
        }
    }

    private int _minA;
    public int MinA
    {
        get
        {
            return _minA;
        }
        set
        {
            _minA = value;
        }
    }

    private int _minS;
    public int MinS
    {
        get
        {
            return _minS;
        }
        set
        {
            _minS = value;
        }
    }

    private int _maxX;
    public int MaxX
    {
        get
        {
            return _maxX;
        }
        set
        {
            _maxX = value;
        }
    }

    private int _maxM;
    public int MaxM
    {
        get
        {
            return _maxM;
        }
        set
        {
            _maxM = value;
        }
    }

    private int _maxA;
    public int MaxA
    {
        get
        {
            return _maxA;
        }
        set
        {
            _maxA = value;
        }
    }

    private int _maxS;
    public int MaxS
    {
        get
        {
            return _maxS;
        }
        set
        {
            _maxS = value;
        }
    }

    public XmasRange Duplicate()
    {
        XmasRange duplicateRange = new();

        duplicateRange.MaxX = MaxX;
        duplicateRange.MaxM = MaxM;
        duplicateRange.MaxA = MaxA;
        duplicateRange.MaxS = MaxS;
        duplicateRange.MinX = MinX;
        duplicateRange.MinM = MinM;
        duplicateRange.MinA = MinA;
        duplicateRange.MinS = MinS;

        return duplicateRange;
    }

    public void UpdateValues(WorkflowInstruction instruction)
    {
        ref int propertyToUpdate = ref GetPropertyToUpdate(instruction);
        propertyToUpdate = GetNewValue(instruction);
    }

    private ref int GetPropertyToUpdate(WorkflowInstruction instruction)
    {
        bool updateMinimum = instruction.Operation == '>';
        if (updateMinimum)
        {
            switch (instruction.XmasKey)
            {
                case 'x':
                    return ref MinX;
                case 'm':
                    return ref MinM;
                case 'a':
                    return ref MinA;
                case 's':
                    return ref MinS;
                default:
                    throw new Exception("Invalid XmasKey provided.");
            }
        }

        switch (instruction.XmasKey)
        {
            case 'x':
                return ref MaxX;
            case 'm':
                return ref MaxM;
            case 'a':
                return ref MaxA;
            case 's':
                return ref MaxS;
            default:
                throw new Exception("Invalid XmasKey provided.");
        }
    }

    private int GetNewValue(WorkflowInstruction instruction)
    {
        bool updateMinimum = instruction.Operation == '>';
        if (updateMinimum)
        {
            return (int)instruction.Comparison + 1;
        }

        return (int)instruction.Comparison - 1;
    }
}
