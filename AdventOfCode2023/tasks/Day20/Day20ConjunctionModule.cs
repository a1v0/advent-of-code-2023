namespace AdventOfCode2023;

public class ConjunctionModule : BaseModule
{
    public ConjunctionModule(string[] destinations) : base(destinations) { }

    public override void EmitPulses()
    {
        string pulseType = GetPulseType();

        foreach (string destination in Destinations)
        {
            Pulse pulse = new(pulseType, destination, this);
            PulseQueue.Add(pulse);
        }
    }

    public override void IngestPulse(Pulse pulse)
    {
        string pulseType = pulse.IsHigh ? "high" : "low";

        InputModules[pulse.Source] = pulseType;

        EmitPulses();
    }

    private string GetPulseType()
    {
        foreach (KeyValuePair<BaseModule, string> inputModuleEntry in InputModules)
        {
            if (inputModuleEntry.Value != "high") return "high";
        }

        return "low";
    }

    private Dictionary<BaseModule, string> _inputModules = new();
    public Dictionary<BaseModule, string> InputModules
    {
        get
        {
            return _inputModules;
        }
    }
}
