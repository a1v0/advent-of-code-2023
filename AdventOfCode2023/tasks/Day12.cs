using System.Text.RegularExpressions;

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

    private ConditionRecord[] GetConditionRecords()
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
{ }

public class ConditionRecord
{
    public ConditionRecord(string input)
    {
        string[] splitInput = input.Split(' ');
        Content = splitInput[0];
        Quantities = GetQuantities(splitInput[1]);
        UnknownDamagedSprings = GetUnknownDamagedSprings();
        RecordPattern = GetRecordPattern();
    }

    private string Content { get; }
    private int[] Quantities { get; }
    private int UnknownDamagedSprings { get; }
    public int Combinations { get; set; } = 0;
    private Regex RecordPattern { get; }

    private Regex GetRecordPattern()
    {
        var regexElementsList = new List<string>() { "^" };
        foreach (int quantity in Quantities)
        {
            string element = $"#{{{quantity}}}";
            regexElementsList.Add(element);
        }
        regexElementsList.Add("$");
        string[] regexElements = regexElementsList.ToArray();
        string regexContent = string.Join(".+", regexElements);

        return new Regex(regexContent);
    }

    private int GetUnknownDamagedSprings()
    {
        int totalDamagedSprings = Quantities.Sum();
        int knownDamagedSprings = 0;

        foreach (char spring in Content)
        {
            if (spring == '#') ++knownDamagedSprings;
        }

        return totalDamagedSprings - knownDamagedSprings;
    }

    private static int[] GetQuantities(string quantitiesCSV)
    {
        string[] quantitiesText = quantitiesCSV.Split(',');
        int[] quantities = new int[quantitiesText.Length];

        for (int i = 0; i < quantities.Length; ++i)
        {
            quantities[i] = int.Parse(quantitiesText[i]);
        }

        return quantities;
    }
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