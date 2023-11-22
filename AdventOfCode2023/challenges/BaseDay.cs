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
}
