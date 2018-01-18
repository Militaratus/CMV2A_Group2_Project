using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBribe : MonoBehaviour
{
    public GameObject[] enemies;

	// Use this for initialization
	void Start ()
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
                enemies[i].GetComponent<BaseEnemy>().Lure(gameObject);
            }
        }
	}
}
