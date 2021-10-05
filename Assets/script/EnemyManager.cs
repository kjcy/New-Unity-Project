using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameManager gameManager;
    public Transform barrageParent;

    private GameObject[] temp = new GameObject[20];

    public GameObject mainBoss;

    private float pattentime;


    [SerializeField]
    private GameObject[] barragebox = new GameObject[5];

    // Start is called before the first frame update
    void Start()
    {

        mainBoss = Instantiate(gameManager.bossPripab, new Vector3(8, -2, 0), Quaternion.identity);
        mainBoss.GetComponent<boss>().enemyManager = this;
    //    StartCoroutine(pattenCor());
        
    }

    // Update is called once per frame
    void Update()
    {
         if(gameManager.play) { 
                pattentime += Time.deltaTime;
            if(Mathf.Round( pattentime) % 120 == 0)
            {
                if (mainBoss.transform.position.y < 0) { 
                mainBoss.GetComponent<Enemy>().MoveEnemy(new Vector3(8, 2.5f, 0), 300);
                }
                else
                {
                    mainBoss.GetComponent<Enemy>().MoveEnemy(new Vector3(8, 2.5f, 0), 300);
                }
            }


        }
    }

    public void BossDie()
    {
        for(int i = 0; i < barrageParent.childCount; i++)
        {
            Destroy(barrageParent.GetChild(i).gameObject);
        }
        gameManager.play = false;
    }

    IEnumerator pattenCor()
    {





        yield return 0;
    }

}
