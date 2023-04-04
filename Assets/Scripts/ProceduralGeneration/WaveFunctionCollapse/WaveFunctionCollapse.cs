using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveFunctionCollapse : MonoBehaviour
{
    [SerializeField] private int width = 2;
    [SerializeField] private int height = 2;

    private Color[] colorMap;
    private Color tileColor;


    private void Start()
    {
        colorMap = new Color[width * height];

        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                colorMap[y * width + x] = GetTileColor();
            }
        }

        CreateMap();
    }

    void CreateMap()
    {
        MapDisplay display = FindObjectOfType<MapDisplay>();

        display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, width, height));
    }

    private Color GetTileColor()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0)
            tileColor.r = Random.Range(0, 255);
        else if (rand == 1)
            tileColor.g = Random.Range(0, 255);
        else if (rand == 2)
            tileColor.b = Random.Range(0, 255);
        return tileColor;//change later
    }
}
