using System.Net;
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

public class ConditionRecord
{
    public ConditionRecord(string input)
    {
        string[] splitInput = input.Split(' ');
        Content = splitInput[0];
        Quantities = GetQuantities(splitInput[1]);
        UnknownDamagedSprings = GetUnknownDamagedSprings();
        RecordPattern = GetRecordPattern();
        CountCombinations(Content, UnknownDamagedSprings);
    }

    private string Content { get; }
    private int[] Quantities { get; }
    private int UnknownDamagedSprings { get; }
    public int Combinations { get; set; } = 0;
    private Regex RecordPattern { get; }

    private Regex GetRecordPattern()
    {
        var regexElementsList = new List<string>();

        foreach (int quantity in Quantities)
        {
            string element = $"\\#{{{quantity}}}";
            regexElementsList.Add(element);
        }

        string[] regexElements = regexElementsList.ToArray();
        string regexContentWithoutEnds = string.Join("\\.+", regexElements);
        string regexContent = "^\\.*" + regexContentWithoutEnds + "\\.*$";

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

    private void CountCombinations(string recordContent, int remainingDamagedSprings)
    {
        int unknownFieldsLeft = CountQuestionMarks(recordContent);
        if (unknownFieldsLeft < remainingDamagedSprings) return;

        bool baseCaseIsMet = unknownFieldsLeft == 0 || remainingDamagedSprings == 0;
        if (baseCaseIsMet)
        {
            bool validCombination = ValidateBaseCase(recordContent);
            if (validCombination)
            {
                ++Combinations;
                Console.WriteLine($"{Combinations} combinations found.");
            }
            return;
        }

        int firstUnknown = recordContent.IndexOf('?');
        char[] values = recordContent.ToCharArray();

        values[firstUnknown] = '#';
        CountCombinations(new string(values), remainingDamagedSprings - 1);

        values[firstUnknown] = '.';
        CountCombinations(new string(values), remainingDamagedSprings);

    }

    private bool ValidateBaseCase(string recordContent)
    {
        string contentWithoutUnknowns = recordContent.Replace('?', '.');
        bool matchesRegex = RecordPattern.IsMatch(contentWithoutUnknowns);

        return matchesRegex;
    }

    private static int CountQuestionMarks(string recordContent)
    {
        int counter = 0;
        foreach (char value in recordContent)
        {
            if (value == '?') ++counter;
        }
        return counter;
    }
}