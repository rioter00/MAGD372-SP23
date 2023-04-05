using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoreHelper
{
    float totalFrequency = 0;
    float totalFrequencyLog = 0;
    PatternManager patternManager;

    public CoreHelper(PatternManager manager)
    {
        patternManager = manager;

        for(int i = 0; i < patternManager.GetNumberOfPatterns(); i++)
        {
            totalFrequency += patternManager.GetPatternFrequency(i);
        }
        totalFrequencyLog = Mathf.Log(totalFrequency, 2);
    }

    public int SelectSolutionPatternFromFrequency(List<int> possibleValues)
    {
        List<float> valueFrequenciesFractions = GetListOfWeigthsFromIndices(possibleValues);
        float randomValue = UnityEngine.Random.Range(0, valueFrequenciesFractions.Sum());
        float sum = 0;
        int index = 0;
        foreach(var item in valueFrequenciesFractions)
        {
            sum += item;
            if(randomValue <= sum)
            {
                return index;
            }
            index++;
        }
        return index - 1;
    }

    private List<float> GetListOfWeigthsFromIndices(List<int> possibleValues)
    {
        var valueFrequencies = possibleValues.Select(i => patternManager.GetPatternFrequency(i)).ToList();
        return valueFrequencies;
    }

    public List<VectorPair> Create4DirectionNeighbours(Vector2Int cellCoordiantes, Vector2Int previousCell)
    {
        List<VectorPair> list = new List<VectorPair>()
        {
            new VectorPair(cellCoordiantes, cellCoordiantes + new Vector2Int(1,0), Direction.Right, previousCell),
            new VectorPair(cellCoordiantes, cellCoordiantes + new Vector2Int(-1,0), Direction.Left, previousCell),
            new VectorPair(cellCoordiantes, cellCoordiantes + new Vector2Int(0,1), Direction.Up, previousCell),
            new VectorPair(cellCoordiantes, cellCoordiantes + new Vector2Int(0,-1), Direction.Down, previousCell)
        };
        return list;
    }

    public List<VectorPair> Create4DirectionNeighbours(Vector2Int cellCoordiante)
    {
        return Create4DirectionNeighbours(cellCoordiante, cellCoordiante);
    }

    public float CalculateEntropy(Vector2Int position, OutputGrid outputGrid)
    {
        float sum = 0;
        foreach(var possibleIndex in outputGrid.GetPossibleValueForPosition(position))
        {
            sum += patternManager.GetPatternFrequencyLog2(possibleIndex);
        }
        return totalFrequencyLog - (sum / totalFrequency);
    }

    public List<VectorPair> CheckIfNeighboursAreCollapsed(VectorPair pairToCheck, OutputGrid outputgrid)
    {
        return Create4DirectionNeighbours(pairToCheck.CellToPropagatePosition, pairToCheck.BaseCellPosition)
            .Where(x => outputgrid.CheckIfValidPosition(x.CellToPropagatePosition) && outputgrid.CheckIfCellIsCollapsed(x.CellToPropagatePosition) == false)
            .ToList();
    }

    public bool CheckCellSolutionForCollision(Vector2Int cellCoordinates, OutputGrid outputGrid)
    {
        foreach(var neighnour in Create4DirectionNeighbours(cellCoordinates))
        {
            if(outputGrid.CheckIfValidPosition(neighnour.CellToPropagatePosition) == false)
            {
                continue;
            }
            HashSet<int> possibleIndices = new HashSet<int>();
            foreach(var patternIndexAtNeighbour in outputGrid.GetPossibleValueForPosition(neighnour.CellToPropagatePosition))
            {
                var possibleNeighboursForBase = patternManager.GetPossibleNeighboursForPatternInDirection(patternIndexAtNeighbour, neighnour.DirectionFromBase.GetOppositeDirectionTo());
                possibleIndices.UnionWith(possibleNeighboursForBase);
            }
            if(possibleIndices.Contains(outputGrid.GetPossibleValueForPosition(cellCoordinates).First()) == false)
            {
                return true;
            }
        }

        return false;
    }
}
