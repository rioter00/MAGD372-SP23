using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private List<PlayerInput> players = new List<PlayerInput>();
    [SerializeField] private GameObject player1Prefab;
    [SerializeField] private GameObject player2Prefab;

    private GameObject player1;
    private GameObject player2;

    private Transform player1Spawn;
    private Transform player2Spawn;

    [SerializeField] private PlayerInputManager playerInputManager;

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
    }

    public void CreateCharacters(Vector3 player1SpawnPos, Vector3  player2SpawnPos)
    {
        //player1Spawn.position = player1SpawnPos;
        //player2Spawn.position = player2SpawnPos;
        player1 = Instantiate(player1Prefab, player1SpawnPos, Quaternion.identity, player1Spawn);
        AddPlayer(player1.GetComponent<PlayerInput>());
        player2 = Instantiate(player2Prefab, player2SpawnPos, Quaternion.identity, player2Spawn);
        AddPlayer(player2.GetComponent<PlayerInput>());
    }

    public void AddPlayer(PlayerInput player)
    {
        players.Add(player);
    }

}
