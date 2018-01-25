using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Written by Jared Nealon
/// Copyright 2018 - Group 2 of class CMV2A
/// </summary>
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

    // Instantiate a minimap icon from the Resources folder
    void SpawnMinimapIcon()
    {
        GameObject newMinimapIconPrefab = Resources.Load("Common/EnemyMinimapIcon") as GameObject;
        GameObject newMinimapIcon = Instantiate(newMinimapIconPrefab, transform.position, Quaternion.identity);
        newMinimapIcon.name = "MinimapIcon";
        newMinimapIcon.transform.parent = transform;
    }

    // Look at the player unless they are lured, and move towards the lure
    private void Update()
    {
        if (!lured)
        {
            transform.LookAt(player);
        }
        else
        {
            // If the lure object still exist, head to the lure
            if (lureObject)
            {
                if (Vector3.Distance(transform.position, lureObject.transform.position) > 2.0f)
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

    // Enable this enemy to be lured
    public void Lure(GameObject location)
    {
        lureObject = location;
        lured = true;
    }
}
