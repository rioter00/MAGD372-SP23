using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

[System.Serializable]
public class Panel
{
    public MenuPanel name;
    public GameObject panel;
    public GameObject backButton;
}

public enum MenuPanel
{
    Options, HowToPlay, Credits
}

public class MenuManager : MonoBehaviour
{
    public List<Panel> Panels;
    public GameObject menuButtons;
    private GameObject currentActivePanel;
    public int gameStartScene;
    [SerializeField] private EventSystem eventSystem;

    private void Start()
    {
        currentActivePanel = menuButtons;
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
        currentActivePanel = Panels.First(item => item.name == MenuPanel.Options).panel;
        currentActivePanel.SetActive(true);
        menuButtons.SetActive(false);
        eventSystem.SetSelectedGameObject(Panels.First(item => item.name == MenuPanel.Options).backButton);
    }

    public void OpenHowToPlayPanel()
    {
        currentActivePanel = Panels.First(item => item.name == MenuPanel.HowToPlay).panel;
        currentActivePanel.SetActive(true);
        menuButtons.SetActive(false);
        eventSystem.SetSelectedGameObject(Panels.First(item => item.name == MenuPanel.HowToPlay).backButton);
    }

    public void OpenCreditsPanel()
    {
        currentActivePanel = Panels.First(item => item.name == MenuPanel.Credits).panel;
        currentActivePanel.SetActive(true);
        menuButtons.SetActive(false);
        eventSystem.SetSelectedGameObject(Panels.First(item => item.name == MenuPanel.Credits).backButton);
    }

    public void BackButton(MenuOption option)
    {
        Panels.First(item => item.name == option.panelName).panel.SetActive(false);
        menuButtons.SetActive(true);
        currentActivePanel = menuButtons;
        eventSystem.SetSelectedGameObject(menuButtons.transform.GetChild(0).gameObject);
    }

    public MenuOption GetCurrentActivePanel()
    {
        return currentActivePanel.transform.GetChild(0).gameObject.GetComponent<MenuOption>();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameStartScene);
    }
}
