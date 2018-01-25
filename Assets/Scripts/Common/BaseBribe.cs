using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Written by Jared Nealon
/// Copyright 2018 - Group 2 of class CMV2A
/// </summary>
public class BaseBribe : MonoBehaviour
{
    public GameObject[] enemies;

	// Use this for initialization
	void Awake ()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
	
	// Check if the enemy is in range of the lure
	void Update ()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (Vector3.Distance(transform.position, enemies[i].transform.position) < 20.0f)
            {
                Lure(i);
            }
        }
	}

    // Send the lure request to the enemy
    public virtual void Lure(int enemyID)
    {
        if (!enemies[enemyID])
        {
            return;
        }

        if (!enemies[enemyID].GetComponent<BaseEnemy>())
        {
            return;
        }

        enemies[enemyID].GetComponent<BaseEnemy>().Lure(gameObject);
    }
}
