using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Panel
{
    public MenuPanel name;
    public GameObject panel;
}

public enum MenuPanel
{
    Options, HowToPlay, Credits
}

public class MenuManager : MonoBehaviour
{
    // public 
    public List<Panel> Panels;
    public int gameStartScene;

    private void Start()
    {
        foreach (var panel in Panels)
        {
            panel.panel.SetActive(false);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenOptionsPanel()
    {
        Panels.First(item => item.name == MenuPanel.Options).panel.SetActive(true);
    }
    
    public void OpenHowToPlayPanel()
    {
        Panels.First(item => item.name == MenuPanel.HowToPlay).panel.SetActive(true);
    }
    
    public void OpenCreditsPanel()
    {
        Panels.First(item => item.name == MenuPanel.Credits).panel.SetActive(true);
    }

    public void BackButton(MenuOption option)
    {
        Panels.First(item => item.name == option.panelName).panel.SetActive(false);
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(gameStartScene);
    }
}

