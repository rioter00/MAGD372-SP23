using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public enum DrawMode { ColorMap, HazardPlacing};
    public DrawMode drawMode;

    public int mapWidth;
    public int mapHeight;

    [Range(0, 6)]
    public int levelOfDetail;
    public float noiseScale;

    public int octaves;
    [Range(0, 1)]
    public float persitance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public bool autoUpdate;

    public HazardType[] hazards;
    public Transform hazardHolder;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale, octaves, persitance, lacunarity, offset);

        Color[] colorMap = new Color[mapWidth * mapHeight];

        for(int y = 0; y < mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];
                for(int i = 0; i < hazards.Length; i++)
                {
                    if(currentHeight <= hazards[i].height)
                    {
                        colorMap[y * mapWidth + x] = hazards[i].colorTest;
                        if(hazards[i].hazardPrefab != null)
                        {
                            Debug.Log(x + ", " + y);
                            Vector3 location = new Vector3(x + mapWidth, 0, y + mapHeight);
                            Instantiate(hazards[i].hazardPrefab, location, hazards[i].hazardPrefab.transform.rotation, hazardHolder);
                        }
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();

        if(drawMode == DrawMode.ColorMap)
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight));

        
    }
    
    //Destorys all hazards
    //Just for editor use right now
    public void RemoveHazards()
    {
        while(hazardHolder.childCount > 0)
        {
            DestroyImmediate(hazardHolder.GetChild(0).gameObject);
        }
    }

    void OnValidate()
    {
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
    }
}

[System.Serializable]
public struct HazardType
{
    public string name;
    public GameObject hazardPrefab;
    public float height;
    public Color colorTest;
}