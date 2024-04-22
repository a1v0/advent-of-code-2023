namespace AdventOfCode2023;

public class Day18 : BaseDay
{
    public override BaseTask Task1
    {
        get;
    } = new Day18Task1();

    public override BaseTask Task2
    {
        get;
    } = new Day18Task2();
}

public class Day18Task1 : BaseTask
{
    public override string Solve()
    {
        DigTerrain();
        int area = CalculateArea();
        return area.ToString();
    }

    private int CalculateArea()
    {
        int totalArea = 0;

        int[] rowNumbers = DugTerrain.Keys.ToArray();
        Array.Sort(rowNumbers);

        foreach (int rowNumber in rowNumbers)
        {
            totalArea += CalculateAreaInRow(rowNumber);
        }

        return totalArea;
    }

    private int CalculateAreaInRow(int rowNumber)
    {
        int totalArea = 0;

        Dictionary<int, TerrainNode> row = DugTerrain[rowNumber];

        int[] rowIndexes = row.Keys.ToArray();
        (int first, int last) rowExtremities = (rowIndexes.Min(), rowIndexes.Max());

        char direction = row[rowExtremities.first].Direction;
        bool currentSquareIsInside = true; // the leftmost square always opens the row

        for (int i = rowExtremities.first; i <= rowExtremities.last; ++i)
        {
            bool keyExists = row.ContainsKey(i);

            if (!keyExists)
            {
                if (currentSquareIsInside) ++totalArea;
                continue;
            }

            ++totalArea;

            TerrainNode dugSquare = row[i];
            if ("LR".Contains(dugSquare.Direction)) continue;
            if (dugSquare.Direction == direction) continue;

            direction = dugSquare.Direction;
            currentSquareIsInside = !currentSquareIsInside;
        }

        return totalArea;
    }

    private Dictionary<int, Dictionary<int, TerrainNode>>? _dugTerrain;

    private Dictionary<int, Dictionary<int, TerrainNode>> DugTerrain
    {
        get
        {
            _dugTerrain ??= CreateBlankTerrainMap();
            return _dugTerrain;
        }
    }

    private static Dictionary<int, Dictionary<int, TerrainNode>> CreateBlankTerrainMap()
    {
        var dugTerrain = new Dictionary<int, Dictionary<int, TerrainNode>>();
        return dugTerrain;
    }

    private void DigTerrain()
    {
        (int x, int y) currentCoordinates = (0, 0);

        foreach (DigInstruction digInstruction in DigInstructions)
        {
            DigTerrainSection(digInstruction, currentCoordinates);

            (int x, int y) newCoordinates = GetNewCoordinates(digInstruction, currentCoordinates);
            currentCoordinates = newCoordinates;
        }

        RenameCorners();
    }

    private void DigTerrainSection(DigInstruction digInstruction, (int, int) currentCoordinates)
    {
        for (int i = 0; i < digInstruction.AmountOfSteps; ++i)
        {
            (int, int) newCoordinates = GetNewCoordinates(digInstruction, currentCoordinates, 1);

            var newNode = new TerrainNode(digInstruction.Direction, digInstruction.HexColour);

            UpsertDugTerrain(newCoordinates, newNode);

            currentCoordinates = newCoordinates;
        }
    }

    private static (int, int) GetNewCoordinates(DigInstruction digInstruction, (int x, int y) currentCoordinates, int? amountOfSteps = null)
    {
        amountOfSteps ??= digInstruction.AmountOfSteps;
        int amountOfStepsWithDirection = (int)amountOfSteps * digInstruction.DirectionCoefficient;

        int changeX = 0;
        int changeY = 0;

        if (digInstruction.IsHorizontal)
        {
            changeX = amountOfStepsWithDirection;
        }

        if (digInstruction.IsVertical)
        {
            changeY = amountOfStepsWithDirection;
        }

        return (currentCoordinates.x + changeX, currentCoordinates.y + changeY);
    }

    private void UpsertDugTerrain((int x, int y) coordinates, TerrainNode terrainNode)
    {
        bool hasRow = DugTerrain.ContainsKey(coordinates.y);
        if (!hasRow)
        {
            DugTerrain.Add(coordinates.y, new Dictionary<int, TerrainNode>());
        }

        Dictionary<int, TerrainNode> row = DugTerrain[coordinates.y];
        bool hasColumn = row.ContainsKey(coordinates.x);
        if (!hasColumn)
        {
            row.Add(coordinates.x, terrainNode);
            return;
        }

        row[coordinates.x] = terrainNode;
    }

    private void RenameCorners()
    {
        //                  U R R 
        //                  U   D R
        //                  U     D
        // starting point > U   L D
        //                  U   D 
        //                  L L D 
        // 
        // the quick patch over the crack might be to use a FindCorners method that goes through the data structure
        // to find any R or L nodes with only one neighbour.
        // 
        // Check above and below the corner. Corners can have only one node above or below. Copy the direction of
        // that node and set it as the corner.

        // 
        // loop through nodes
        // check for horizontal neighbour
        // if horizontal neighbour, check for vertical neighbour
        // overwrite direction with vertical neighbour's direction
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
        // 
        // 
        // 
        // 
        // 
        // 
        // 

        foreach (KeyValuePair<int, Dictionary<int, TerrainNode>> row in DugTerrain)
        {
            int rowNo = row.Key;

            foreach (KeyValuePair<int, TerrainNode> column in row.Value)
            {
                int colNo = column.Key;

                bool hasLeftNeighbour = row.Value.ContainsKey(colNo - 1);
                bool hasRightNeighbour = row.Value.ContainsKey(colNo + 1);
                if (!hasLeftNeighbour && !hasRightNeighbour) continue;

                bool hasUpperNeighbour = DugTerrain.ContainsKey(rowNo - 1) && DugTerrain[rowNo - 1].ContainsKey(colNo);
                bool hasLowerNeighbour = DugTerrain.ContainsKey(rowNo + 1) && DugTerrain[rowNo + 1].ContainsKey(colNo);

                if (!hasUpperNeighbour && !hasLowerNeighbour) continue;

                if (hasUpperNeighbour)
                {
                    column.Value.Direction = DugTerrain[rowNo - 1][colNo].Direction;
                }
                else if (hasLowerNeighbour)
                {
                    column.Value.Direction = DugTerrain[rowNo + 1][colNo].Direction;
                }
            }
        }
    }

    private DigInstruction[]? _digInstructions;

    private DigInstruction[] DigInstructions
    {
        get
        {
            _digInstructions ??= GetDigInstructions();
            return _digInstructions;
        }
    }

    private DigInstruction[] GetDigInstructions()
    {
        List<DigInstruction> digInstructions = new();

        foreach (string inputRow in InputRows)
        {
            DigInstruction currentDigInstruction = new(inputRow);
            digInstructions.Add(currentDigInstruction);
        }

        return digInstructions.ToArray();
    }
}

public class Day18Task2 : Day18Task1
{ }
