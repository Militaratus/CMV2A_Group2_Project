using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : BaseItem
{
    public override void Collect()
    {
        managerGame.ExpandInventory();
        gameObject.SetActive(false);
    }
}
