using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IslandData", menuName = "ScriptableObjects/IslandData", order = 0)]
public class IslandData : ScriptableObject
{
    public string Name;
    public int identifier;
    public GameObject island;
    public Color color;//temp

    public int[] socketArray;

    public Dictionary<string, int> sockets = new Dictionary<string, int>();

    public List<IslandData> validNeighbors;

    public void SetDictionary()
    {
        sockets = new Dictionary<string, int>();
        sockets.Add("up", socketArray[0]);
        sockets.Add("left", socketArray[1]);
        sockets.Add("right", socketArray[2]);
        sockets.Add("down", socketArray[3]);
    }
}
