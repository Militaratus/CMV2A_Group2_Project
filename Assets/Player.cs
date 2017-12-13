using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool amTalking = false;
    public bool collectedSomething = false;
    public bool pickedSomething = false;
    float pickupCooldown = 0;

    public GameObject panelDialogue;

    RaycastHit hit;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadSceneAsync("LoadingScreen");
        }

        if (hit.distance < 4 && hit.transform.GetComponent<Pickup>() && !amTalking)
        {
            Debug.Log(hit.transform.gameObject.name);

            if (Input.GetMouseButtonDown(0) && Time.time > pickupCooldown)
            {
                Debug.Log("TIIRGGED");
                hit.transform.parent = transform.parent;
                hit.transform.localPosition = Vector3.forward;
                hit.transform.localRotation = Quaternion.identity;
                hit.transform.GetComponent<Pickup>().PickedUp();
                collectedSomething = true;
            }
        }

        if (hit.distance < 4 && hit.transform.GetComponent<PickupCharacter>())
        {
            if (Input.GetKeyDown(KeyCode.E) && !amTalking)
            {
                transform.parent.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_MouseLook.SetCursorLock(false);
                transform.parent.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
                panelDialogue.SetActive(true);
                amTalking = true;
            }
        }

        if (pickedSomething == true && !amTalking)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Transform exObject = transform.parent.GetChild(1);


                if (exObject.GetComponent<PickupCharacter>())
                {
                    exObject.SetParent(null);
                    exObject.GetComponent<PickupCharacter>().LetGo();
                    exObject.GetComponent<Rigidbody>().AddRelativeForce((Vector3.forward * 2) + (Vector3.up * 1), ForceMode.Impulse);
                }
                else
                {
                    exObject.SetParent(null);
                    exObject.GetComponent<Pickup>().LetGo();
                    exObject.GetComponent<Rigidbody>().AddRelativeForce((Vector3.forward * 75) + (Vector3.up * 5), ForceMode.Impulse);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                pickedSomething = false;
                pickupCooldown = Time.time + 1;
            }

        }

        if (collectedSomething == true && Input.GetMouseButtonUp(0))
        {
            collectedSomething = false;
            pickedSomething = true;
        }
    }

    void FixedUpdate ()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd, out hit) && collectedSomething == false && pickedSomething == false)
        {
            //
        }
    }

    public void DoneTalking()
    {
        
        transform.parent.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
        transform.parent.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().m_MouseLook.SetCursorLock(true);
        panelDialogue.SetActive(false);
        amTalking = false;
    }
}
