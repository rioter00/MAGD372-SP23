using UnityEngine;

public class MapGeneration : MonoBehaviour
{
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

    public bool autoUPdate;

    public HazardType[] hazards;

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
                    //if(currentHeight <= hazards[i].height)
                }
            }
        }
    }
}

[System.Serializable]
public struct HazardType
{
    public string name;
    public GameObject hazardPrefab;
    public Color colorTest;
}