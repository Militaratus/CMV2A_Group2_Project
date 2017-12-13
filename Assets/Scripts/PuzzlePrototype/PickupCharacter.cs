using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCharacter : Pickup
{
    bool goingToButton = false;
    public Transform buttonPosition;
    public Player scriptPlayer;

    public override void PickedUp()
    {
        Rigidbody myBody = GetComponent<Rigidbody>();
        myBody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public override void LetGo()
    {
        transform.SetParent(null);
        Rigidbody myBody = GetComponent<Rigidbody>();
        myBody.constraints = RigidbodyConstraints.FreezeRotation;

    }

    public void GotoButton()
    {
        goingToButton = true;
        scriptPlayer.DoneTalking();
    }

    private void Update()
    {
        if (goingToButton)
        {
            transform.position = Vector3.MoveTowards(transform.position, buttonPosition.position, Time.deltaTime * 2);
        }
    }
}
