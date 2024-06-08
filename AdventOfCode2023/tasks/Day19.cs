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
        // loop through all parts
        // start at 'in' and cycle through NextCommand values (e.g. while Rejected == null)
        //
    }

    private int SumAcceptedParts()
    {
        int sum=0;

        foreach (MachinePart machinePart of MachineParts)
        {
            bool? rejected= machinePart.Rejected,
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
            _machineParts ?= GetMachineParts();
            return _machineParts;
        }
    }

    private Dictionary<string, Workflow>? _workflows;
    private Dictionary<string, Workflow> Workflows
    {
        get
        {
            _workflows ?= GetWorkflows();
            return _workflows;
        }
    }

    private MachinePart[] GetMachineParts()
    {
        string partsInput=Input.Split("\n\n")[1];
        string partsInputRows=partsInput.Split('\n');

        MachinePart[] machineParts=new MachinePart[partsInputRows.Length];

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
        string workflowsInput = Input.Split("\n\n")[0];
        string workflowsInputRows = workflowsInput.Split('\n');

        Dictionary<string, Workflow> workflows=new();

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
