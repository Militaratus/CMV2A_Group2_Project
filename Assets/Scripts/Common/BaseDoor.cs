using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Written by Jared Nealon
/// Copyright 2018 - Group 2 of class CMV2A
/// </summary>
public class BaseDoor : MonoBehaviour
{
    // Managers
    GameManager managerGame;

    // Required variables
    public GameObject key;
    public string levelName;

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
        gameObject.tag = "Door";
    }

    // Open the door if it's possible, which secretly calls to GameManager to handle the loading.
    public void Open()
    {
        if (!key)
        {
            managerGame.LoadLevel(levelName);
            return;
        }

        if (managerGame.CheckKey(key.name))
        {
            managerGame.LoadLevel(levelName);
        }
    }
}
