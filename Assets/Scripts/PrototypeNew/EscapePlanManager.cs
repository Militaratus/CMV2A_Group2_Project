using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Written by Jared Nealon
/// Copyright 2018 - Group 2 of class CMV2A
/// </summary>
public class EscapePlanManager : MonoBehaviour
{
    // Manager References
    public GameManager managerGame;
    public GUIManager managerGUI;

    // Escape Plan
    public EscapePlan myPlan;

    public Text textTitle;
    public Text textDescription;

    public Toggle[] TaskToggle;
    public Text[] TaskLabel;

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
        managerGame.LoadTasks();
        myPlan = managerGame.activeEscapePlan;
        FillPanel();
    }

    // Update the UI elements with the Escape Plan data
    void FillPanel()
    {
        textTitle.text = myPlan.title;
        textDescription.text = myPlan.description.Replace("NEWLINE", "\n");

        for (int i = 0; i < managerGame.tasks.Length; i++)
        {
            if (managerGame.tasks[i] == GameManager.Task.Complete)
            {
                TaskToggle[i].isOn = true;
                FillLabel(i);
            }
            else if (managerGame.tasks[i] == GameManager.Task.Incomplete)
            {
                TaskToggle[i].isOn = false;
                FillLabel(i);
            }
            else
            {
                TaskToggle[i].gameObject.SetActive(false);
            }
        }
    }

    // Autofill the task text depending on task type and target name
    void FillLabel(int slot)
    {
        string taskString = "";
        switch (myPlan.objectives[slot].taskType)
        {
            case Objective.Type.Collect:
                taskString = "Collect a "; break;
            case Objective.Type.Enter:
                taskString = "Enter the "; break;
            case Objective.Type.Talk:
                taskString = "Talk to "; break;
        }
        taskString = taskString + myPlan.objectives[slot].taskTarget;
        TaskLabel[slot].text = taskString;
    }

    // Toggle the Task Checkbox to be checked
    public void UpdatePanel(int slot)
    {
        TaskToggle[slot].isOn = true;
    }
}
