using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUse : MonoBehaviour
{
    GameManager managerGame;

    public enum ObjectDirection { Up, Down, Left, Right, Forward, Backwards }

    public Transform myObject;
    public ObjectDirection objectDirection;

    bool used = false;

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
        gameObject.tag = "Use";
    }

    // Use this for initialization
    public void Use ()
    {
        if (used)
        {
            return;
        }

        managerGame.CompleteTask(Objective.Type.Use, gameObject.name);

		switch (objectDirection)
        {
            case ObjectDirection.Down:
                myObject.transform.position += Vector3.down * 10; break;
            case ObjectDirection.Left:
                myObject.transform.position += Vector3.left * 10; break;
            case ObjectDirection.Right:
                myObject.transform.position += Vector3.right * 10; break;
            case ObjectDirection.Up:
                myObject.transform.position += Vector3.up * 10; break;
            case ObjectDirection.Forward:
                myObject.transform.position += Vector3.forward * 2; break;
            case ObjectDirection.Backwards:
                myObject.transform.position += Vector3.back * 2; break;
            default:
                Debug.Log("ERROR: YOU DONE FUBAR'D UP!"); break;
        }

        used = true;

    }
}
