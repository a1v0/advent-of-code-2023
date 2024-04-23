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
        // DigTerrain();
        // long area = CalculateArea();
        // return area.ToString();
    }

    private long CalculateArea()
    {
        // long totalArea = 0;

        // int[] rowNumbers = DugTerrain.Keys.ToArray();
        // Array.Sort(rowNumbers);

        // foreach (int rowNumber in rowNumbers)
        // {
        //     totalArea += CalculateAreaInRow(rowNumber);
        // }

        // return totalArea;
    }

    private long CalculateAreaInRow(int rowNumber)
    {
        // 
        // 
        // instead of looping through every conceivable column number,
        // turn each row's keys into a sorted array. To get the distance
        // between two nodes, just do basic subtraction.
        // 
        // should be a simple enough refactor, and it should reduce running
        // time by a decent bit. Still, it won't be anywhere near quick
        // enough to solve this task in a reasonable time.
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
        // long totalArea = 1;// start at 1 because of leftmost tile
        // Dictionary<int, TerrainNode> row = DugTerrain[rowNumber];
        // int[] rowKeys = row.Keys.ToArray();
        // Array.Sort(rowKeys);

        // char direction = row[rowKeys[0]].Direction;
        // bool currentSquareIsInside = true; // the leftmost square always opens the row

        // for (int rowKey = 1; rowKey < rowKeys.Length; ++rowKey)
        // {
        //     // if we're inside: calculate distance between two nodes, including the current node, but not the starting point
        //     // if we're outside, just add 1 for current node
        //     // 
        //     // check if node is horizontal. If so, continue
        //     // if it's vertical but direction is same, continue
        //     // if vertical and direction changes, reverse in/out status
        //     // 
        //     // 
        //     // 
        //     // 
        //     // 
        //     // 
        //     // 
        //     // 
        //     // 
        //     // 
        //     // 
        //     // 
        //     // 
        //     // 
        //     // 
        //     // 
        //     // 
        //     // 
        //     // 
        //     // 
        //     // 
        //     // 
        //     // 
        //     // 
        //     // 
        //     // 
        //     // 
        //     // 
        //     // 
        // }

        // return totalArea;
    }

    private Dictionary<int, Dictionary<int, TerrainNode>>? _dugTerrain;

    private Dictionary<int, Dictionary<int, TerrainNode>> DugTerrain
    {
        get
        {
            // _dugTerrain ??= CreateBlankTerrainMap();
            // return _dugTerrain;
        }
    }

    private static Dictionary<int, Dictionary<int, TerrainNode>> CreateBlankTerrainMap()
    {
        // var dugTerrain = new Dictionary<int, Dictionary<int, TerrainNode>>();
        // return dugTerrain;
    }

    private void DigTerrain()
    {
        // (int x, int y) currentCoordinates = (0, 0);

        // foreach (DigInstruction digInstruction in DigInstructions)
        // {
        //     DigTerrainSection(digInstruction, currentCoordinates);

        //     (int x, int y) newCoordinates = GetNewCoordinates(digInstruction, currentCoordinates);
        //     currentCoordinates = newCoordinates;
        // }

        // RenameCorners();
    }

    private void DigTerrainSection(DigInstruction digInstruction, (int, int) currentCoordinates)
    {
        // for (int i = 0; i < digInstruction.AmountOfSteps; ++i)
        // {
        //     (int, int) newCoordinates = GetNewCoordinates(digInstruction, currentCoordinates, 1);

        //     var newNode = new TerrainNode(digInstruction.Direction, digInstruction.HexColour);

        //     UpsertDugTerrain(newCoordinates, newNode);

        //     currentCoordinates = newCoordinates;
        // }
    }

    private static (int, int) GetNewCoordinates(DigInstruction digInstruction, (int x, int y) currentCoordinates, int? amountOfSteps = null)
    {
        // amountOfSteps ??= digInstruction.AmountOfSteps;
        // int amountOfStepsWithDirection = (int)amountOfSteps * digInstruction.DirectionCoefficient;

        // int changeX = 0;
        // int changeY = 0;

        // if (digInstruction.IsHorizontal)
        // {
        //     changeX = amountOfStepsWithDirection;
        // }

        // if (digInstruction.IsVertical)
        // {
        //     changeY = amountOfStepsWithDirection;
        // }

        // return (currentCoordinates.x + changeX, currentCoordinates.y + changeY);
    }

    private void UpsertDugTerrain((int x, int y) coordinates, TerrainNode terrainNode)
    {
        // bool hasRow = DugTerrain.ContainsKey(coordinates.y);
        // if (!hasRow)
        // {
        //     DugTerrain.Add(coordinates.y, new Dictionary<int, TerrainNode>());
        // }

        // Dictionary<int, TerrainNode> row = DugTerrain[coordinates.y];
        // bool hasColumn = row.ContainsKey(coordinates.x);
        // if (!hasColumn)
        // {
        //     row.Add(coordinates.x, terrainNode);
        //     return;
        // }

        // row[coordinates.x] = terrainNode;
    }

    private void RenameCorners()
    {
        // foreach (KeyValuePair<int, Dictionary<int, TerrainNode>> row in DugTerrain)
        // {
        //     int rowNo = row.Key;

        //     foreach (KeyValuePair<int, TerrainNode> column in row.Value)
        //     {
        //         int colNo = column.Key;

        //         bool hasLeftNeighbour = row.Value.ContainsKey(colNo - 1);
        //         bool hasRightNeighbour = row.Value.ContainsKey(colNo + 1);
        //         if (!hasLeftNeighbour && !hasRightNeighbour) continue;

        //         bool hasUpperNeighbour = DugTerrain.ContainsKey(rowNo - 1) && DugTerrain[rowNo - 1].ContainsKey(colNo);
        //         bool hasLowerNeighbour = DugTerrain.ContainsKey(rowNo + 1) && DugTerrain[rowNo + 1].ContainsKey(colNo);

        //         if (!hasUpperNeighbour && !hasLowerNeighbour) continue;

        //         if (hasUpperNeighbour)
        //         {
        //             column.Value.Direction = DugTerrain[rowNo - 1][colNo].Direction;
        //         }
        //         else if (hasLowerNeighbour)
        //         {
        //             column.Value.Direction = DugTerrain[rowNo + 1][colNo].Direction;
        //         }
        //     }
        // }
    }

    private DigInstruction[]? _digInstructions;

    private DigInstruction[] DigInstructions
    {
        get
        {
            // _digInstructions ??= GetDigInstructions();
            // return _digInstructions;
        }
    }

    protected virtual DigInstruction[] GetDigInstructions()
    {
        // List<DigInstruction> digInstructions = new();

        // foreach (string inputRow in InputRows)
        // {
        //     DigInstruction currentDigInstruction = new(inputRow);
        //     digInstructions.Add(currentDigInstruction);
        // }

        // return digInstructions.ToArray();
    }
}

