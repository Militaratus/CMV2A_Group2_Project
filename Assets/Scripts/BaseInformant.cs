using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInformant : MonoBehaviour
{
    public bool amActive = false;
    public bool amTalking = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (amActive && !amTalking)
        {
            EnableDialogue();
        }
	}

    void EnableDialogue()
    {
        amTalking = true;
    }

    void DisableDialogue()
    {
        amActive = false;
        amTalking = false;
    }
}
