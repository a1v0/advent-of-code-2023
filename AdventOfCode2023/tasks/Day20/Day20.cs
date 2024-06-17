namespace AdventOfCode2023;

public class Day20 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day20Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day20Task2();
}

public class Day20Task1 : BaseTask
{
    public override string Solve()
    {
        // parse input into dictionary of modules
        // loop 1000 times
        // ping broadcaster
        // loop over contents of queue and processes pulses accordingly
        // - deleting processed items in a list while you're iterating isn't a good approach
        // - you might get away with not deleting at all, but the queue could potentially get very large
        // - feels a bit hacky but: every e.g. 1000 iterations over pulses in the queue, end the loop, delete the first 1000 items and then restart the loop

        int productOfPulseTallies = PulseQueue.GetProductOfTallies();
        return productOfPulseTallies.ToString();
    }

    private Dictionary<string, BaseModule> GetModules()
    {
        Dictionary<string, BaseModule> modules = new();
        foreach(string row in InputRows)
        {
            (string moduleName, string moduleBlueprint) = GetModuleElements(row);
            BaseModule module = GetModule(moduleBlueprint);

            modules.Add(moduleName, module);
        }

        return modules;
// broadcaster -> a, b, c
// %a -> b
// %b -> c
// %c -> inv
// &inv -> a
//
    }

    private (string, string) GetModuleElements(string input)
    {
        string[] elements = input.Split(" -> ");
        return (elements[0], elements[1]);
    }

    private Dictionary<string, BaseModule>? _modules;
    private Dictionary<string, BaseModule> Modules
    {
        get
        {
            _modules ??= GetModules();
            return _modules;
        }
    }
}

public class Day20Task2 : Day20Task1
{ }
