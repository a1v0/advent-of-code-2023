using System.Text.RegularExpressions;

namespace AdventOfCode2023;

public class ConditionRecord
{
    public ConditionRecord(string input)
    {
        string[] splitInput = input.Split(' ');
        Content = splitInput[0];
        Quantities = GetQuantities(splitInput[1]);
        UnknownDamagedSprings = GetUnknownDamagedSprings();
        RecordPattern = GetRecordPattern();
        RecordRegex = new Regex(RecordPattern);
        CountCombinations(Content, UnknownDamagedSprings);

    }

    private string Content { get; }
    private int[] Quantities { get; }
    private int UnknownDamagedSprings { get; }
    public int Combinations { get; set; } = 0;
    private string RecordPattern { get; }
    private Regex RecordRegex { get; }

    private string GetRecordPattern()
    {
        var regexElementsList = new List<string>();

        foreach (int quantity in Quantities)
        {
            string elements = "";
            for (int i = 0; i < quantity; ++i)
            {
                elements += "\\#";
            }
            regexElementsList.Add(elements);
        }

        string[] regexElements = regexElementsList.ToArray();
        string regexContentWithoutEnds = string.Join("\\.+", regexElements);
        string regexContent = "^\\.*" + regexContentWithoutEnds + "\\.*$";

        return regexContent;
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
        bool isWorthContinuing = ValidateIfWorthContinuing(recordContent);
        if (!isWorthContinuing) return;

        int unknownFieldsLeft = CountQuestionMarks(recordContent);
        if (unknownFieldsLeft < remainingDamagedSprings) return;

        bool baseCaseIsMet = unknownFieldsLeft == 0 || remainingDamagedSprings == 0;
        if (baseCaseIsMet)
        {
            bool validCombination = ValidateBaseCase(recordContent);
            if (validCombination)
            {
                ++Combinations;
                if (Combinations % 10000 == 0) Console.WriteLine($"{Combinations} combinations found.");
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

    private bool ValidateIfWorthContinuing(string recordContent)
    {
        string recordUpToFirstUnknown = recordContent.Split('?')[0];
        int numberOfDamagedSprings = CountDamagedSprings(recordUpToFirstUnknown);
        string abbreviatedRegexPattern = GetAbbreviatedRegex(numberOfDamagedSprings);
        var abbreviatedRegex = new Regex(abbreviatedRegexPattern);

        return abbreviatedRegex.IsMatch(recordUpToFirstUnknown);
    }

    private string GetAbbreviatedRegex(int numberOfDamagedSprings)
    {
        int counter = 0;
        int stopIndex = 0;
        for (int i = 0; i < RecordPattern.Length; ++i)
        {
            char value = RecordPattern[i];
            if (value == '#') ++counter;
            if (counter == numberOfDamagedSprings)
            {
                stopIndex = i + 1;
                break;
            }
        }

        return RecordPattern.Substring(0, stopIndex);
    }

    private static int CountDamagedSprings(string record)
    {
        int counter = 0;
        foreach (char spring in record)
        {
            if (spring == '#') ++counter;
        }
        return counter;
    }

    private bool ValidateBaseCase(string recordContent)
    {
        string contentWithoutUnknowns = recordContent.Replace('?', '.');
        bool matchesRegex = RecordRegex.IsMatch(contentWithoutUnknowns);

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