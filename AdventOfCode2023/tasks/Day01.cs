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

    private string[] UnsanitisedCalibrations
    {
    get;
    } = ParseInput();

    public override string Solve()
    {
        int[] calibrations = SanitiseCalibrations();
        
        // 
        // loop through each line
        // loop through each char
        // try using one of C#'s double loops to count down as well as up
        // ensure that, if there's only one int, that the int doesn't get counted twice
        // combine both numbers, convert to int and add to sanitisedCals
        // sum array
        // return
        // 
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

    private int ExtractHiddenValue(string unsanitisedCalibration)
    {
        string validInts = "123456789";

        char firstNumber = "",
            secondNumber = "";

        foreach (char c in unsanitisedCalibration)
        {
            // if firstNum is "" and c is a number, assign
            // if firstNum is assigned, then assign to secondNum
        }

        string combinedValue = firstNumber + secondNumber;
        return int.Parse(combinedValue);
    }

    private string[] ParseInput()
    {
        return Input.Split('\n');
    }
}

public class Day01Task2 : Day01Task1
{

}
