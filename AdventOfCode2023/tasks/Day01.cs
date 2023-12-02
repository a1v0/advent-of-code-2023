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
}
