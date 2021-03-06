﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Written by Jared Nealon
/// Copyright 2018 - Group 2 of class CMV2A
/// </summary>
public class PlayerController : MonoBehaviour
{
    GUIManager.MenuPanel openMenu = GUIManager.MenuPanel.HUD;

    public GameManager managerGame;
    public GUIManager managerGUI;
    public GameObject activeInventoryItem;

    Transform myCamera;
    RaycastHit hit;

    // Unity Standard Assets reference of the First Person Controller, which I slightly changed the variable accessability
    UnityStandardAssets.Characters.FirstPerson.FirstPersonController saFPC;

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
        managerGUI = GameObject.Find("System/LevelManager").GetComponent<GUIManager>();

        // Set Camera reference to shortcut
        myCamera = Camera.main.transform;

        // Get Standard Asset Component
        saFPC = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();

        // Grab Inventory from previous load
        GrabInventory();
    }

    // Grab Inventory from previous load
    void GrabInventory()
    {
        managerGame.GiveInventory();
    }
	
	// Update is called once per frame
	void Update ()
    {
        ExtraInput();
        FuckGravity();

    }

    // Input Manager on top of the Standard Assets FPC
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

        if (Input.GetButtonDown("Fire1") && !Blocked())
        {
            switch(hit.transform.tag)
            {
                case "Character":
                    hit.transform.GetComponent<BaseCharacter>().Talk(); break;
                case "Item":
                    hit.transform.GetComponent<BaseItem>().Collect(); break;
                case "Door":
                    hit.transform.GetComponent<BaseDoor>().Open(); break;
                case "Use":
                    hit.transform.GetComponent<BaseUse>().Use(); break;
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

        if (Input.GetButtonUp("ShowEscapePlan"))
        {
            if(openMenu != GUIManager.MenuPanel.EscapePlan)
            {
                OpenMenu(GUIManager.MenuPanel.EscapePlan);
            }
            else
            {
                CloseMenu();
            }
        }
    }

    // Name based on a popular meme, disables gravity and lightens the load
    void FuckGravity()
    {
        if (activeInventoryItem == null)
        {
            return;
        }

        if (activeInventoryItem.name == "Chicken Flyer")
        {
            saFPC.m_GravityMultiplier = 1;
            GetComponent<Rigidbody>().useGravity = false;
        }
        else
        {
            saFPC.m_GravityMultiplier = 3;
            GetComponent<Rigidbody>().useGravity = false;
        }
    }

    void OpenMenu(GUIManager.MenuPanel menu)
    {
        openMenu = menu;
        managerGUI.ActivatePanel(openMenu);
    }

    void CloseMenu()
    {
        openMenu = GUIManager.MenuPanel.HUD;
        managerGUI.ActivatePanel(openMenu);
    }

    // Raycast is heavy on the game, using FixedUpdate to limit the amount it is called/generated
    private void FixedUpdate()
    {
        Vector3 fwd = myCamera.TransformDirection(Vector3.forward);

        if (Physics.Raycast(myCamera.position, fwd, out hit) && !Blocked())
        {
            if (hit.distance < 10.0f)
            {
                switch (hit.transform.tag)
                {
                    case "Character":
                        managerGUI.ShowInteractIconTalk(); break;
                    case "Item":
                        managerGUI.ShowInteractIconCollect(); break;
                    case "Door":
                        CheckLock(); break;
                    case "Use":
                        managerGUI.ShowInteractIconUse(); break;
                    default:
                        managerGUI.HideInteractionIcon(); break;
                }
            }
            else
            {
                managerGUI.HideInteractionIcon();
            }
        }
        else
        {
            managerGUI.HideInteractionIcon();
        }
    }

    void CheckLock()
    {
        // Does it need a key?
        if (!hit.transform.GetComponent<BaseDoor>().key)
        {
            managerGUI.ShowInteractIconOpen();
            return;
        }

        // Do I have the key?
        if (managerGame.CheckKey(hit.transform.GetComponent<BaseDoor>().key.name))
        {
            managerGUI.ShowInteractIconOpen();
        }
        else
        {
            managerGUI.ShowInteractIconLocked();
        }
    }

    // Disable Raycast if the HUD is not active
    bool Blocked()
    {
        bool amBlocked = false;

        if (managerGUI.activePanel != GUIManager.MenuPanel.HUD)
        {
            amBlocked = true;
            saFPC.m_MouseLook.SetCursorLock(false);
        }
        else
        {

            saFPC.m_MouseLook.SetCursorLock(true);
        }

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
