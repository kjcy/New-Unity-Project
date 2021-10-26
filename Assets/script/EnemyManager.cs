using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnemyManager : MonoBehaviour
{
    public GameManager gameManager;

    

    public GameObject foothold;
    public Transform barrageParent;

    private GameObject[] temp = new GameObject[20];

    public GameObject mainBoss;
    private Animator BossAni;
    private float pattentime;


    [SerializeField]
    private GameObject[] barragebox = new GameObject[5];

    private IEnumerator BossPattenCor = null;
    private bool stage2 = true;
    // Start is called before the first frame update
    void Start()
    {

       
    //    StartCoroutine(pattenCor());
        
    }

    //지금 scene 공유를 위해 코드 변경하는중
    public void CreateBoss(int index)
    {
        BossPattenCor = null;
        stage2 = true;

        gameManager.win = false;

        barrageParent = GameObject.Find("BarrageParent").transform;
        mainBoss = Instantiate(gameManager.bossPripab, new Vector3(8, -2, 0), Quaternion.identity);
        mainBoss.GetComponent<boss>().enemyManager = this;
        BossAni = mainBoss.GetComponent<Animator>();
    }



    // Update is called once per frame
    void Update()
    {
        Patten();
    }

    public void Bossaniplay(int index) {
        switch (index)
        {
            //행동할 애니메이션들을 관리

        }
    
    }
    

    public void Patten()
    {
        

        if (gameManager.battle)
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
            
            for (int i = 0; i < 4; i++) //직선 패턴 
            {
                temp[i] = Instantiate(barragebox[0], mainBoss.transform.position + new Vector3(0,-1.3f,0), Quaternion.identity, barrageParent);
                temp[i].GetComponent<Enemy>().MoveEnemy(mainBoss.transform.position + new Vector3(-20, -1.3f, 0), 100f);
                yield return new WaitForSecondsRealtime(1f);//1초 뒤에 다음 패턴

            }
            temp[4] = Instantiate(barragebox[1], mainBoss.transform.position + new Vector3(0, -1.3f, 0), Quaternion.identity, barrageParent);
            temp[4].GetComponent<Enemy>().MoveEnemy(mainBoss.transform.position + new Vector3(-20, -1.3f, 0), 100f);
         


            yield return new WaitForSecondsRealtime(3f);//3초 뒤에 다음 패턴
           
            for(int i = 0; i < 3; i++) // 엇갈리는 패턴 
            { 
            temp[0] = Instantiate(barragebox[i%2], new Vector3(7,0,0), Quaternion.identity, barrageParent);
            temp[1] = Instantiate(barragebox[(i+1)%2], new Vector3(7, 0, 0), Quaternion.identity, barrageParent);

            temp[0].GetComponent<Enemy>().Uturn(new Vector3(-20, 3, 0), 250f, 3f);
            temp[1].GetComponent<Enemy>().Uturn(new Vector3(-20, -3, 0), 250f, -3f);

                yield return new WaitForSecondsRealtime(1f);//1초 뒤에 다음 패턴
            }

            yield return new WaitForSecondsRealtime(2f);//2초 뒤에 다음 패턴
         
            for (int i = 0; i < 3; i++)  // 직선 나오는 패턴 + 통통 튀는 탄환 패턴 
            {
                temp[0] = Instantiate(barragebox[i % 2], new Vector3(7,-3, 0), Quaternion.identity, barrageParent);
                temp[1] = Instantiate(barragebox[(i + 1) % 2], new Vector3(7, -3, 0), Quaternion.identity, barrageParent);

                temp[0].GetComponent<Enemy>().Uturn(new Vector3(-20, 3, 0), 250f, 3f);
                temp[1].GetComponent<Enemy>().Uturn(new Vector3(-20, -3, 0), 250f, -3f);

               
            }  
   
            yield return new WaitForSecondsRealtime(1f); //1초 뒤에 다음 패턴

        } while (true);

    }
     
    // 2페이지는 2페이지 답게 난이도를 조금 올렸습니다.
   
    //2페이즈 코루틴
    IEnumerator PattenStage2Cor()
    {
        do
        {
            GameObject[] temp = new GameObject[5];
            for(int i = 0; i < 3; i++) // 유도 탄환 패턴 
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
            for (int i = 0; i < 3; i++) // 2개 겹쳐서 나오는 패턴 점프해서 피하는 패턴 
            {
                temp[0] = Instantiate(barragebox[i % 2], new Vector3(10, -3, 0), Quaternion.identity, barrageParent);
                temp[1] = Instantiate(barragebox[(i + 1) % 2], new Vector3(20, -3, 0), Quaternion.identity, barrageParent);

                temp[0].GetComponent<Enemy>().Uturn(new Vector3(-25, -2, 0), 250f, 3f);
                temp[1].GetComponent<Enemy>().Uturn(new Vector3(-20, -3, 0), 250f, -3f);


                yield return new WaitForSecondsRealtime(1f);
            }
            yield return new WaitForSecondsRealtime(2f);//2초 뒤에 다음 패턴
            for (int i = 0; i < 3; i++)  // 직선 나오는 패턴 + 통통 튀는 탄환 패턴 
            {
                temp[0] = Instantiate(barragebox[i % 2], new Vector3(7, -3, 0), Quaternion.identity, barrageParent);
                temp[1] = Instantiate(barragebox[(i + 1) % 2], new Vector3(7, -3, 0), Quaternion.identity, barrageParent);

                temp[0].GetComponent<Enemy>().Uturn(new Vector3(-20, 3, 0), 250f, 3f);
                temp[1].GetComponent<Enemy>().Uturn(new Vector3(-20, -3, 0), 250f, -3f);


            }


            yield return new WaitForSecondsRealtime(5f);
        } while (true);
    }



    public void BossDie()
    {
        var bullet = GameObject.FindGameObjectsWithTag("barrage");
        for(int i = 0; i < bullet.Length; i++)
        {
            Destroy(bullet[i]);
        }
        bullet = GameObject.FindGameObjectsWithTag("parringbarrage");
        for (int i = 0; i < bullet.Length; i++)
        {
            Destroy(bullet[i]);
        }


        StopCoroutine(BossPattenCor);//보스가 죽을 경우 코루틴 종료
        gameManager.GameEndLoadScene();
    }

   

   


}
