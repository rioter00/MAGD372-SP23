using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternDataResults
{
    private int[][] patternIndciesGrid;

    public Dictionary<int, PatternData> PatternIndexDictionary { get; internal set; }

    public PatternDataResults(int[][] patternIndicesGrid, Dictionary<int, PatternData> patternIndexDictionary)
    {
        this.patternIndciesGrid = patternIndciesGrid;
        PatternIndexDictionary = patternIndexDictionary;
    }

    public int GetGridLengthX()
    {
        return patternIndciesGrid[0].Length;
    }

    public int GetGridLengthY()
    {
        return patternIndciesGrid.Length;
    }

    public int GetIndexAt(int x, int y)
    {
        return patternIndciesGrid[y][x];
    }
    
    public int GetNeighbourInDirection(int x, int y, Direction dir)
    {
        if(patternIndciesGrid.CheckJaggedArray2IfIndexIsValid(x,y) == false)
        {
            return -1;
        }

        switch(dir)
        {
            case Direction.Up:
                if(patternIndciesGrid.CheckJaggedArray2IfIndexIsValid(x, y + 1))
                {
                    return GetIndexAt(x, y + 1);
                }
                return -1;
            case Direction.Down:
                if (patternIndciesGrid.CheckJaggedArray2IfIndexIsValid(x, y - 1))
                {
                    return GetIndexAt(x, y - 1);
                }
                return -1;
            case Direction.Left:
                if (patternIndciesGrid.CheckJaggedArray2IfIndexIsValid(x - 1, y))
                {
                    return GetIndexAt(x - 1, y);
                }
                return -1;
            case Direction.Right:
                if (patternIndciesGrid.CheckJaggedArray2IfIndexIsValid(x + 1, y))
                {
                    return GetIndexAt(x + 1, y);
                }
                return -1;
            default:
                return -1;
        }
    }
}
