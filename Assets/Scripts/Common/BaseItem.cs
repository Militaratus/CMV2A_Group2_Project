using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
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

    public void Collect()
    {
        if (managerGame.Collect(gameObject.name))
        {
            Destroy(gameObject);
        }
    }
}
