using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRise : MonoBehaviour
{
    Quaternion sunNight = new Quaternion(0.9f, 0.0f, 0.0f, -0.4f);
    Quaternion sunDay = new Quaternion(0.7f, 0.0f, 0.0f, 0.7f);

    // Use this for initialization
    void Start ()
    {
        Debug.Log(transform.rotation);
        transform.rotation = sunNight;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //if (transform.rotation.w < sunDay.w)
        //{
        transform.rotation = Quaternion.Lerp(sunNight, sunDay, Time.time * 0.01f);
        //}

        //Debug.Log(transform.rotation);
        //transform.rotation = 
	}
}
