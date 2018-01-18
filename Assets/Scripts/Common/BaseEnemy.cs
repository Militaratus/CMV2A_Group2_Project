using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    GameManager managerGame;
    Transform player;

    Rigidbody myRigidbody;

    bool lured = false;
    GameObject lureObject;

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
        gameObject.tag = "Enemy";
        SpawnMinimapIcon();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        myRigidbody = GetComponent<Rigidbody>();
    }

    void SpawnMinimapIcon()
    {
        GameObject newMinimapIconPrefab = Resources.Load("Common/EnemyMinimapIcon") as GameObject;
        GameObject newMinimapIcon = Instantiate(newMinimapIconPrefab, transform.position, Quaternion.identity);
        newMinimapIcon.name = "MinimapIcon";
        newMinimapIcon.transform.parent = transform;
    }

    private void Update()
    {
        if (!lured)
        {
            transform.LookAt(player);
        }
        else
        {
            if (lureObject)
            {
                if (Vector3.Distance(transform.position, lureObject.transform.position) > 5.0f)
                {
                    transform.LookAt(lureObject.transform.position);
                    myRigidbody.velocity = transform.forward * 2.0f; ;
                }
            }
            else
            {
                lured = false;
            }
        }
    }

    public void Lure(GameObject location)
    {
        lureObject = location;
        lured = true;
    }
}
