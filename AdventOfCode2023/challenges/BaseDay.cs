namespace AdventOfCode2023;

public class BaseDay
{
    public string DayName
    {
        get
        {
            string fullDayName = this.GetType().Name;
            string dayName = fullDayName.Substring(3); // Day name always has format "DayXY", so this will always return last two digits

            bool dayNametooShort=dayName.Length != 2;
            bool dayNameIsNaN = Int32.TryParse(dayName, out int result);
            
            if (dayNametooShort || dayNameIsNaN)
            {
                throw new Exception($"Unable to extract DayName from {fullDayName}.");
            }
            
            return dayName;
        }
    }

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
                break;
        }
    }
    
    public virtual BaseTask Task1
    {
        get;
    }

    public virtual BaseTask Task2
    {
        get;
    }
}

public class BaseTask
{
    public virtual void Solve()
    {
        Console.WriteLine("If you are seeing this message, it means that no solution has been created for the chosen Task.");
    }    
}
