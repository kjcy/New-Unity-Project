using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parringbarrage : Enemy
{
    private void Start()
    {
        hp = 999;
        enemyType = type.paringBarrage;
        Destroy(this.gameObject, 15f);
    }
}
