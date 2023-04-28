using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Essentials.Reference_Variables.Variables;
using TMPro;


public enum Player
{
    player1,
    player2
}


public enum GameStage
{
    settingsScreen,
    instructions,
    mainMenu,
    roundBeginning,
    inRound,
    roundEnding,
    gameEnding
}


public class GameManager : MonoBehaviour
{

    
    private GameStage gameStage;
    private bool player1Paused; //whether the first player has the game put on paused - local pause
    private bool player2Paused; //whether the second player has the game put on paused - local pause
    private bool gamePaused; //whether gameplay is paused over all - global pause

    private float timeLeftInRound; //time left in round in seconds

    private float team1WaterLevel; //water level of team 1's well
    private float team2WaterLevel; //water level of team 2's well

    //public Fill team1WaterMeter;
    //public Fill team2WaterMeter;
    public TMP_Text timerText;
    public Canvas settingsScreen;
    public Canvas instructionsScreen;
    public Canvas menuScreen; //main menu screen
    public Canvas pauseScreenPlayer1;
    public Canvas pauseScreenPlayer2;

    public GameObject team1WaterWell;
    public GameObject team2WaterWell;

    public FloatVariable player1Pause;
    public FloatVariable player2Pause;

    Transform player1Spawnpoint;
    Transform player2Spawnpoint;

    float waterLvlForVictory; //the accumulated amount of water a player must bring back to their well in order for their team to win



    private void updateTimerUI()
    {

        timerText.text = timeLeftInRound.ToString();
    }

    private void updateWaterLevelGraphics()
    {

        //waterLevelUI.text = "Water level: " + (String) team1WaterLevel;
    }


    public GameStage getGameStage()
    {
        return gameStage;
    }

    public void setGameStage(GameStage stage)
    {
        settingsScreen.enabled = false;
        instructionsScreen.enabled = false;
        menuScreen.enabled = false;
        pauseScreenPlayer1.enabled = false;
        pauseScreenPlayer2.enabled = false;
        gameStage = stage;

        switch (gameStage)
        {
            case GameStage.settingsScreen:
                settingsScreen.enabled = true;
                gamePaused = true;
                break;


            case GameStage.instructions:
                instructionsScreen.enabled = true;
                gamePaused = true;
                break;

            case GameStage.mainMenu:
                menuScreen.enabled = true;
                gamePaused = true;
                break;

            case GameStage.roundBeginning:
                setTeam1WaterLevel(0);
                setTeam2WaterLevel(0);
                break;

            case GameStage.inRound:
                setTeam1WaterLevel(0);
                setTeam2WaterLevel(0);
                break;

            default:
                gamePaused = false;
                break;
        }
    }


    public void setTeam1WaterLevel(float level)
    {
        team1WaterLevel = 0;

        updateWaterLevelGraphics();
    }

    public void setTeam2WaterLevel(float level)
    {
        team2WaterLevel = 0;

        updateWaterLevelGraphics();
    }

    public void addWaterToTeam1Well(float amount)
    {
        team1WaterLevel += amount;

        if (team1WaterLevel >= waterLvlForVictory)
        {

            setGameStage(GameStage.roundBeginning);
        }


        updateWaterLevelGraphics();
    }

    public void addWaterToTeam2Well(float amount)
    {
        team2WaterLevel += amount;

        if (team2WaterLevel >= waterLvlForVictory)
        {

            setGameStage(GameStage.roundBeginning);
        }

        updateWaterLevelGraphics();
    }

    private void setGamePaused(bool paused)
    {

        gamePaused = paused;

        if (gamePaused)
        {

            Time.timeScale = 0;
        }
        else
        {

            Time.timeScale = 1;
        }
    }

    private void OnPlayer1Paused(object sender, System.EventArgs args)
    {

        if (player1Paused)
        {
            togglePauseGameAs((int)Player.player1);
        }
    }

    private void OnPlayer2Paused(object sender, System.EventArgs args)
    {

        if (player2Paused)
        {
            togglePauseGameAs((int)Player.player2);
        }
    }

    //Pause the game from a particular player 1, 2's perspective.
    public void togglePauseGameAs(int asPlayer)
    {

        if (asPlayer == (int)Player.player1)
        {
            player1Paused = !player1Paused;

            pauseScreenPlayer1.enabled = player1Paused;

        }
        else
        {
            player2Paused = !player2Paused;

            pauseScreenPlayer2.enabled = player2Paused;
        }

        setGamePaused(player1Paused || player2Paused);
    }


    public void Awake()
    {

        player1Pause.ValueChanged += OnPlayer1Paused;
        player2Pause.ValueChanged += OnPlayer2Paused;
    }


    public void Start()
    {

        setGameStage(GameStage.mainMenu);

    }


    //Main co-routine for mid-round
    public IEnumerator mainGameLoop()
    {

        timeLeftInRound += Time.deltaTime;
        updateTimerUI();

        yield return mainGameLoop();
    }
}
