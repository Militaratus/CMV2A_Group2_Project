using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //
    public GameObject currentTarget;

	// Use this for initialization
	void Start ()
    {
        SpawnTarget();

    }

    void SpawnTarget()
    {
        GameObject targetModel = Resources.Load("Model1") as GameObject;
        currentTarget = Instantiate(targetModel, new Vector3(20, 0, 20), Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
