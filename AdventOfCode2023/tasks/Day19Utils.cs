namespace AdventOfCode2023;

public class MachinePart
{
  public MachinePart(string machineData)
  {
    (int x,int m,int a,int s) = ParseMachineData(machineData);
    _x = x;
    _m = m;
    _a = a;
    _s = s;
  }

  private readonly int _x;
  public int X
  {
    get;
  }

  private readonly int _m;
  public int M
  {
    get;
  }

    private readonly int _a;
  public int A
  {
    get;
  }

    private readonly int _s;
  public int S
  {
    get;
  }

  public bool? Accepted
  {
    get;
    set;
  } = null;

  public bool? Rejected
  {
    get;
    set;
  } = null;
}

public class Workflow
{

}
