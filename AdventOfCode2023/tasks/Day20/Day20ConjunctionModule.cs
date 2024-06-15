namespace AdventOfCode2023;

public class ConjunctionModule : BaseModule, IBaseModule
{
    public void EmitPulses()
    {
        // Conjunction modules (prefix &) remember the type of the most recent
        // pulse received from each of their connected input modules; they
        // initially default to remembering a low pulse for each input. When
        // a pulse is received, the conjunction module first updates its memory
        // for that input. Then, if it remembers high pulses for all inputs, it
        // sends a low pulse; otherwise, it sends a high pulse.
    }

    public void IngestPulse(Pulse pulse)
    {
        
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
