using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUse : MonoBehaviour
{
    GameManager managerGame;

    public enum ObjectDirection { Up, Down, Left, Right }

    public Transform myObject;
    public ObjectDirection objectDirection;

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
            default:
                Debug.Log("ERROR: YOU DONE FUBAR'D UP!"); break;
        }
	}
}
