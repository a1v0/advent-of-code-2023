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
        // - BaseModule (parent of other module types)
        //   (this could be done in conjunction with an interface)
        //   - public Destinations = List of names of modules
        //   - public void SendPulse (either create a blank method in an interface or create another method called "RunMiddleware" that does all the custom logic specific to a type of module)
        //   - public void ReceivePulse (or better name)
        //
        // - Classes for each type of module
        //
        // parse modules into dictionary
    }
}

public class Day20Task2 : Day20Task1
{ }
