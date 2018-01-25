using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Written by Jared Nealon
/// Copyright 2018 - Group 2 of class CMV2A
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    // Private
    private bool confirmLoad;
    private enum MenuPanel { MainMenu, PlayMode, Controls, Credits };
    private GameManager managerGame;

    // Public
    public Animator menuAnimation;
    public GameObject panelMainMenu;
    public GameObject panelPlayModes;
    public GameObject panelControls;
    public GameObject panelCredits;

    // Use this for initialization
    private void Awake()
    {
#if UNITY_EDITOR
        if (GameObject.Find("GameManager"))
        {
            managerGame = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        else
        {
            GameObject newGameManagerPrefab = Resources.Load("Common/GameManager") as GameObject;
            GameObject newGameNager = Instantiate(newGameManagerPrefab);
            newGameNager.name = "GameManager";
            managerGame = newGameNager.GetComponent<GameManager>();
        }
#else
        managerGame = GameObject.Find("GameManager").GetComponent<GameManager>();
#endif
    }

    // Use this for second initialization
    void Start ()
    {
        ActivatePanel(MenuPanel.MainMenu);
    }


    // Handle panel switching
    void ActivatePanel(MenuPanel panel)
    {
        panelMainMenu.SetActive(false);
        panelPlayModes.SetActive(false);
        panelControls.SetActive(false);
        panelCredits.SetActive(false);

        switch(panel)
        {
            case MenuPanel.MainMenu:
                panelMainMenu.SetActive(true); break;
            case MenuPanel.PlayMode:
                panelPlayModes.SetActive(true); break;
            case MenuPanel.Controls:
                panelControls.SetActive(true); break;
            case MenuPanel.Credits:
                panelCredits.SetActive(true); break;
            default:
                Debug.Log("FUCKING ERROR HAS OCCURED!"); break;
        }
    }

    public void ActivateMainMenu()
    {
        ActivatePanel(MenuPanel.MainMenu);
        menuAnimation.SetFloat("ToPig", 0);
        menuAnimation.SetFloat("ToCow", 0);
        menuAnimation.SetFloat("ToFarm", 0);
    }

    public void ActivatePlayMode(bool loadGame)
    {
        confirmLoad = loadGame;

        ActivatePanel(MenuPanel.PlayMode);
        menuAnimation.SetFloat("ToPig", 1);
        menuAnimation.SetFloat("ToCow", 0);
        menuAnimation.SetFloat("ToFarm", 0);
    }

    public void ChoosePlayMode(int playMode)
    {
        switch (playMode)
        {
            case 0:
                SetPlayMode(GameManager.GameMode.LifeWithoutParole); break;
            case 1:
                SetPlayMode(GameManager.GameMode.DeathRow); break;
            default:
                Debug.Log("FUCKING ERROR HAS OCCURED!"); break;
        }
        
    }

    public void SetPlayMode(GameManager.GameMode gameMode)
    {
        managerGame.setGameMode = gameMode;

        switch (confirmLoad)
        {
            case true:
                Debug.Log("TODO: LOAD GAME"); break;
            case false:
                Debug.Log("TODO: NEW GAME"); break;
            default:
                Debug.Log("FUCKING ERROR HAS OCCURED!"); break;
        }

        ActivateControls();
    }

    public void ActivateControls()
    {
        ActivatePanel(MenuPanel.Controls);
        menuAnimation.SetFloat("ToPig", 1);
        menuAnimation.SetFloat("ToCow", 1);
        menuAnimation.SetFloat("ToFarm", 0);
    }

    public void ActivateCredits()
    {
        ActivatePanel(MenuPanel.Credits);
        menuAnimation.SetFloat("ToPig", 1);
        menuAnimation.SetFloat("ToCow", 1);
        menuAnimation.SetFloat("ToFarm", 1);
    }

    public void PlayGame()
    {

        menuAnimation.SetFloat("ToPig", 1);
        menuAnimation.SetFloat("ToCow", 1);
        menuAnimation.SetFloat("ToFarm", 1);
        managerGame.LoadLevel("FarmFinal");
    }
}
