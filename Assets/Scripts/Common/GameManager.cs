using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Written by Jared Nealon
/// Copyright 2018 - Group 2 of class CMV2A
/// </summary>
public class GameManager : MonoBehaviour
{
    public enum Task { None, Incomplete, Complete };
    public enum GameMode { LifeWithoutParole, DeathRow };
    internal GameMode setGameMode;
    public EscapePlan activeEscapePlan;
    public string[] keyInventory = new string[0];
    public GameObject[] inventory = new GameObject[2];
    public string[] lastInventory;

    public string nextScene = "";

    // Manager Tracker
    GUIManager managerGui;
    EscapePlanManager managerEscapePlan;

    // Task Tracker
    public Task[] tasks = new Task[12];

    // Player Tracker
    public Transform player;

    // Ensure this is never destroyed
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    // Reload Inventory on Scene Switch
    public void GiveInventory()
    {
        for (int i = 0; i < lastInventory.Length; i++)
        {
            if (lastInventory[i] != "" && lastInventory[i] != null)
            {
                Collect(lastInventory[i]);
            }
        }
    }

    // Add a key to the key inventory
    public void AddKey(string name)
    {
        string[] oldKeyInventory = new string[0];
        if (keyInventory.Length > 0)
        {
            oldKeyInventory = keyInventory;
        }
        
        keyInventory = new string[keyInventory.Length + 1];

        if (oldKeyInventory.Length > 0)
        {
            for (int i = 0; i < oldKeyInventory.Length; i++)
            {
                keyInventory[i] = oldKeyInventory[i];
            }
        }

        int lastID = keyInventory.Length - 1;
        keyInventory[lastID] = name;
    }

    // Check if we have the specified key, and return true if we do.
    public bool CheckKey(string name)
    {
        bool canOpen = false;

        for (int i = 0; i < keyInventory.Length; i++)
        {
            if (name == keyInventory[i])
            {
                canOpen = true;
            }
        }

        return canOpen;
    }
    
    // The function to have the object to be added to our inventory if we can, returns false if we have no space
    public bool Collect(string name)
    {
        bool collected = false;

        // We require the player to be in the scene
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;

            if (!player)
            {
                Debug.Log("ERROR: Where is the player?");
                return false;
            }
        }

        // We require the GUI Manager to update the HUD
        if(!managerGui)
        {
            managerGui = GameObject.Find("System/LevelManager").GetComponent<GUIManager>();

            if (!managerGui)
            {
                Debug.Log("ERROR: Where is GUI Manager?");
                return false;
            }
        }

        // Do we have a space to spare?
        int openSlot = FindEmptySlot();
        if (openSlot != -1)
        {
            // Spawn the inventory item for easy retrieval
            GameObject newItemPrefab = Resources.Load("Items/" + name) as GameObject;
            GameObject newItem = Instantiate(newItemPrefab);
            newItem.name = newItem.name.Replace("(Clone)", "");
            newItem.transform.parent = Camera.main.transform;
            newItem.transform.localPosition = new Vector3(0, -0.8f, 1);
            newItem.GetComponent<Collider>().enabled = false;
            newItem.GetComponent<Rigidbody>().useGravity = false;
            newItem.SetActive(false);

            // Add it to the inventory
            inventory[openSlot] = newItem;
            managerGui.ShowInventory(openSlot, name);

            // Complete Objective if this was a target
            CompleteTask(Objective.Type.Collect, newItem.name);

            collected = true;
        }

        return collected;
    }

    // Scan the inventory array if there is an unassigned spot
    int FindEmptySlot()
    {
        int emptySlot = -1;

        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                emptySlot = i;
                break;
            }
        }

        return emptySlot;
    }

    // Retrieve an item from a specific slot
    public GameObject GetInventory(int slot)
    {
        if ((inventory.Length - 1)  < slot)
        {
            Debug.Log("WARNING: Inventory not expanded yet!");
            return null;
        }

        return inventory[slot];
    }

    // Remove the item from the inventory array
    public void DropInventory(GameObject myItem)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == myItem)
            {
                managerGui.HideInventory(i);
                myItem.transform.parent = null;
                myItem.GetComponent<Collider>().enabled = true;
                myItem.GetComponent<Rigidbody>().useGravity = true;
                inventory[i] = null;
            }
        }
    }

    // Expand the inventory array from 2 to 6
    public void ExpandInventory()
    {
        GameObject[] oldInventory = inventory;
        inventory = new GameObject[6];
        for (int i = 0; i < oldInventory.Length; i++)
        {
            inventory[i] = oldInventory[i];
        }
        managerGui.ExpandInventory();
    }

    // Fill in the the task tracker array
    public void LoadTasks()
    {
        for (int i = 0; i < activeEscapePlan.objectives.Length; i++)
        {
            if (activeEscapePlan.objectives[i].taskType != Objective.Type.None)
            {
                if (tasks[i] != Task.Complete)
                {
                    tasks[i] = Task.Incomplete;
                }
            }
            else
            {
                tasks[i] = Task.None;
            }
        }
    }

    // Toggles a task to complete if it exists within the current escape plan
    public void CompleteTask(Objective.Type type, string target)
    {
        // We need the Escape Plan Manager for this
        if (!managerEscapePlan)
        {
            managerEscapePlan = GameObject.Find("System/LevelManager").GetComponent<EscapePlanManager>();

            if (!managerEscapePlan)
            {
                Debug.Log("ERROR: Where is Escape Plan Manager?");
                return;
            }
        }

        // See if the parameters match one in the active escape plan, set it to complete if it's a match
        for (int i = 0; i < activeEscapePlan.objectives.Length; i++)
        {
            if (activeEscapePlan.objectives[i].taskType == type && activeEscapePlan.objectives[i].taskTarget == target)
            {
                tasks[i] = Task.Complete;
                managerEscapePlan.UpdatePanel(i);
            }
        }
    }

    // Prepares the game for the loading screen to handle the scene it has to load
    public void LoadLevel(string nextLevel)
    {
        // Save the inventory to respawn upon gameplay scene load
        lastInventory = new string[inventory.Length];
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                lastInventory[i] = "";
            }
            else
            {
                lastInventory[i] = inventory[i].name;
            }
        }

        nextScene = nextLevel;
        SceneManager.LoadSceneAsync("LoadingScreen");
    }
}
