using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject panelMainMenu;
    public GameObject panelPlayModes;
    public GameObject panelControls;
    public GameObject panelCredits;

    // Use this for initialization
    void Start ()
    {
        ActivatePanel("MainMenu");
    }

    void ActivatePanel(string panel)
    {
        panelMainMenu.SetActive(false);
        panelPlayModes.SetActive(false);
        panelControls.SetActive(false);
        panelCredits.SetActive(false);

        switch(panel)
        {
            case "MainMenu":
                panelMainMenu.SetActive(true); break;
            case "PlayMode":
                panelPlayModes.SetActive(true); break;
            case "Controls":
                panelControls.SetActive(true); break;
            case "Credits":
                panelCredits.SetActive(true); break;
            default:
                Debug.Log("FUCKING ERROR HAS OCCURED!"); break;
        }
    }

    public void ActivateMainMenu()
    {
        ActivatePanel("MainMenu");
    }

    public void ActivatePlayMode()
    {
        ActivatePanel("PlayMode");
    }

    public void ActivateControls()
    {
        ActivatePanel("Controls");
    }

    public void ActivateCredits()
    {
        ActivatePanel("Credits");
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("LoadingScreen");
    }
}
