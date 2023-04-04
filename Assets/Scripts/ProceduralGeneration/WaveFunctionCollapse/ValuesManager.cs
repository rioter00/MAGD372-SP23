using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValuesManager<T>
{
    int[][] _grid;
    Dictionary<int, IValue<T>> valueIndexDictionary = new Dictionary<int, IValue<T>>();
    int index = 0;

    public ValuesManager(IValue<T>[][] gridOfValues)
    {
        CreateGridOfIndices(gridOfValues);
    }

    private void CreateGridOfIndices(IValue<T>[][] gridOfValues)
    {
        _grid = MyCollectionExtension.CreateJaggedArray<int[][]>(gridOfValues.Length, gridOfValues[0].Length);
    }
}
