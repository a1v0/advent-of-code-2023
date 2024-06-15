namespace AdventOfCode2023;

public class FlipFlopModule : BaseModule, IBaseModule
{
    private bool _isOn = false;
    private bool IsOn
    {
        get
        {
            return _isOn;
        }

        set
        {
          // not actually sure whether this is the correct way of coding this type of thing
              _isOn = !_isOn;
        }
    }

    public void EmitPulses()
    {
        // Flip-flop modules (prefix %) are either on or off; they are initially off.
        // If a flip-flop module receives a high pulse, it is ignored and nothing happens.
        // However, if a flip-flop module receives a low pulse, it flips between on and off.
        // If it was off, it turns on and sends a high pulse.
        // If it was on, it turns off and sends a low pulse.
    }

    public void IngestPulse(Pulse pulse)
    {
        
    }
}
