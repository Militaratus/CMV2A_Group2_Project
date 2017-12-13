using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public virtual void PickedUp()
    {
        Rigidbody myBody = GetComponent<Rigidbody>();
        myBody.constraints = RigidbodyConstraints.FreezeAll;
    }

    public virtual void LetGo()
    {
        transform.SetParent(null);
        Rigidbody myBody = GetComponent<Rigidbody>();
        myBody.constraints = RigidbodyConstraints.None;

    }
}
