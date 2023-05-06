using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private List<PlayerInput> players = new List<PlayerInput>();

    [SerializeField] private List<Transform> spawnPoints;


    [SerializeField] private PlayerInputManager playerInputManager;
    [SerializeField] private GameManager gameManager;
    private NewPlayerController newPlayerController;

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
    }

    public void Create()
    {
        Instantiate(playerPrefab);
        Instantiate(playerPrefab);

    }


    public void AddPlayer(PlayerInput player)
    {
        players.Add(player);
        player.transform.position = spawnPoints[players.Count - 1].position;

        if(players.Count == 1)
        {
            newPlayerController = player.gameObject.GetComponent<NewPlayerController>();
            newPlayerController.playerPausePanel = gameManager.pauseScreenPlayer1;
            int height = newPlayerController.GetCamera().pixelHeight;
            int width = newPlayerController.GetCamera().pixelWidth;
            newPlayerController.GetCamera().pixelRect = new Rect(0, height / 2, width, height/2);
            //player.actions.FindActionMap("Player").Disable();
        }
        if(players.Count == 2)
        {
            players[0].actions.FindActionMap("Player").Enable();
            newPlayerController = player.gameObject.GetComponent<NewPlayerController>();
            newPlayerController.playerPausePanel = gameManager.pauseScreenPlayer1;
            int height = newPlayerController.GetCamera().pixelHeight;
            int width = newPlayerController.GetCamera().pixelWidth;
            newPlayerController.GetCamera().pixelRect = new Rect(0, 0, width, height / 2);
            gameManager.StartTimer();
        }
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("player joined");
    }

}
