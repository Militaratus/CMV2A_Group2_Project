using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager managerGame;
    public GUIManager managerGUI;
    public GameObject activeInventoryItem;

    UnityStandardAssets.Characters.FirstPerson.FirstPersonController saFPC;
    Transform myCamera;
    RaycastHit hit;

    // Use this for initialization
    void Awake ()
    {
        // Get Managers
#if UNITY_EDITOR
        if (GameObject.Find("GameManager"))
        {
            managerGame = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        else
        {
            GameObject newGameManagerPrefab = Resources.Load("Common/GameManager") as GameObject;
            GameObject newGameNager = Instantiate(newGameManagerPrefab);
            newGameNager.name = "GameManager";
            managerGame = newGameNager.GetComponent<GameManager>();
        }
#else
        managerGame = GameObject.Find("GameManager").GetComponent<GameManager>();
#endif

        // Set Camera reference to shortcut
        myCamera = Camera.main.transform;

        // Get Standard Asset Component
        saFPC = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        ExtraInput();
	}

    void ExtraInput()
    {
        if (Input.GetButton("Crouch"))
        {
            saFPC.m_UseHeadBob = false;
            myCamera.localPosition = new Vector3(0, 0.2f, 0);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            saFPC.m_UseHeadBob = true;
            myCamera.localPosition = new Vector3(0, 0.8f, 0);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            switch(hit.transform.tag)
            {
                case "Character":
                    Debug.Log("Open Dialog"); break;
                case "Item":
                    hit.transform.GetComponent<BaseItem>().Collect(); break;
                default:
                    Debug.Log("ERROR: Player is shooting at ghosts!"); break;
            }
        }

        if (Input.GetButtonUp("Fire2"))
        {
            DropInventory();
        }

        if (Input.GetButtonUp("Inventory1"))
        {
            GetInventory(0);
        }
        else if (Input.GetButtonUp("Inventory2"))
        {
            GetInventory(1);
        }
        else if (Input.GetButtonUp("Inventory3"))
        {
            GetInventory(2);
        }
        else if (Input.GetButtonUp("Inventory4"))
        {
            GetInventory(3);
        }
        else if (Input.GetButtonUp("Inventory5"))
        {
            GetInventory(4);
        }
        else if (Input.GetButtonUp("Inventory6"))
        {
            GetInventory(5);
        }
    }

    private void FixedUpdate()
    {
        Vector3 fwd = myCamera.TransformDirection(Vector3.forward);

        if (Physics.Raycast(myCamera.position, fwd, out hit) && !Blocked())
        {
            switch(hit.transform.tag)
            {
                case "Character":
                    managerGUI.ShowInteractIconTalk(); break;
                case "Item":
                    managerGUI.ShowInteractIconCollect(); break;
                default:
                    managerGUI.HideInteractionIcon(); break;
            }
        }
        else
        {
            managerGUI.HideInteractionIcon();
        }
    }

    bool Blocked()
    {
        bool amBlocked = false;

        return amBlocked;
    }

    void GetInventory(int slot)
    {
        if (activeInventoryItem)
        {
            activeInventoryItem.SetActive(false);
        }

        GameObject prevActive = activeInventoryItem;
        if (managerGame.GetInventory(slot))
        {
            activeInventoryItem = managerGame.GetInventory(slot);

            if (prevActive != activeInventoryItem)
            {
                activeInventoryItem.SetActive(true);
            }
            else
            {
                activeInventoryItem = null;
            }
        }
    }

    void DropInventory()
    {
        // Check if I am holding something
        if (!activeInventoryItem)
        {
            return;
        }

        managerGame.DropInventory(activeInventoryItem);
        activeInventoryItem = null;
    }
}