public class Day18Task2 : Day18Task1
{
    // 
    // 
    // 
    // 
    // idea for speeding things up:
    // what if we got rid of every single horizontal node?
    // Use RenameCorners method to find all corners, but create
    // and store corners inside new object. Since the changes
    // in vertical direction are all we're truly interested in,
    // this approach should do the trick.
    // 
    // 
    // 
    // 
    // better idea to speed things up (but it'll require a rewrite of much of the solution so far):
    // get rid of everything except the corners. Not even any non-corner vertical segments
    // start on the top row. use the corners to work out the amount of area per row
    // go to the next row with corners. count the y-axis difference and multiply first row's area by that
    // rinse and repeat
    // 
    // things to pay heed to:
    // - a vertical column is always 'opened' by a corner, but it can equally be 'closed' by one. you need to keep track of when corners are opened
    // - this shape is a case that needs handling:
    //   ####
    //   #  #
    //   ## # < here a new corner is introduced. however, the area of this row is not continued below
    //    # #
    //    ###
    // - here it is in steps:
    //   1. loop through each row of corners
    //   2. calculate area of row in the conventional way (counting open but not closed squares)
    //   3. then 'close' any open corners and calculate the area of the cornerless section below
    //   4. once you reach next corner, find the y-axis distance between corners and multiply the row area by that number (making sure not to count rows with corners twice)
    // 
    // 
    // 
    // 
    // 
    // 
    // 
    // to achieve all this, we'll need to change how we parse the input
    // - instead of mapping out every single piece of input, we just need to put the corners somewhere
    // - the end of every up or down section is always a corner whose direction is already correct
    // - the end of every right or left section is a corner but the direction is wrong
    //   - when parsing, horizontal tiles should take the direction of the following vertical section
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
    protected override DigInstruction[] GetDigInstructions()
    {
        // List<DigInstruction> digInstructions = new();

        // foreach (string inputRow in EnlargedInputRows)
        // {
        //     DigInstruction currentDigInstruction = new(inputRow);
        //     digInstructions.Add(currentDigInstruction);
        // }

        // return digInstructions.ToArray();
    }

    private string[]? _enlargedInputRows;
    private string[] EnlargedInputRows
    {
        get
        {
            // _enlargedInputRows ??= GetEnlargedInputRows();
            // return _enlargedInputRows;
        }
    }

    private string[] GetEnlargedInputRows()
    {
        // var enlargedInputRows = new List<string>();

        // foreach (string inputRow in InputRows)
        // {
        //     string enlargedInputRow = GetEnlargedInputRow(inputRow);
        //     enlargedInputRows.Add(enlargedInputRow);
        // }

        // return enlargedInputRows.ToArray();
    }

    private static string GetEnlargedInputRow(string input)
    {
        // var inputInstruction = new DigInstruction(input);
        // string hex = inputInstruction.HexColour;

        // char direction = GetDirectionFromHex(hex);
        // long distance = GetDistanceFromHex(hex);

        // return $"{direction} {distance} ({hex})";
    }

    private static char GetDirectionFromHex(string hex)
    {
        // char lastDigit = hex.Last();
        // switch (lastDigit)
        // {
        //     case '0':
        //         return 'R';
        //     case '1':
        //         return 'D';
        //     case '2':
        //         return 'L';
        //     case '3':
        //         return 'U';
        //     default:
        //         throw new Exception($"Invalid final digit in hex string {hex}.");
        // }
    }

    private static long GetDistanceFromHex(string hex)
    {
        // string distanceHex = "";

        // for (int i = 1; i < 6; ++i)
        // {
        //     // this is a bit of a clunky way of extracting the middle five characters of 'hex'
        //     distanceHex += hex[i];
        // }

        // long decimalDistance = Convert.ToInt64(distanceHex, 16);
        // return decimalDistance;
    }
}
