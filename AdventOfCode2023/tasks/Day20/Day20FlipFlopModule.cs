namespace AdventOfCode2023;

public class FlipFlopModule : BaseModule
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

    public override void EmitPulses()
    {
        string pulseType = IsOn ? "high" : "low";

        foreach (string destination in Destinations)
        {
            Pulse pulse = new(pulseType, destination, this);
            PulseQueue.Add(pulse);
        }
    }

    public override void IngestPulse(Pulse pulse)
    {
        if (pulse.IsHigh) return;

        FlickSwitch();
        EmitPulses();
    }
}
