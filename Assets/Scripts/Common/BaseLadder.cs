using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Written by Jared Nealon
/// Copyright 2018 - Group 2 of class CMV2A
/// </summary>
public class BaseLadder : MonoBehaviour
{
    GameObject player;

    // Push the player up if they are inside the Trigger zone and pushing up
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
    
    // Check if the player has entered the Trigger zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
        }
    }

    // Check if the player has left the Trigger zone
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player = null;
        }
    }
}
