using System.Text.RegularExpressions;

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
        // ping broadcaster
        // loop over contents of queue and processes pulses accordingly
        // - deleting processed items in a list while you're iterating isn't a good approach
        // - you might get away with not deleting at all, but the queue could potentially get very large
        // - feels a bit hacky but: every e.g. 1000 iterations over pulses in the queue, end the loop, delete the first 1000 items and then restart the loop
        for (int i = 0; i < ButtonPushes; ++i)
        {
            PushButton();
        }
        int productOfPulseTallies = PulseQueue.GetProductOfTallies();
        return productOfPulseTallies.ToString();
    }

    private void PushButton()
    {
        BroadcastModule broadcaster = (BroadcastModule)Modules["broadcaster"];
        broadcaster.EmitPulses();

        ProcessPulseQueue();
    }

    private void ProcessPulseQueue()
    {
        for (int i = 0; i < PulseQueue.Queue.Count; ++i)
        {
            Pulse pulse = PulseQueue.Queue[i];
            Modules[pulse.Destination].IngestPulse(pulse);
        }

        PulseQueue.Clear();
    }

    private Dictionary<string, BaseModule> GetModules()
    {
        Dictionary<string, BaseModule> modules = new();
        foreach (string row in InputRows)
        {
            string moduleName = GetModuleName(row);
            BaseModule module = GetModule(row);

            modules.Add(moduleName, module);
        }

        return modules;
    }

    private static string GetModuleName(string input)
    {
        string pattern = @"[a-z]+";
        Match name = Regex.Match(input, pattern);
        return name.Value;
    }

    private static BaseModule GetModule(string input)
    {
        string[] inputElements = input.Split(" -> ");
        string nameAndType = inputElements[0];
        string destinationsCSV = inputElements[1];

        string[] destinations = destinationsCSV.Split(", ");

        if (nameAndType == "broadcaster")
        {
            return new BroadcastModule(destinations);
        }

        char type = nameAndType[0];

        if (type == '&')
        {
            return new ConjunctionModule(destinations);
        }

        if (type == '%')
        {
            return new FlipFlopModule(destinations);
        }

        throw new Exception("Invalid module type: " + type);
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

    private readonly int _buttonPushes = 1000;
    private int ButtonPushes
    {
        get
        {
            return _buttonPushes;
        }
    }
}

public class Day20Task2 : Day20Task1
{ }
