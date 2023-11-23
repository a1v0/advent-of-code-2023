namespace AdventOfCode2023;

public class AOCUtils
{
    public static string GetRawInput(string dayName)
    {
        string? runMode = Environment.GetEnvironmentVariable("RUN_MODE");
        bool isTest = runMode == "TEST";
        string testSuffix = isTest ? ".test" : "";
        //
        //
        //
        // this is causing bugs.
        // the tests runs from a different base directory, meaning
        // that this path will not work during test. As such, I
        // should do something like hardcode a full path to the
        // input files as an environment variable.
        //
        //
        //
        //
        string path = $"AdventOfCode2023/inputs/{dayName}{testSuffix}.txt";
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

    private bool EnoughArgs(string[] args)
    {
        return args.Length >= 2;
    }

    private int ParseDayArg(string day){}
    
    private int ParseTaskArg(string task){}

    public (int, int) ParseInitialArgs(string[] args)
    {
        bool notEnoughArgs = !EnoughArgs(args);
        if (notEnoughArgs) throw new Exception("Not enough arguments given. Two are required: Day and Task.");
        
        string chosenDay = args[0];
        string chosenChallenge = args[1];

        int parsedDay = ParseDayArg(chosenDay);
        int parsedTask = ParseTaskArg(chosenChallenge);

        return (parsedDay, parsedTask);
    }
}
