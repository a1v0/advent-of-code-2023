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
            if (rejected == false) continue;
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
        string partsInputRows = partsInput.Split("\n");

        MachinePart[] machineParts = new MachinePart[partsInputRows.Length];

        for (int i = 0; i < partsInputRows.Length; ++i)
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
        string workflowsInputRows = workflowsInput.Split("\n");

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
{ }
