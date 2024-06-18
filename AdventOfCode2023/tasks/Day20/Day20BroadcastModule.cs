namespace AdventOfCode2023;

public class BroadcastModule : BaseModule
{
    public BroadcastModule(string[] destinations) : base(destinations) { }

    public override void IngestPulse(Pulse pulse)
    {
        EmitPulses();
    }

    public override void EmitPulses()
    {
        foreach (string destination in Destinations)
        {
            Pulse pulse = new("low", destination, this);
            PulseQueue.Add(pulse);
        }
    }
}
