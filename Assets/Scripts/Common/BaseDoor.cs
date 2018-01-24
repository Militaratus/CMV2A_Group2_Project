using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDoor : MonoBehaviour
{
    // Managers
    GameManager managerGame;


    public GameObject key;
    public string levelName;

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
