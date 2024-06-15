namespace AdventOfCode2023;

public class ConjunctionModule : BaseModule, IBaseModule
{
    public void EmitPulses()
    {
        string pulseType = GetPulseType();

        foreach (string destination in Destinations)
        {
            Pulse pulse = new(pulseType, destination, this);
            PulseQueue.Add(pulse);
        }
    }

    public void IngestPulse(Pulse pulse)
    {
        string pulseType = pulse.IsHigh ? "high":"low";
        
        bool isNewInput = !InputModules.ContainsKey(pulse.Source);
        if (isNewInput) InputModules.Add(pulse.Source, pulseType)
        else InputModules[pulse.Source] = pulseType;

        EmitPulses();
    }

    private string GetPulseType()
    {
foreach(KeyValuePair<BaseModule,string> inputModuleEntry in InputModules)
{
    if (inputModuleEntry.Value != "high") return "high";
}

        return "low";
    }
    
    private Dictionary<BaseModule, string> _inputModules = new();
    private Dictionary<BaseModule, string> InputModules
    {
        get
        {
            return _inputModules;
        }
    }
}
