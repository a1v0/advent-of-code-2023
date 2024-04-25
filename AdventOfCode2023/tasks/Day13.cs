namespace AdventOfCode2023;

public class Day13 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day13Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day13Task2();
}

public class Day13Task1 : BaseTask
{
    public override string Solve()
    {
        int summarisedPatterns = SummariseAshPatterns();
        return summarisedPatterns.ToString();
    }

    private int SummariseAshPatterns()
    {
        int summary = AshPatterns.Aggregate(0, (acc, x) =>
        {
            return acc + x.Summary;
        });
        return summary;
    }

    private AshPattern[]? _ashPatterns;
    private AshPattern[] AshPatterns
    {
        get
        {
            _ashPatterns ??= GetAshPatterns();
            return _ashPatterns;
        }
    }

    protected virtual AshPattern[] GetAshPatterns()
    {
        string[] splitInput = Input.Split("\n\n");
        var ashPatterns = new AshPattern[splitInput.Length];

        for (int i = 0; i < ashPatterns.Length; ++i)
        {
            ashPatterns[i] = new AshPattern(splitInput[i]);
        }

        return ashPatterns;
    }
}

public class Day13Task2 : Day13Task1
{
    protected override AshPattern[] GetAshPatterns()
    {
        string[] splitInput = Input.Split("\n\n");
        var ashPatterns = new AshPattern[splitInput.Length];

        for (int i = 0; i < ashPatterns.Length; ++i)
        {
            ashPatterns[i] = GetAshPattern(splitInput[i]);
        }

        return ashPatterns;
    }

    private static AshPattern GetAshPattern(string input)
    {
        var ashPatternWithSmudge = new AshPattern(input);

        for (int i = 0; i < input.Length; ++i)
        {
            char current = input[i];
            bool isPartOfPattern = current == '.' || current == '#';
            if (!isPartOfPattern) continue;

            char[] inputBuilder = input.ToCharArray();
            char updatedCurrent = current == '.' ? '#' : '.';
            inputBuilder[i] = updatedCurrent;

            string updatedInput = new string(inputBuilder);
            var ashPattern = new AshPattern(updatedInput, ashPatternWithSmudge.ColumnsLeftOfMirror, ashPatternWithSmudge.RowsAboveMirror);

            bool doesNotHaveExactlyOneMirror = (ashPattern.ColumnsLeftOfMirror > 0 && ashPattern.RowsAboveMirror > 0)
                                            || (ashPattern.ColumnsLeftOfMirror == 0 && ashPattern.RowsAboveMirror == 0);
            bool mirrorHasDifferentLocation = (ashPattern.ColumnsLeftOfMirror == 0 || ashPattern.ColumnsLeftOfMirror != ashPatternWithSmudge.ColumnsLeftOfMirror)
                                           && (ashPattern.RowsAboveMirror == 0 || ashPattern.RowsAboveMirror != ashPatternWithSmudge.RowsAboveMirror);

            bool isValidPattern = !doesNotHaveExactlyOneMirror && mirrorHasDifferentLocation;
            if (!isValidPattern) continue;

            return ashPattern;
        }

        throw new Exception("No mirror identified.");
    }
}
