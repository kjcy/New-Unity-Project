using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameManager gameManager;
    public Transform barrageParent;

    private GameObject[] temp = new GameObject[20];

    public GameObject mainBoss;

    [SerializeField]
    private GameObject[] barragebox = new GameObject[5];

    // Start is called before the first frame update
    void Start()
    {

        mainBoss = Instantiate(gameManager.bossPripab, new Vector3(8, -2, 0), Quaternion.identity);
        mainBoss.GetComponent<boss>().enemyManager = this;
        StartCoroutine(pattenCor());
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        
        
        do
        {
            if (!gameManager.play) break;

            if (mainBoss.transform.position.y < 0) { 
            mainBoss.GetComponent<Enemy>().MoveEnemy(new Vector3(8, 2, 0), 300f);
            }
            else
            {
                mainBoss.GetComponent<Enemy>().MoveEnemy(new Vector3(8, -2, 0), 300f);
            }
            for (int i = 0; i < 4; i++)
            {
                temp[i] = Instantiate(barragebox[0], mainBoss.transform.position, Quaternion.identity, barrageParent);
                temp[i].GetComponent<Enemy>().enemyManager = this;
            }
            temp[4] = Instantiate(barragebox[1], mainBoss.transform.position, Quaternion.identity, barrageParent);
            temp[4].GetComponent<Enemy>().enemyManager = this;

            for (int i = 0; i < 5; i++)
            {
                temp[i].GetComponent<Enemy>().MoveEnemy(new Vector3(-10, -3 + i * 1.35f, 0), 100f);
                yield return new WaitForSeconds(0.57f);
            }

            yield return new WaitForSeconds(5f);
        } while (gameManager.play);

        yield return 0;
    }

}
