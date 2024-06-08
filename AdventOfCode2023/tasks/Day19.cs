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
        get{
            _machineParts ?= GetMachineParts();
            return _machineParts;
        }
    }

    private Workflow[]? _workflows;
    private Workflow[] Workflows
    {
        get{
            _workflows ?= GetWorkflows();
            return _workflows;
        }
    }
}

public class Day19Task2 : Day19Task1
{ }
