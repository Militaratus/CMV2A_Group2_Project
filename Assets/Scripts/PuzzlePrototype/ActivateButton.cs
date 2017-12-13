using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateButton : MonoBehaviour
{
    public bool activated = false;

	// Use this for initialization
	void Start ()
    {
        transform.GetChild(2).GetComponent<Renderer>().material.color = Color.clear;

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (activated && transform.GetChild(0).localPosition.y > -0.1f)
        {
            transform.GetChild(0).Translate(Vector3.down * (Time.deltaTime / 2));
        }

        if (activated && transform.GetChild(1).localPosition.y > 0)
        {
            transform.GetChild(1).Translate(Vector3.down * (Time.deltaTime / 2));
        }

        if (activated && transform.GetChild(0).localPosition.y <= -0.1f && transform.GetChild(1).localPosition.y <= 0)
        {
            transform.GetChild(2).GetComponent<Renderer>().material.color = Color.yellow;
        }

        if (activated && transform.GetChild(0).GetChild(0).gameObject.name == "Player")
        {
            transform.GetChild(0).GetChild(0).localPosition = new Vector3(0, 2, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Pickup>())
        {
            other.transform.GetComponent<Pickup>().LetGo();
            other.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            other.transform.SetParent(transform.GetChild(0));
            other.transform.localPosition = new Vector3(0, 6.0f, 0);
            activated = true;
        }

        if (other.gameObject.name == "Player")
        {
            other.transform.SetParent(transform.GetChild(0));
            other.transform.localPosition = new Vector3(0, 3.5f, 0);
            activated = true;
        }
    }
}
