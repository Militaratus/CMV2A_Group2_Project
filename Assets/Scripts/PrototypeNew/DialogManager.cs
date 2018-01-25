using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Written by Jared Nealon
/// Copyright 2018 - Group 2 of class CMV2A
/// </summary>
public class DialogManager : MonoBehaviour
{
    // Manager References
    public GameManager managerGame;
    public GUIManager managerGUI;

    public Text textName;
    public Text textDialog;
    public Text textChoice1;
    public Text textChoice2;

    public GameObject choice1;
    public GameObject choice2;

    Dialog activeDialog;
    int currentChoice = 0;
    int choice1Path = 0;
    int choice2Path = 0;

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
        managerGUI = GameObject.Find("System/LevelManager").GetComponent<GUIManager>();
    }

    // Activate the Dialog system
    public void StartTalking (Dialog newDialog, string name)
    {
        activeDialog = newDialog;
        currentChoice = 0;
        textName.text = name;
        UpdateDialog();
        managerGUI.ActivatePanel(GUIManager.MenuPanel.Dialog);
    }
	
    // Update the UI elements with the new data
	void UpdateDialog ()
    {
        Dialogue curDialog = activeDialog.dialogue[currentChoice];
        textDialog.text = curDialog.responseText;

        if (curDialog.completesObjective)
        {
            managerGame.CompleteTask(Objective.Type.Talk, textName.text);
        }
        
        if (curDialog.choice1Text != "")
        {
            choice1.SetActive(true);
            textChoice1.text = curDialog.choice1Text;
            choice1Path = curDialog.choice1Path;
        }
        else
        {
            choice1.SetActive(false);
        }

        if (curDialog.choice2Text != "")
        {
            choice2.SetActive(true);
            textChoice2.text = curDialog.choice2Text;
            choice2Path = curDialog.choice2Path;
        }
        else
        {
            choice2.SetActive(false);
        }
    }

    public void Choice1()
    {
        currentChoice = choice1Path;
        UpdateDialog();
    }

    public void Choice2()
    {
        currentChoice = choice2Path;
        UpdateDialog();
    }

    public void Goodbye()
    {
        managerGUI.ActivatePanel(GUIManager.MenuPanel.HUD);
    }
}

[System.Serializable]
public struct DialogTree
{
    string responseText;
    string choice1text;
    string choice1response;
    string choice2text;
    string choice2response;

}
