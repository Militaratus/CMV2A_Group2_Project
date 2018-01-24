using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : BaseBribe
{
    public override void Lure(int enemyID)
    {
        if (!enemies[enemyID])
        {
            return;
        }

        if (!enemies[enemyID].GetComponent<GuardDog>())
        {
            return;
        }

        enemies[enemyID].GetComponent<GuardDog>().Lure(gameObject);
    }
}
