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

    private IEnumerator BossPattenCor = null;
    private bool stage2 = true;
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
        Patten();
    }

    public void Patten()
    {
        

        if (gameManager.play)
        {
            if(BossPattenCor == null)//막 처음 시작할때 패턴이 없다면 1페이지 코루틴을 시작한다.
            {
                BossPattenCor = PattenStage1Cor();
                StartCoroutine(BossPattenCor);
            }

            if(mainBoss.GetComponent<Enemy>().hp < 300&& stage2)//체력이 100이하 내려간다면 1페이지 코루틴을 종료하고 2페이지 코루틴을 실행한다.
            {
                stage2 = false;
                StopCoroutine(BossPattenCor);
                BossPattenCor = PattenStage2Cor();
                StartCoroutine(BossPattenCor);
            }




        }
    }

    //1페이즈 코루틴
    IEnumerator PattenStage1Cor()
    {
        do
        {
            
            for (int i = 0; i < 4; i++)
            {
                temp[i] = Instantiate(barragebox[0], mainBoss.transform.position + new Vector3(0,-1.3f,0), Quaternion.identity, barrageParent);
            }
            temp[4] = Instantiate(barragebox[1], mainBoss.transform.position + new Vector3(0, -1.3f, 0), Quaternion.identity, barrageParent);
            
            for(int i = 0; i < 5; i++)
            {
                temp[i].GetComponent<Enemy>().MoveEnemy(mainBoss.transform.position + new Vector3(-20, -1.3f, 0), 100f);
                yield return new WaitForSecondsRealtime(0.95f);
            }

            yield return new WaitForSecondsRealtime(3f);//3초 뒤에 다음 패턴

            for(int i = 0; i < 3; i++) { 
            temp[0] = Instantiate(barragebox[i%2], new Vector3(7,0,0), Quaternion.identity, barrageParent);
            temp[1] = Instantiate(barragebox[(i+1)%2], new Vector3(7, 0, 0), Quaternion.identity, barrageParent);

            temp[0].GetComponent<Enemy>().Uturn(new Vector3(-20, 3, 0), 250f, 3f);
            temp[1].GetComponent<Enemy>().Uturn(new Vector3(-20, -3, 0), 250f, -3f);

                yield return new WaitForSecondsRealtime(0.77f);
            }
            yield return new WaitForSecondsRealtime(3f);

        } while (true);
    }

   
    //2페이즈 코루틴
    IEnumerator PattenStage2Cor()
    {
        do
        {
            GameObject[] temp = new GameObject[5];
            for(int i = 0; i < 3; i++)
            {
                temp[i] = Instantiate(barragebox[0], new Vector3(-5 + i * 3, 5, 0), Quaternion.identity, barrageParent);
                temp[i].GetComponent<Enemy>().MoveEnemy(temp[i].transform.position + new Vector3(0, -3, 0), 150f);
            }
            yield return new WaitForSecondsRealtime(2f);
            
            for(int i = 0; i < 3; i++)
            {
                if (temp[i] == false) continue;
                temp[i].GetComponent<Enemy>().reflex(gameManager.PlayerManager.Player.transform.position, 50f);

                yield return new WaitForSecondsRealtime(0.77f);
                Destroy(temp[i]);
            }
           
            

            yield return new WaitForSecondsRealtime(5f);
        } while (true);
    }




    public void BossDie()
    {
        for(int i = 0; i < barrageParent.childCount; i++)
        {
            Destroy(barrageParent.GetChild(i).gameObject);
        }
        StopCoroutine(BossPattenCor);//보스가 죽을 경우 코루틴 종료
        gameManager.GameOver.Invoke();//게임이 끝났다는것을 알려준다.
    }

   

   


}
