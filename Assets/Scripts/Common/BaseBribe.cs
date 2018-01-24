using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBribe : MonoBehaviour
{
    public GameObject[] enemies;

	// Use this for initialization
	void Awake ()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
	
	// Update is called once per frame
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
