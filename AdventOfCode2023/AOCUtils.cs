using AdventOfCode2023;

public class AOCUtils
{
    public static string GetRawInput(string DayName)
    {
        string path = $"inputs/{DayName}.txt";
        string rawInput = File.ReadAllText(path);
        return rawInput;
    }

    public static void ShowResult(string result) { }
}