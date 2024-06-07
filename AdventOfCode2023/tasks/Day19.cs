namespace AdventOfCode2023;

public class Day19 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day19Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day19Task2();
}

public class Day19Task1 : BaseTask
{
    public override string Solve()
    {
      // create MachinePart class
      // - needs Accept and Reject methods which set Accepted and Rejected properties to true/false (they're null by default)
      // parse parts as MachineParts and make array
      // 
      // create Workflow class
      // - name
      // - public GetNextInstruction method which takes a MachinePart as parameter, performs its checks and returns an instruction for what comes next
      // parse Workflows as dictionary
      // 
      // loop through MachineParts until every one is accepted or rejected
      // add totals and return
      // 
      // 
      // 
      // 
      // 
      // 
    }
}

public class Day19Task2 : Day19Task1
{ }
