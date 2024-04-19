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
    protected ConditionRecord[] ConditionRecords
    {
        get
        {
            _conditionRecords ??= GetConditionRecords();
            return _conditionRecords;
        }
    }

    protected virtual ConditionRecord[] GetConditionRecords()
    {
        var records = new List<ConditionRecord>();

        foreach (string inputRow in InputRows)
        {
            var record = new ConditionRecord(inputRow);
            records.Add(record);
        }

        return records.ToArray();
    }
}

public class Day12Task2 : Day12Task1
{
    public override string Solve()
    {
        return SumOfCombinations.ToString();
    }

    private long SumOfCombinations
    {
        get
        {
            long sum = 0;
            foreach (ConditionRecord record in ConditionRecords)
            {
                sum += record.Combinations;
            }
            return sum;
        }
    }

    protected override ConditionRecord[] GetConditionRecords()
    {
        var records = new List<ConditionRecord>();

        int counter = 0;

        foreach (string inputRow in InputRows)
        {
            Console.WriteLine($"Processing Condition Record {++counter}");

            string extendedRow = ExtendRow(inputRow);
            var record = new ConditionRecord(extendedRow);
            records.Add(record);
        }

        return records.ToArray();
    }

    private static string ExtendRow(string input)
    {
        string[] splitInput = input.Split(' ');
        string shortContent = splitInput[0];
        string shortQuantities = splitInput[1];

        string extendedContent = $"{shortContent}?{shortContent}?{shortContent}?{shortContent}?{shortContent}";
        string extendedQuantities = $"{shortQuantities},{shortQuantities},{shortQuantities},{shortQuantities},{shortQuantities}";

        return $"{extendedContent} {extendedQuantities}";
    }
}
