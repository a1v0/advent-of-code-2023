namespace AdventOfCode2023;

public class AOCUtils
{
    public static string GetRawInput(string dayName)
    {
        string? basePath = Environment.GetEnvironmentVariable("PATH_TO_SOLUTION");

        if (basePath is null)
        {
            throw new Exception("Base path not set in PATH_TO_SOLUTION environment variable.");
        }
        
        string? runMode = Environment.GetEnvironmentVariable("RUN_MODE");
        bool isTest = runMode == "TEST";
        string testSuffix = isTest ? ".test" : "";
        
        string path = $"{basePath}/AdventOfCode2023/inputs/{dayName}{testSuffix}.txt";
        string rawInput = File.ReadAllText(path);
        
        return rawInput;
    }

    public static void ShowResult(string dayName, string result)
    {
        int length = result.Length;
        string border = GetResultBorder(length);

        string titleRow = GetResultRow("Day " + dayName, border.Length),
            resultRow = GetResultRow(result, border.Length);

        string output = $"{border}\n{titleRow}\n{resultRow}\n{border}";
        Console.WriteLine(output);
    }

    private static string GetResultRow(string content, int borderLength)
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

    private static string GetResultBorder(int length)
    {
        if (length < 6) length = 6;
        int padding = 4;
        string border = new('*', length + padding);
        return border;
    }
}
