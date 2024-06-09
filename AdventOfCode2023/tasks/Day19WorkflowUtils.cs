namespace AdventOfCode2023;

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
