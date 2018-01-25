using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Written by Jared Nealon
/// Copyright 2018 - Group 2 of class CMV2A
/// </summary>
public class BaseCharacter : MonoBehaviour
{
    // Managers tracker
    GameManager managerGame;
    DialogManager managerDialog;

    // Internal variables
    public Dialog myDialog;

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
        gameObject.tag = "Character";
        SpawnMinimapIcon();

        managerDialog = GameObject.Find("System/LevelManager").GetComponent<DialogManager>();
    }

    // Instantiate a minimap icon from the Resources folder
    void SpawnMinimapIcon()
    {
        GameObject newMinimapIconPrefab = Resources.Load("Common/CharacterMinimapIcon") as GameObject;
        GameObject newMinimapIcon = Instantiate(newMinimapIconPrefab, transform.position, Quaternion.identity);
        newMinimapIcon.name = "MinimapIcon";
        newMinimapIcon.transform.parent = transform;
    }

    // Ask the dialog manager to enable the dialog system
    public void Talk()
    {
        managerDialog.StartTalking(myDialog, gameObject.name);
    }
}
