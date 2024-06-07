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
}

public class Workflow
{

}
