using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveFunctionCollapse : MonoBehaviour
{
    [SerializeField] private int width = 2;
    [SerializeField] private int height = 2;

    private Color[] colorMap;
    private Color tileColor;

    public List<IslandData> islands;

    public void SetIslands()
    {
        for(int i = 0; i < islands.Count;i++)
        {
            islands[i].SetDictionary();
        }
        colorMap = new Color[width * height];

        CheckIslands();

        //for (int y = 0; y < height; y++)
        //{
        //    for (int x = 0; x < width; x++)
        //    {
                
        //    }
        //}


        MapDisplay display = FindObjectOfType<MapDisplay>();

        display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, width, height));
    }

    private void CheckIslands()
    {
        Debug.Log(islands[0].sockets);
        //foreach(int value in islands[0].sockets)
        //{

        //}
        //if(islands[0].sockets["up"] == islands[1].identifier)
        //{
        //    islands[0].validNeighbors.Add(islands[1]);
        //}
    }

    private void CheckIsland(IslandData island)
    {
        Debug.Log(island.sockets["up"]);


        //for(int i = 0; i < 4; i++)
        //{
        //    if(island.socketArray[i] != 0)
        //    {
        //        Debug.Log("Position " + i + " is not zero for island " + island.Name);
        //    }
        //}
    }

    public void CreateMap()
    {
        

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colorMap[y * width + x] = GetTileColor();
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();

        display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, width, height));
    }

    public void ClearMap()
    {
        colorMap = new Color[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colorMap[y * width + x] = Color.black;
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();

        display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, width, height));
    }

    private Color GetTileColor()
    {
        int rand = Random.Range(1, 100);
        if (rand <= 33)
            tileColor.r = Random.Range(0, 255);
        else if (rand > 33 && rand <= 66)
            tileColor.g = Random.Range(0, 255);
        else if (rand > 66)
            tileColor.b = Random.Range(0, 255);
        return tileColor;//change later
    }
}
