using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameManager gameManager;
    public Transform barrageParent;

    [SerializeField]
    private GameObject[] barragebox = new GameObject[5];

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(pattenCor());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator pattenCor()
    {
        int i = 0;
        do
        {
            GameObject temp = Instantiate(barragebox[0], new Vector3(15, -3, 0), Quaternion.identity, barrageParent);
            temp.GetComponent<barrage>().enemyManager = this;
            temp.GetComponent<barrage>().MoveEnemy(new Vector3(-15, -3, 0), 500f);
            yield return new WaitForSeconds(5f);
            GameObject temp2 = Instantiate(barragebox[1], new Vector3(15, -3, 0), Quaternion.identity, barrageParent);
            temp2.GetComponent<parringbarrage>().enemyManager = this;
            temp2.GetComponent<parringbarrage>().MoveEnemy(new Vector3(-15, -3, 0), 500f);
            yield return new WaitForSeconds(5f);
        } while (gameManager.play);
    }

}
