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

        string titleRow = GetContentRow("Day " + dayName, border.Length),
            resultRow = GetContentRow(result, border.Length);
        
        string output = $"{border}\n{titleRow}\n{resultRow}\n{border}";
        Console.WriteLine(output);
    }

    private static string GetContentRow(string content, int borderLength)
    {
        string contentRow = $"* {content} *";
        bool appendBefore = false;

        while (contentRow.Length < borderLength)
        {
            if (appendBefore)
            {
                content = " " + content;
            }
            else
            {
                content = content + " ";
            }

            contentRow = $"* {content} *";
            appendBefore = !appendBefore;
        }

        return contentRow;
    }

    private static string GetBorder(int length)
    {
        if (length < 6) length = 6;
        int padding = 4;
        string border = new('*', length + padding);
        return border;
    }
}