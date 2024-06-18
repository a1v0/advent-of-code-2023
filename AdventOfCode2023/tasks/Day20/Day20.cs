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
        PrepareSolution();
        for (int i = 0; i < ButtonPushes; ++i)
        {
            PushButton();
        }
        int productOfPulseTallies = PulseQueue.GetProductOfTallies();
        return productOfPulseTallies.ToString();
    }

    protected void PushButton()
    {
        // Create dummy source module purely so I can ping the Broadcaster
        string[] dummyDestinations = Array.Empty<string>();
        BaseModule dummyModule = new(dummyDestinations);

        Pulse broadcasterPulse = new("low", "broadcaster", dummyModule);
        PulseQueue.Add(broadcasterPulse);

        ProcessPulseQueue();
    }

    protected virtual void ProcessPulseQueue()
    {
        for (int i = 0; i < PulseQueue.Queue.Count; ++i)
        {
            Pulse pulse = PulseQueue.Queue[i];
            if (Modules.ContainsKey(pulse.Destination))
            {
                Modules[pulse.Destination].IngestPulse(pulse);
            }
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
    protected Dictionary<string, BaseModule> Modules
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

    protected void PrepareSolution()
    {
        PulseQueue.Reset();
        PopulateConjunctionInputs();
    }

    private void PopulateConjunctionInputs()
    {
        foreach (KeyValuePair<string, BaseModule> pair in Modules)
        {
            BaseModule module = pair.Value;
            foreach (string destination in module.Destinations)
            {
                if (!Modules.ContainsKey(destination)) continue;
                BaseModule destinationModule = Modules[destination];
                bool isConjunction = destinationModule is ConjunctionModule;

                if (!isConjunction) continue;

                ConjunctionModule destinationModuleAsConjunction = (ConjunctionModule)destinationModule;
                destinationModuleAsConjunction.InputModules.Add(module, "low");
            }
        }
    }
}

public class Day20Task2 : Day20Task1
{
    public override string Solve()
    {
        PrepareSolution();
        int i = 0;
        while (CONDITION GOES HERE){
            ++i;
            PushButton();
        }

        return i.ToString();
    }

    protected override void ProcessPulseQueue()
    {
        for (int i = 0; i < PulseQueue.Queue.Count; ++i)
        {
            Pulse pulse = PulseQueue.Queue[i];
            bool rxGetsLowPulse = (pulse.Destination == "rx") && pulse.IsLow;
            if (rxGetsLowPulse)
            {
                LowPulseSentToRx = true;
                return;
            }

            if (Modules.ContainsKey(pulse.Destination))
            {
                Modules[pulse.Destination].IngestPulse(pulse);
            }
        }

        PulseQueue.Clear();
    }
}
