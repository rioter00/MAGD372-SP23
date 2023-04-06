using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Text;
using System.Linq;
using UnityEditor;

public class WaveFunctionCollapse : MonoBehaviour
{
    public Tilemap inputTilemap;
    public Tilemap outputTilemap;
    public int patternSize;
    public int maxIteration = 500;
    public int outputWidth = 5;
    public int outputHeight = 5;
    public bool equalWeights = false;
    ValuesManager<TileBase> valueManager;
    WFCCore core;
    PatternManager manager;
    TileMapOutput output;

    void Start()
    {
        CreateWFC();
    }

    public void CreateWFC()
    {
        InputReader reader = new InputReader(inputTilemap);
        var grid = reader.ReadInputToGrid();
        valueManager = new ValuesManager<TileBase>(grid);
        manager = new PatternManager(2);
        manager.ProcessGrid(valueManager, equalWeights);
        core = new WFCCore(5, 5, 500, manager);

    }

    public void CreateTilemap()
    {
        output = new TileMapOutput(valueManager, outputTilemap);
        Debug.Log(core.CreateOutputGrid());
        var result = core.CreateOutputGrid();
        output.CreateOutput(manager, result, outputWidth, outputHeight);
    }

    public void SaveTilemap()
    {
        if(output.OutputImage != null)
        {
            outputTilemap = output.OutputImage;
            GameObject objectToSave = outputTilemap.gameObject;

            PrefabUtility.SaveAsPrefabAsset(objectToSave, "Assets/Saved/output.prefab");
        }
    }
}
