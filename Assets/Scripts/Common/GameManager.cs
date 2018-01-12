using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Task { None, Incomplete, Complete };
    public enum GameMode { LifeWithoutParole, DeathRow };
    internal GameMode setGameMode;
    public EscapePlan activeEscapePlan;
    public GameObject[] inventory = new GameObject[2];

    // Manager Tracker
    GUIManager managerGui;

    // Task Tracker
    public Task[] tasks = new Task[12];

    // Player Tracker
    public Transform player;

    // Ensure this is never destroyed
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    void LoadEscapePlan()
    {
        if (!activeEscapePlan)
        {
            Debug.Log("ERROR: No Escape Plan Loaded!");
            return;
        }


    }
    
    public bool Collect(string name)
    {
        bool collected = false;

        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;

            if (!player)
            {
                Debug.Log("ERROR: Where is the player?");
                return false;
            }
        }

        if(!managerGui)
        {
            managerGui = GameObject.Find("System/LevelManager").GetComponent<GUIManager>();

            if (!managerGui)
            {
                Debug.Log("ERROR: Where is GUI Manager?");
                return false;
            }
        }

        int openSlot = FindEmptySlot();
        if (openSlot != -1)
        {
            // Spawn the inventory item for easy retrieval
            GameObject newItemPrefab = Resources.Load("Items/" + name) as GameObject;
            GameObject newItem = Instantiate(newItemPrefab);
            newItem.name = newItem.name.Replace("(Clone)", "");
            newItem.transform.parent = player.GetChild(0);
            newItem.transform.localPosition = new Vector3(0, -0.8f, 1);
            newItem.SetActive(false);

            // Add it to the inventory
            inventory[openSlot] = newItem;
            managerGui.ShowInventory(openSlot, name);
            

            collected = true;
        }

        return collected;
    }

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

    public GameObject GetInventory(int slot)
    {
        if ((inventory.Length - 1)  < slot)
        {
            Debug.Log("WARNING: Inventory not expanded yet!");
            return null;
        }

        return inventory[slot];
    }

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
}
