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
        // parse parts as MachineParts and make array
        // parse Workflows into dictionary
        //
        //
        // 
        // loop through MachineParts until every one is accepted or rejected
        // sum totals and return
        // 
        // 
        // 
        // 
        // 
        // 
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

    private Workflow[]? _workflows;
    private Workflow[] Workflows
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

    private Workflow[] GetWorkflows()
    {
        string workflowsInput = Input.Split("\n\n")[0];
        string workflowsInputRows = workflowsInput.Split('\n');

        Workflow[] workflows=new Workflow[workflowsInputRows.Length];

        for (int i = 0; i < workflowsInputRows.Length; ++i)
        {
            string currentInput = workflowsInputRows[i];
            Workflow workflow = new(currentInput);
            workflows[i] = workflow;
        }

        return workflows;
    }
}

public class Day19Task2 : Day19Task1
{ }
