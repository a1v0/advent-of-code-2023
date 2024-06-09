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
    private Dictionary<string, Workflow> Workflows
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
        MachinePart[] machineParts = new MachinePart[partsInputRows.Length - 1]; // -1 because there always seems to be a blank line at the end of the input

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
    // find number of combinations per path:
    // - create some sort of object to map the maximum and minimum values of each letter
    // - the minimum value of a letter always starts at 1 and the max always starts at 4000
    // - if your path contains, e.g., x<2500, you then reduce the max value of X to 2499
    // - go down every possible option. If you "step over" a condition in order to evaluate the following condition, set the max/min values accordingly
    // - find the size of each range (max - min), then construct the number of possibilities. I THINK this is done by multiplying the size of each range
    //
    // Theoretically, this should only provide us with distinct paths
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
        // identify every possible route to an A or an R
        // - create class called XmasRange with min/max properties, and methods to calculate range sizes
        // - cycle through all workflows, starting at 'in' and going down every path until you reach an end
        // - discard if path ends in R
        // use backtracking with recursion
        // iterate over each WorkflowInstruction in current location
        // create copy of current XmasRange in each iteration
        // if range ends in A, add to Ranges and return; if R, just return
        // after backing out, skip over previous instruction by updating max/min values to the reverse
        // - ensure that you correctly set and unset max/min values whilst backtracking
    }

    private void EvaluateWorkflow(string currentWorkflowName, XmasRange range)
    {
        Workflow workflow = Workflows[currentWorkflowName];
        foreach (WorkflowInstruction instruction in workflow.Instructions)
        {
            if (instruction.IsFinal)
            {
                HandleEndOfPath(instruction, range);
                return;
            }

            if (instruction.IsDefault)
            {
                EvaluateWorkflow(instruction.NextCommand, range);
                continue;
            }

            XmasRange duplicateRange = range.Duplicate();
            duplicateRange.UpdateValues(instruction);
            EvaluateWorkflow(instruction.NextCommand, duplicateRange);

            range.UpdateValues(instruction, "inverse");
        }
    }

    private void HandleEndOfPath(WorkflowInstruction instruction, XmasRange range)
    {
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
