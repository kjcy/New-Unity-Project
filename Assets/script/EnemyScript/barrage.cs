using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrage : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        hp = 999;
        enemyType = type.barrage;
        Destroy(this.gameObject, 15f);
    }


}
