using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject foothold;
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
                pattentime += 1;
            

            if(mainBoss.GetComponent<Enemy>().hp<100)
            {
                if (Mathf.Round(pattentime) % 500 == 0)
                {
                    if (mainBoss.transform.position.y < 0)
                    {
                        mainBoss.GetComponent<Enemy>().MoveEnemy(new Vector3(8, 2.5f, 0), 300);
                    }
                    else
                    {
                        mainBoss.GetComponent<Enemy>().MoveEnemy(new Vector3(8, -2.5f, 0), 300);
                    }
                }
                if(Mathf.Round(pattentime)%600 == 0) { 
                Debug.Log("패턴발사~~");
                StartCoroutine(pattenCor2());
                }
            }
            else { 
                if(pattentime % 1200 == 0)
                {
                    StartCoroutine(pattenCor());
                }else if(pattentime % 600 == 0)
                {
                    StartCoroutine(pattenCor1());
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

        for(int i = 0; i < 3; i++)
        {
            temp[i] = Instantiate(barragebox[0], mainBoss.transform.position - new Vector3(0,0.5f,0), Quaternion.identity, barrageParent);
            temp[i].GetComponent<Enemy>().MoveEnemy(new Vector3(temp[i].transform.position.x - 20f, temp[i].transform.position.y-0.5f, temp[i].transform.position.z), 150f);
            yield return new WaitForSecondsRealtime(1.22f);
        }

        

     
    }

    IEnumerator pattenCor1()
    {
        for(int i = 3; i < 7; i++)
        {
            temp[i] = Instantiate(barragebox[0], mainBoss.transform.position - new Vector3(0, 0.5f, 0), Quaternion.identity, barrageParent);
            temp[i].GetComponent<Enemy>().MoveEnemy(new Vector3(temp[i].transform.position.x - 20f, temp[i].transform.position.y, temp[i].transform.position.z), 150f);
            yield return new WaitForSecondsRealtime(0.75f);
        }
        temp[7] = Instantiate(barragebox[1], mainBoss.transform.position - new Vector3(0, 0.5f, 0), Quaternion.identity, barrageParent);
        temp[7].GetComponent<Enemy>().MoveEnemy(new Vector3(temp[7].transform.position.x - 20f, temp[7].transform.position.y, temp[7].transform.position.z), 150f);
       
    }

    IEnumerator pattenCor2()
    {
        GameObject footholdTemp = Instantiate(foothold, new Vector3(-3, -1, 0), Quaternion.identity);

        for(int i = 8; i < 15; i++)
        {
            temp[i] = Instantiate(barragebox[i%2], mainBoss.transform.position + new Vector3(0, Random.Range(-1, 1), 0), Quaternion.identity, barrageParent);
            yield return new WaitForSecondsRealtime(0.12f);
        }

        for(int i = 8; i < 15; i++)
        {
            temp[i].GetComponent<Enemy>().MoveEnemy(new Vector3(temp[i].transform.position.x - 20f, temp[i].transform.position.y, temp[i].transform.position.z), 150f);
            yield return new WaitForSecondsRealtime(0.334f);
        }

        Destroy(footholdTemp);

    }


}
