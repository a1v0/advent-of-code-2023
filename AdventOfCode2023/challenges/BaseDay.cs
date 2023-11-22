namespace AdventOfCode2023;

public class BaseDay
{
    public string DayName
    {
        get
        {
            string fullDayName = this.GetType().Name;
            string dayName = fullDayName.Substring(3); // Day name always has format "DayXY", so this will always return last two digits
            if (dayName.Length != 2)
            {
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //
                //
                // throw exception here
            }
            
            return dayName;
        }
    }
}
