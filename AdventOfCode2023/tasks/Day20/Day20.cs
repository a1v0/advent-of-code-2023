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
      return "";
        // Classes required:
        // - Pulse
        //   - private Type = "high" or "low"
        //   - public IsHigh = bool
        //   - public IsLow = bool (returns !IsHigh)
        //   - public Destination = module
        //
        // - Module (parent of other module types)
        //   (this could be done in conjunction with an interface)
        //   - public Destinations = List of modules
        //   - public void SendPulse (either create a blank method in an interface or create another method called "RunMiddleware" that does all the custom logic specific to a type of module)
        //   - public void ReceivePulse (or better name)
        //
        // - Classes for each type of module
        //
        // - Queue
        //   - basically this just needs a static List of pulses that can be added to
        //   - the program would iterate over the contents of this list in some way
    }
}

public class Day20Task2 : Day20Task1
{ }
