using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : Enemy
{

    public int bossId;

    public GameObject[] bulletBox;

    // Start is called before the first frame update
    void Start()
    {
        enemyType = type.boss;
        switch (bossId) {
            case 0:
            hp = 500;
                break;
            case 1:
                hp = 700;
                break;
        }
}

    // Update is called once per frame


    void Update()
    {
    }

   

}
