using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWinner : MonoBehaviour
{
    public string areaName = "";
    GameManager managerGame;
    GUIManager managerGUI;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            managerGame.CompleteTask(Objective.Type.Enter, areaName);
            managerGUI.ShowWin();
        }
    }
}
