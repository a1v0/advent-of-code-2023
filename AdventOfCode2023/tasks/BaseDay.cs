namespace AdventOfCode2023;

public class BaseDay
{
    public void Solve(int task) {
        switch (task)
        {
            case 1:
                Task1.Solve();
                break;
            case 2:
                Task2.Solve();
                break;
            default:
                throw new Exception("An invalid task number has been selected.");
        }
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
    private string DayName
    {
        get
        {
            string fullDayAndTaskName = this.GetType().Name;
            string dayName = fullDayAndTaskName.Substring(3, 2); // Day name always has format "DayXYTaskZ"

            bool dayNameTooShort = dayName.Length != 2;
            bool dayNameIsNaN = Int32.TryParse(dayName, out int result);
            
            if (dayNameTooShort || dayNameIsNaN)
            {
                // This error handling could be more robust but, since it's only me using the repo, I'll leave it at this
                throw new Exception($"Unable to extract DayName from {fullDayAndTaskName}.");
            }
            
            return dayName;
        }
    }

    private string Input
    {
        get
        {
            return AOCUtils.GetRawInput(DayName);
        }
    }
    
    public virtual void Solve()
    {
        Console.WriteLine("If you are seeing this message, it means that no solution has been created for the chosen Task.");
    }    
}
