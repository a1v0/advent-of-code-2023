namespace AdventOfCode2023;

public class Day19 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day19Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day19Task2();
}

public class Day19Task1 : BaseTask
{
    public override string Solve()
    {
        OrganiseParts();
        int sumOfAcceptedParts = SumAcceptedParts();
        return sumOfAcceptedParts.ToString();
    }

    private void OrganiseParts()
    {
        foreach (MachinePart machinePart in MachineParts)
        {
            CycleThroughWorkflows(machinePart);
        }
    }

    private void CycleThroughWorkflows(MachinePart machinePart)
    {
        string currentWorkflowName = "in";
        while (machinePart.Accepted == null || machinePart.Rejected == null)
        {
            Workflow currentWorkflow = Workflows[currentWorkflowName];
            string nextCommand = currentWorkflow.GetNextCommand(machinePart);

            if (nextCommand == "A") machinePart.Accept();
            else if (nextCommand == "R") machinePart.Reject();
            else currentWorkflowName = nextCommand;
        }
    }

    private int SumAcceptedParts()
    {
        int sum = 0;

        foreach (MachinePart machinePart in MachineParts)
        {
            bool? rejected = machinePart.Rejected,
                  accepted = machinePart.Accepted;
            if (rejected == true) continue;
            if (rejected == null || accepted == null)
            {
                throw new Exception("Accepted/Rejected state of MachinePart is null.");
            }

            sum += machinePart.SumXmasValues();
        }

        return sum;
    }

    private MachinePart[]? _machineParts;
    private MachinePart[] MachineParts
    {
        get
        {
            _machineParts ??= GetMachineParts();
            return _machineParts;
        }
    }

    private Dictionary<string, Workflow>? _workflows;
    protected Dictionary<string, Workflow> Workflows
    {
        get
        {
            _workflows ??= GetWorkflows();
            return _workflows;
        }
    }

    private MachinePart[] GetMachineParts()
    {
        string[] splitInput = Input.Split("\n\n");
        string partsInput = splitInput[1];
        string[] partsInputRows = partsInput.Split("\n");
        MachinePart[] machineParts = new MachinePart[partsInputRows.Length];

        for (int i = 0; i < machineParts.Length; ++i)
        {
            string currentInput = partsInputRows[i];
            MachinePart machinePart = new(currentInput);
            machineParts[i] = machinePart;
        }

        return machineParts;
    }

    private Dictionary<string, Workflow> GetWorkflows()
    {
        string[] splitInput = Input.Split("\n\n");
        string workflowsInput = splitInput[0];
        string[] workflowsInputRows = workflowsInput.Split("\n");

        Dictionary<string, Workflow> workflows = new();

        for (int i = 0; i < workflowsInputRows.Length; ++i)
        {
            string currentInput = workflowsInputRows[i];
            Workflow workflow = new(currentInput);
            workflows.Add(workflow.Name, workflow);
        }

        return workflows;
    }
}

public class Day19Task2 : Day19Task1
{
    public override string Solve()
    {
        PopulateRanges();
        long distinctCombinations = GetDistinctCombinations();
        return distinctCombinations.ToString();
    }

    private long GetDistinctCombinations()
    {
        long sumOfDistinctCombinations = GetSumOfDistinctCombinations();
        return sumOfDistinctCombinations;
    }

    private long GetSumOfDistinctCombinations()
    {
        long sum = 0;

        foreach (XmasRange range in Ranges)
        {
            sum += range.GetTotalCombinations();
        }

        return sum;
    }

    private void PopulateRanges()
    {
        string currentWorkflowName = "in";
        XmasRange range = new();

        EvaluateWorkflow(currentWorkflowName, range);
    }

    private void EvaluateWorkflow(string currentWorkflowName, XmasRange range)
    {
      System.Console.WriteLine("I'm currently at '"+currentWorkflowName+"'");
        Workflow workflow = Workflows[currentWorkflowName];
        foreach (WorkflowInstruction instruction in workflow.Instructions)
        {
            if (instruction.IsFinal)
            {
                XmasRange duplicateRangeFinal = range.Duplicate();
                if (!instruction.IsDefault)
                {
                    duplicateRangeFinal.UpdateValues(instruction);
                }

                HandleEndOfPath(instruction, duplicateRangeFinal);
                range.UpdateValues(instruction, true);
                continue;
            }

            else if (instruction.IsDefault)
            {
                XmasRange duplicateRangeDefault = range.Duplicate();
                EvaluateWorkflow(instruction.NextCommand, duplicateRangeDefault);
                continue;
            }

            XmasRange duplicateRange = range.Duplicate();
            duplicateRange.UpdateValues(instruction);
            EvaluateWorkflow(instruction.NextCommand, duplicateRange);

            range.UpdateValues(instruction, true);
        }
    }

    private void HandleEndOfPath(WorkflowInstruction instruction, XmasRange range)
    {
      System.Console.WriteLine("I'm done! The end command is "+instruction.NextCommand);
      System.Console.WriteLine(range.MinX.ToString()+"-"+range.MaxX.ToString()+" "+range.MinM.ToString()+"-"+range.MaxM.ToString()+" "+range.MinA.ToString()+"-"+range.MaxA.ToString()+" "+range.MinS.ToString()+"-"+range.MaxS.ToString());
        if (instruction.NextCommand == "A")
        {
            Ranges.Add(range);
        }
    }

    private List<XmasRange> _ranges = new();
    private List<XmasRange> Ranges
    {
        get
        {
            return _ranges;
        }
    }
}
