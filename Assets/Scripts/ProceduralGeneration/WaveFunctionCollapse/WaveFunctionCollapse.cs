using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Text;
using System.Linq;

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
    Tilemap output;

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
        manager.Processgrid(valueManager, false);
        core = new WFCCore(5, 5, 500, manager);

    }

    public void CreateTilemap()
    {
        output = new TileMapOutput(valueManager, outputTilemap);
        var result = core.CreateOutputGrid();
        output.CreateOutput(manager, result, outputWidth, outputHeight);
    }

    public void SaveTilemap()
    {

    }
}
