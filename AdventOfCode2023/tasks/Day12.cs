namespace AdventOfCode2023;

public class Day12 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day12Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day12Task2();
}

public class Day12Task1 : BaseTask
{
    public override string Solve()
    {
        return SumOfCombinations.ToString();
    }

    private int SumOfCombinations
    {
        get
        {
            int sum = 0;
            foreach (ConditionRecord record in ConditionRecords)
            {
                sum += record.Combinations;
            }
            return sum;
        }
    }

    private ConditionRecord[]? _conditionRecords;
    private ConditionRecord[] ConditionRecords
    {
        get
        {
            _conditionRecords ??= GetConditionRecords();
            return _conditionRecords;
        }
    }
}

public class Day12Task2 : Day12Task1
{ }

public class ConditionRecord
{
    // create class ConditionRecord
    // - string of content (or maybe bool? array where true=#, false=. and null=?)
    // - array of quantities
    // - int of total unknown damaged springs (i.e. total damaged springs minus known damaged springs)
    // - int of amount of different combinations
    // - regex based on quantities
    // use quantities to create regexes
    // - something like "^.*" + ["####", "#" etc.].Join(".+") + ".*$"
    // test everything against this regex
    // 
    // calculate total number of combinations
    // - using quantity of unknown damaged springs, add them to the string
    //   - if quantity is 0, then set combinations to 1 and break
    // 
    // backtracking solution (possibly very inefficient)
    // - if no more ? in string OR if we've run out of #s to distribute
    //   - set all ? to . and test with regex
    //   - if a match, ++, otherwise no
    //   - return
    // - otherwise change next ? with # and run recursive method again
    // - as above but with .
    // 
    // backtracking method details:
    // - copy string
    // - find next index of ? and replace with #, then .
    // - run method again recursively
}