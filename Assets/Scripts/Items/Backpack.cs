using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Written by Jared Nealon
/// Copyright 2018 - Group 2 of class CMV2A
/// </summary>
public class Backpack : BaseItem
{
    // Expand the inventory instead of adding it to the inventory
    public override void Collect()
    {
        managerGame.ExpandInventory();

        // Complete Objective if this was a target
        managerGame.CompleteTask(Objective.Type.Collect, gameObject.name);

        gameObject.SetActive(false);
    }
}
