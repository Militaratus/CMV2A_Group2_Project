using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Written by Jared Nealon
/// Copyright 2018 - Group 2 of class CMV2A
/// </summary>
public class BaseArea : MonoBehaviour
{
    public string areaName = "";
    GameManager managerGame;

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

    // Send a task completion event to the GameManager if the player entered the Trigger zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            managerGame.CompleteTask(Objective.Type.Enter, areaName);
        }
    }

}
