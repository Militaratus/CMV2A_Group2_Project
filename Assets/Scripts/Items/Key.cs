using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : BaseItem
{
    public override void Collect()
    {
        managerGame.AddKey(gameObject.name);
        gameObject.SetActive(false);
    }
}
