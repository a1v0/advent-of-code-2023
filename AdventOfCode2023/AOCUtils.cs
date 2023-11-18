using AdventOfCode2023;

public class AOCUtils
{
    public static string GetRawInput(string dayName)
    {
        string path = $"inputs/{dayName}.txt";
        string rawInput = File.ReadAllText(path);
        return rawInput;
    }

    public static void ShowResult(string dayName, string result)
    {
        int length = result.Length;
        string border = GetBorder(length);
        string titleRow = GetTitleRow(dayName, border.Length),
            resultRow = GetResultRow(result,border.Length);
        string output = $"{border}\n{titleRow}\n{resultRow}\n{border}";
        Console.WriteLine(output);
    }

    private static string GetTitleRow(string dayName, int borderLength)
    {
        string fullDayName = "Day " + dayName;
        string titleRow = $"* {fullDayName} *";
        bool appendBefore = false;

        while (titleRow.Length < borderLength)
        {
            if (appendBefore)
            {
                fullDayName = " " + fullDayName;
            }
            else
            {
                fullDayName = fullDayName + " ";
            }
            titleRow = $"* {fullDayName} *";
            appendBefore = !appendBefore;
        }

        return titleRow;
    }

    private static string GetResultRow(string result, int borderLength)
    {
        string resultRow = $"* {result} *";
        bool appendBefore = false;

        while (resultRow.Length < borderLength)
        {
            if (appendBefore)
            {
                result = " " + result;
            }
            else
            {
                result = result + " ";
            }
            resultRow = $"* {result} *";
            appendBefore = !appendBefore;
        }

        return resultRow;
    }

    private static string GetBorder(int length)
    {
        if (length < 6) length = 6;
        int padding = 4;
        string border = new string('*', length + padding);
        return border;
    }
}