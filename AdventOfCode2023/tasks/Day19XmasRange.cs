namespace AdventOfCode2023;

public class XmasRange
{
    public XmasRange(List<string>? trajectory = null)
    {
        _minX = 1;
        _maxX = 4000;
        _minM = 1;
        _maxM = 4000;
        _minA = 1;
        _maxA = 4000;
        _minS = 1;
        _maxS = 4000;

        _trajectory = new();
        if (trajectory != null)
        {
            foreach (string item in trajectory)
            {
                Trajectory.Add(item);
            }
        }
    }

    public long GetTotalCombinations()
    {
        long rangeX = GetRange(MinX, MaxX),
             rangeM = GetRange(MinM, MaxM),
             rangeA = GetRange(MinA, MaxA),
             rangeS = GetRange(MinS, MaxS);

        return rangeX * rangeM * rangeA * rangeS;
    }

    private static long GetRange(int min, int max)
    {
        return max - min + 1;
    }

    private int _minX;
    public int MinX
    {
        get
        {
            return _minX;
        }
        set
        {
            _minX = value;
        }
    }

    private int _minM;
    public int MinM
    {
        get
        {
            return _minM;
        }
        set
        {
            _minM = value;
        }
    }

    private int _minA;
    public int MinA
    {
        get
        {
            return _minA;
        }
        set
        {
            _minA = value;
        }
    }

    private int _minS;
    public int MinS
    {
        get
        {
            return _minS;
        }
        set
        {
            _minS = value;
        }
    }

    private int _maxX;
    public int MaxX
    {
        get
        {
            return _maxX;
        }
        set
        {
            _maxX = value;
        }
    }

    private int _maxM;
    public int MaxM
    {
        get
        {
            return _maxM;
        }
        set
        {
            _maxM = value;
        }
    }

    private int _maxA;
    public int MaxA
    {
        get
        {
            return _maxA;
        }
        set
        {
            _maxA = value;
        }
    }

    private int _maxS;
    public int MaxS
    {
        get
        {
            return _maxS;
        }
        set
        {
            _maxS = value;
        }
    }

    public XmasRange Duplicate()
    {
        XmasRange duplicateRange = new(Trajectory);

        duplicateRange.MaxX = MaxX;
        duplicateRange.MaxM = MaxM;
        duplicateRange.MaxA = MaxA;
        duplicateRange.MaxS = MaxS;
        duplicateRange.MinX = MinX;
        duplicateRange.MinM = MinM;
        duplicateRange.MinA = MinA;
        duplicateRange.MinS = MinS;

        return duplicateRange;
    }

    public void UpdateValues(WorkflowInstruction instruction, bool invertUpdate = false)
    {
        bool updateMinimum = instruction.Operation == '>';
        if (invertUpdate) updateMinimum = !updateMinimum;

        int newValue = GetNewValue(instruction, updateMinimum, invertUpdate);
        SetValue(instruction, newValue, updateMinimum);
    }

    private void SetValue(WorkflowInstruction instruction, int newValue, bool updateMinimum)
    {
        if (updateMinimum)
        {
            switch (instruction.XmasKey)
            {
                case 'x':
                    if (newValue > MinX) MinX = newValue;
                    break;
                case 'm':
                    if (newValue > MinM) MinM = newValue;
                    break;
                case 'a':
                    if (newValue > MinA) MinA = newValue;
                    break;
                case 's':
                    if (newValue > MinS) MinS = newValue;
                    break;
                default:
                    throw new Exception("Invalid XmasKey provided.");
            }

            return;
        }

        switch (instruction.XmasKey)
        {
            case 'x':
                if (newValue < MaxX) MaxX = newValue;
                break;
            case 'm':
                if (newValue < MaxM) MaxM = newValue;
                break;
            case 'a':
                if (newValue < MaxA) MaxA = newValue;
                break;
            case 's':
                if (newValue < MaxS) MaxS = newValue;
                break;
            default:
                throw new Exception("Invalid XmasKey provided.");
        }
    }

    private int GetNewValue(WorkflowInstruction instruction, bool updateMinimum, bool invertUpdate)
    {
        int inversionPadding = invertUpdate ? 0 : 1;
        if (updateMinimum)
        {
            return (int)instruction.Comparison + inversionPadding;
        }

        return (int)instruction.Comparison - inversionPadding;
    }

    private List<string> _trajectory;
    public List<string> Trajectory
    {
        // This property exists entirely for debugging purposes
        get
        {
            return _trajectory;
        }
    }
}
