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
        NumberAsWord[] oNumbers = GetONumberWords();
        NumberAsWord[] tNumbers = GetTNumberWords();
        NumberAsWord[] fNumbers = GetFNumberWords();
        NumberAsWord[] sNumbers = GetSNumberWords();
        NumberAsWord[] eNumbers = GetENumberWords();
        NumberAsWord[] nNumbers = GetNNumberWords();

        string firstNumber = "",
           secondNumber = "";

        for (int i = 0; i < unsanitisedCalibration.Length; ++i)
        {
            char c = unsanitisedCalibration[i];
            string remainderOfCalibration = unsanitisedCalibration[i..];

            string? searchResult = null;

            string digit = "";

            switch (c)
            {
                case '1' or '2' or '3' or '4' or '5' or '6' or '7' or '8' or '9':
                    digit = c.ToString();
                    break;
                case 'o':
                    searchResult = CheckForNumbers(oNumbers, remainderOfCalibration);
                    break;
                case 't':
                    searchResult = CheckForNumbers(tNumbers, remainderOfCalibration);
                    break;
                case 'f':
                    searchResult = CheckForNumbers(fNumbers, remainderOfCalibration);
                    break;
                case 's':
                    searchResult = CheckForNumbers(sNumbers, remainderOfCalibration);
                    break;
                case 'e':
                    searchResult = CheckForNumbers(eNumbers, remainderOfCalibration);
                    break;
                case 'n':
                    searchResult = CheckForNumbers(nNumbers, remainderOfCalibration);
                    break;
                default:
                    continue;
            }

            digit = searchResult is null ? digit : searchResult;

            if (firstNumber == "") firstNumber = digit;
            secondNumber = digit;
        }

        string combinedValue = $"{firstNumber}{secondNumber}";
        return int.Parse(combinedValue);
    }

    private static string? CheckForNumbers(NumberAsWord[] numbers, string substring)
    {
        string? output = null;

        foreach (NumberAsWord numberAsWord in numbers)
        {
            output = FindNumberAsWord(numberAsWord, substring);
            if (output is not null) break;
        }

        return output;
    }

    private static string? FindNumberAsWord(NumberAsWord desiredNumber, string substring)
    {
        for (int i = 0; i < desiredNumber.AsWord.Length; ++i)
        {
            try
            {
                char cDesired = desiredNumber.AsWord[i];
                char cSubstring = substring[i];

                if (cDesired != cSubstring) return null;
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }

        return desiredNumber.AsNumber;
    }

    private NumberAsWord[] GetONumberWords()
    {
        NumberAsWord[] validNumberWords = new NumberAsWord[1]{
            new NumberAsWord("one", "1"),
        };

        return validNumberWords;
    }


    private NumberAsWord[] GetTNumberWords()
    {
        NumberAsWord[] validNumberWords = new NumberAsWord[2]{
            new NumberAsWord("two", "2"),
            new NumberAsWord("three", "3"),
        };

        return validNumberWords;
    }


    private NumberAsWord[] GetFNumberWords()
    {
        NumberAsWord[] validNumberWords = new NumberAsWord[2]{
            new NumberAsWord("four", "4"),
            new NumberAsWord("five", "5"),
        };

        return validNumberWords;
    }


    private NumberAsWord[] GetSNumberWords()
    {
        NumberAsWord[] validNumberWords = new NumberAsWord[2]{
            new NumberAsWord("six", "6"),
            new NumberAsWord("seven", "7"),
        };

        return validNumberWords;
    }


    private NumberAsWord[] GetENumberWords()
    {
        NumberAsWord[] validNumberWords = new NumberAsWord[1]{
            new NumberAsWord("eight", "8"),
        };

        return validNumberWords;
    }


    private NumberAsWord[] GetNNumberWords()
    {
        NumberAsWord[] validNumberWords = new NumberAsWord[1]{
            new NumberAsWord("nine", "9"),
        };

        return validNumberWords;
    }
}

class NumberAsWord
{
    public NumberAsWord(string asWord, string asNumber)
    {
        _asWord = asWord;
        _asNumber = asNumber;
    }

    readonly string _asWord;
    public string AsWord
    {
        get
        {
            return _asWord;
        }
    }

    readonly string _asNumber;
    public string AsNumber
    {
        get
        {
            return _asNumber;
        }
    }
}