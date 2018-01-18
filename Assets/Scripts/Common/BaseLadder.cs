using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLadder : MonoBehaviour
{
    GameObject player;

    private void Update()
    {
        if (player != null)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                player.transform.position += (Vector3.up / 5.0f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player = null;
        }
    }
}
