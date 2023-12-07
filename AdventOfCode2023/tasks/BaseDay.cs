namespace AdventOfCode2023;

public class BaseDay
{
    public string Solve(byte task)
    {
        BaseTask chosenTask;

        if (task == 1) chosenTask = Task1;
        else if (task == 2) chosenTask = Task2;
        else
        {
            throw new Exception("An invalid task number has been selected.");
        }

        string result = chosenTask.Solve();

        AOCUtils.ShowResult(chosenTask.DayName, result);
        return result;
    }

    public virtual BaseTask Task1
    {
        get;
    } = new BaseTask();

    public virtual BaseTask Task2
    {
        get;
    } = new BaseTask();
}

public class BaseTask
{
    public string DayName
    {
        get
        {
            string fullDayAndTaskName = this.GetType().Name;
            string dayName = fullDayAndTaskName.Substring(3, 2); // Day name always has format "DayXYTaskZ"

            bool dayNameTooShort = dayName.Length != 2;
            bool dayNameIsNumber = Int32.TryParse(dayName, out int result);

            if (dayNameTooShort || !dayNameIsNumber)
            {
                // This error handling could be more robust but, since it's only me using the repo, I'll leave it at this
                throw new Exception($"Unable to extract DayName from {fullDayAndTaskName}. Received {dayName}.");
            }

            return dayName;
        }
    }

    protected string Input
    {
        get
        {
            return AOCUtils.GetRawInput(DayName);
        }
    }

    private string[]? _inputRows;

    protected string[] InputRows
    {
        get
        {
            _inputRows ??= GetInputRows();
            return _inputRows;
        }
    }

    private string[] GetInputRows()
    {
        return Input.Split('\n');
    }

    public virtual string Solve()
    {
        Console.WriteLine("If you are seeing this message, it means that no solution has been created for the chosen Task.");
        return "";
    }
}
