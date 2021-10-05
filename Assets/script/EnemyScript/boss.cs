using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        enemyType = type.boss;
        hp = 300;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDestroy()
    {
        enemyManager.BossDie();
    }

}
