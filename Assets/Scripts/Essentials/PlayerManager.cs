using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject player1Prefab;
    [SerializeField] private GameObject player2Prefab;

    private GameObject player1;
    private GameObject player2;

    private Transform player1Spawn;
    private Transform player2Spawn;

    public void Create()
    {
        Instantiate(player1Prefab);
        Instantiate(player2Prefab);
    }


    public void CreateCharacters(Vector3 player1SpawnPos, Vector3  player2SpawnPos)
    {
        //player1Spawn.position = player1SpawnPos;
        //player2Spawn.position = player2SpawnPos;
        player1 = Instantiate(player1Prefab, player1SpawnPos, Quaternion.identity, player1Spawn);
        player2 = Instantiate(player2Prefab, player2SpawnPos, Quaternion.identity, player2Spawn);
    }
}
