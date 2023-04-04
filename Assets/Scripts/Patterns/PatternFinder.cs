using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternFinder : MonoBehaviour
{
    internal static PatternDataResults GetPatternDataFromGrid<T>(ValuesManager<T> valueManager, int patternSize, bool equalWeights)
    {
        throw new NotImplementedException();
    }

    internal static Dictionary<int, PatternNeighbours> FindPossibleNeighboursForAllPatterns(IFindNeighbourStrategy strategy, PatternDataResults patternFinderResult)
    {
        throw new NotImplementedException();
    }
}
