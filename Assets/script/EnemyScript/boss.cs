using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : Enemy
{

    public int bossId;

    // Start is called before the first frame update
    void Start()
    {
        enemyType = type.boss;
        hp = 700;
    }

    // Update is called once per frame


    void Update()
    {
    }

   

}
