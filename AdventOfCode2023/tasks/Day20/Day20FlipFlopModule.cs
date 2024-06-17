namespace AdventOfCode2023;

public class FlipFlopModule : BaseModule, IBaseModule
{
    public FlipFlopModule(string[] destinations) : base(destinations) { }

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

    private void FlickSwitch()
    {
        IsOn = !IsOn;
    }

    public new void EmitPulses()
    {
        string pulseType = IsOn ? "high" : "low";

        foreach (string destination in Destinations)
        {
            Pulse pulse = new(pulseType, destination, this);
            PulseQueue.Add(pulse);
        }
    }

    public new void IngestPulse(Pulse pulse)
    {
        if (pulse.IsHigh) return;

        FlickSwitch();
        EmitPulses();
    }
}
