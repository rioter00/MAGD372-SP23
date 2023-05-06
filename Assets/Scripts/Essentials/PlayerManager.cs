using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private List<PlayerInput> players = new List<PlayerInput>();

    [SerializeField] private List<Transform> spawnPoints;


    [SerializeField] private PlayerInputManager playerInputManager;
    [SerializeField] private GameManager gameManager;

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
    }


    public void AddPlayer(PlayerInput player)
    {
        players.Add(player);
        player.transform.position = spawnPoints[players.Count - 1].position;

        if(players.Count == 1)
        {
            player.actions.FindActionMap("Player").Disable();
        }
        if(players.Count == 2)
        {
            players[0].actions.FindActionMap("Player").Enable();
            gameManager.StartTimer();
        }
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("player joined");
    }

}
