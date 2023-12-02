namespace AdventOfCode2023;

public class Day01 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day01Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day01Task2();
}

public class Day01Task1 : BaseTask
{

    private string[]? _unsanitisedCalibrations;

    private string[] UnsanitisedCalibrations
    {
        get
        {
            if (_unsanitisedCalibrations == null)
            {
                _unsanitisedCalibrations = ParseInput();
            }

            return _unsanitisedCalibrations;
        }
    }

    public override string Solve()
    {
        int[] calibrations = SanitiseCalibrations();

        int sum = calibrations.Sum();

        return sum.ToString();
    }

    private int[] SanitiseCalibrations()
    {
        int[] sanitisedCalibrations = new int[UnsanitisedCalibrations.Length];

        for (int i = 0; i < UnsanitisedCalibrations.Length; ++i)
        {
            string currentLine = UnsanitisedCalibrations[i];
            sanitisedCalibrations[i] = ExtractHiddenValue(currentLine);
        }

        return sanitisedCalibrations;
    }

    protected virtual int ExtractHiddenValue(string unsanitisedCalibration)
    {
        string validInts = "123456789";

        string firstNumber = "",
            secondNumber = "";

        foreach (char c in unsanitisedCalibration)
        {
            bool isValidInt = validInts.Contains(c);
            if (!isValidInt) continue;

            if (firstNumber == "")
            {
                firstNumber = c.ToString();
            }

            secondNumber = c.ToString();
        }

        string combinedValue = $"{firstNumber}{secondNumber}";
        return int.Parse(combinedValue);
    }

    private string[] ParseInput()
    {
        return Input.Split('\n');
    }
}

public class Day01Task2 : Day01Task1
{

    protected override int ExtractHiddenValue(string unsanitisedCalibration)
    {
        string validInts = "123456789";
        NumberAsWord[] validNumberWords = GetValidNumberWords();

        string firstNumber = "",
           secondNumber = "";
        string combinedValue = $"{firstNumber}{secondNumber}";
        return int.Parse(combinedValue);
        // PLAN:
        // - check for literal ints
        // - if not an int, check whether the letter is one of the starting letters of a number
        //   - maybe use a switch for this
        // - create a method that looks for a given value in a string
        // - if number is there, convert to normal int string and the rest is as before

    }

    private NumberAsWord[] GetValidNumberWords()
    {
        NumberAsWord[] validNumberWords = new NumberAsWord[9]{
            new NumberAsWord("one", 1),
            new NumberAsWord("two", 2),
            new NumberAsWord("three", 3),
            new NumberAsWord("four", 4),
            new NumberAsWord("five", 5),
            new NumberAsWord("six", 6),
            new NumberAsWord("seven", 7),
            new NumberAsWord("eight", 8),
            new NumberAsWord("nine", 9),
        };

        return validNumberWords;
    }
}

class NumberAsWord
{
    public NumberAsWord(string asWord, int asInt)
    {
        _asWord = asWord;
        _asInt = asInt;
    }

    readonly string _asWord;
    public string AsWord
    {
        get
        {
            return _asWord;
        }
    }

    readonly int _asInt;
    public int AsInt
    {
        get
        {
            return _asInt;
        }
    }
}