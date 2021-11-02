﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    private GameObject[] barragebox;

    private IEnumerator BossPattenCor = null;
    private bool stage2 = true;
    // Start is called before the first frame update
    void Start()
    {

       
    //    StartCoroutine(pattenCor());
        
    }

    
    public void CreateBoss()
    {
        BossPattenCor = null;
        stage2 = true;

        gameManager.win = false;

        barrageParent = GameObject.Find("BarrageParent").transform;
        if (!mainBoss) { 
        if (SceneManager.GetActiveScene().name.Equals("pro")) { //현재 씬에 따라 보스의 id를 정해주고 이를 통해 보스의 패턴 코루틴을 실행시킨다.
            mainBoss = Instantiate(gameManager.bossPripab[0], new Vector3(8, -2, 0), Quaternion.identity);//신에 따라 pripab을 다르게 적용해 씬에 맞는 보스를 소환한다.
            mainBoss.GetComponent<boss>().bossId = 0;
        }

        if (SceneManager.GetActiveScene().name.Equals("pro 1"))
        { //현재 씬에 따라 보스의 id를 정해주고 이를 통해 보스의 패턴 코루틴을 실행시킨다.
            mainBoss = Instantiate(gameManager.bossPripab[1], new Vector3(8, -2, 0), Quaternion.identity);//신에 따라 pripab을 다르게 적용해 씬에 맞는 보스를 소환한다.
            mainBoss.GetComponent<boss>().bossId = 1;
        }
            mainBoss.GetComponent<boss>().enemyManager = this;
        }

        BossAni = mainBoss.GetComponent<Animator>();
        barragebox = mainBoss.GetComponent<boss>().bulletBox;
    }



    // Update is called once per frame
    void Update()
    {
        if (gameManager.battle)
        {
           Patten();
        }
    }

    public void Bossaniplay(int index) {
        switch (index)
        {
            //행동할 애니메이션들을 관리

        }
    
    }
    

    public void Patten()
    {

        if (mainBoss.GetComponent<boss>().bossId == 0)
        { //보스id 가 0일때 튜토리얼 보스가 나온다.
            if (BossPattenCor == null)//막 처음 시작할때 패턴이 없다면 1페이지 코루틴을 시작한다.
            {
                BossPattenCor = PattenStage1Cor();
                StartCoroutine(BossPattenCor);
            }

            if (stage2 && mainBoss.GetComponent<boss>().hp < 250)
            {
                Clearbullet();
                stage2 = false;
                StopCoroutine(BossPattenCor);
                StartCoroutine(page2Start());
            }
        }else if(mainBoss.GetComponent<boss>().bossId == 1)
        {
          
        }
    }

    IEnumerator page2Start()
    {
        
        gameManager.battle = false;
        GameObject changeBackgound = GameObject.Find("ChangeBackground");
        for(int i = 0; i <= 30; i++)
        {
            changeBackgound.GetComponent<Image>().color = new Color(255, 255, 255, (i/30f));
            yield return new WaitForSecondsRealtime(0.05f);
        }

        GameObject.Find("Background").GetComponent<SpriteRenderer>().sprite = gameManager.page2Background;
        for (int i = 0; i <= 30; i++)
        {
            changeBackgound.GetComponent<Image>().color = new Color(255, 255, 255, 1-(i / 30f));
            yield return new WaitForSecondsRealtime(0.05f);
        }

        yield return new WaitForSecondsRealtime(0.5f);
        gameManager.battle = true;
        BossPattenCor = null;
        yield return null;
    }



    //1번째 보스 패턴 코루틴
    IEnumerator PattenStage1Cor()
    {
        do
        {
            if (!gameManager.battle) continue;//전투 상태가 아닐때는 패턴을 멈춘다.(ex 일시정지, 2페이즈 진입상태등)

            if (mainBoss.GetComponent<Enemy>().hp > 250) {//1페이즈 
                for (int i = 0; i < 4; i++) //직선 패턴 
                {
                    temp[i] = Instantiate(barragebox[0], mainBoss.transform.position + new Vector3(0,-1.3f,0), Quaternion.identity, barrageParent);
                    temp[i].GetComponent<Enemy>().MoveEnemy(mainBoss.transform.position + new Vector3(-20, -1.3f, 0), 100f);
                    yield return new WaitForSecondsRealtime(1f);//1초 뒤에 다음 패턴

                }
                temp[4] = Instantiate(barragebox[1], mainBoss.transform.position + new Vector3(0, -1.3f, 0), Quaternion.identity, barrageParent);
                temp[4].GetComponent<Enemy>().MoveEnemy(mainBoss.transform.position + new Vector3(-20, -1.3f, 0), 100f);
         
                yield return new WaitForSecondsRealtime(3f);//3초 뒤에 다음 패턴

                temp[3] = Instantiate(foothold, new Vector3(3, 5, 0), Quaternion.identity);//발판 생성후 이동
                temp[4] = Instantiate(foothold, new Vector3(-5, 5, 0), Quaternion.identity);//발판 생성후 이동
                temp[3].GetComponent<Enemy>().MoveEnemy(new Vector3(3, -1.5f, 0), 70f);
                temp[4].GetComponent<Enemy>().MoveEnemy(new Vector3(-5, -1.5f, 0), 70f);
                yield return new WaitForSecondsRealtime(0.7f);//이동에 여유를 주는 시간
                for(int i = 0; i < 3; i++) // 엇갈리는 패턴 
                { 
                temp[0] = Instantiate(barragebox[i%2], new Vector3(7,0,0), Quaternion.identity, barrageParent);
                temp[1] = Instantiate(barragebox[(i+1)%2], new Vector3(7, 0, 0), Quaternion.identity, barrageParent);

                temp[0].GetComponent<Enemy>().Uturn(new Vector3(-10, 3, 0), 200f, 1.6f);
                temp[1].GetComponent<Enemy>().Uturn(new Vector3(-10, -3, 0), 200f, 1.6f);

                    yield return new WaitForSecondsRealtime(1f);//1초 뒤에 다음 패턴
                }
                yield return new WaitForSecondsRealtime(2f);
                for (int i = 3; i <= 4; i++) { 
                temp[i].GetComponent<Enemy>().MoveEnemy(new Vector3(0, -6, 0), 50f);//발판의 이용이 끝남으로 아래로
                }
                yield return new WaitForSecondsRealtime(2f);//2초 뒤에 다음 패턴
                Destroy(temp[3]);//발판 삭제
                Destroy(temp[4]);//발판 삭제
                for (int i = 0; i < 3; i++)  // 직선 나오는 패턴 + 통통 튀는 탄환 패턴 
                {
                    temp[0] = Instantiate(barragebox[0], new Vector3(7,-1.7f, 0), Quaternion.identity, barrageParent);
                    temp[1] = Instantiate(barragebox[1], new Vector3(7, -2, 0), Quaternion.identity, barrageParent);

                    temp[0].GetComponent<Enemy>().Uturn(new Vector3(-20, 0, 0), 250f, 3);
                    temp[1].GetComponent<Enemy>().MoveEnemy(new Vector3(-20, -2, 0), 250);

                    yield return new WaitForSecondsRealtime(1.45f);
                }  
                yield return new WaitForSecondsRealtime(1f); //1초 뒤에 다음 패턴
            }
            else//2페이즈
            {
                

                for(int i = 0; i < 3; i++) // 유도 탄환 패턴 
                {
                    temp[i] = Instantiate(barragebox[0], new Vector3(-5 + i * 3, 5, 0), Quaternion.identity, barrageParent);
                    temp[i].GetComponent<Enemy>().MoveEnemy(temp[i].transform.position + new Vector3(0, -3, 0), 100f);
                }
                yield return new WaitForSecondsRealtime(2f);

                for (int i = 0; i < 3; i++)
                {
                    if (temp[i] == false) continue;
                    temp[i].GetComponent<Enemy>().reflex(gameManager.PlayerManager.Player.transform.position + new Vector3(0,0.5f,0), 30f);

                    yield return new WaitForSecondsRealtime(0.77f);
                }

                for(int i = 0; i < 3; i++)
                {
                    if (temp[i] == false) continue;
                    temp[i].GetComponent<Enemy>().MoveEnemy(new Vector3(-10, temp[i].transform.position.y, 0), 100f);
                }
                yield return new WaitForSecondsRealtime(0.5f);
                for (int i = 0; i < 3; i++) // 2개 겹쳐서 나오는 패턴 점프해서 피하는 패턴 
                {
                    temp[0] = Instantiate(barragebox[i % 2], new Vector3(10, -3, 0), Quaternion.identity, barrageParent);
                    temp[1] = Instantiate(barragebox[(i + 1) % 2], new Vector3(20, -3, 0), Quaternion.identity, barrageParent);

                    temp[0].GetComponent<Enemy>().Uturn(new Vector3(-25, -2, 0), 250f, 1f);
                    temp[1].GetComponent<Enemy>().Uturn(new Vector3(-20, -3, 0), 250f, 1f);


                    yield return new WaitForSecondsRealtime(1f);
                }
                yield return new WaitForSecondsRealtime(2f);//2초 뒤에 다음 패턴
                for (int i = 0; i < 3; i++)  // 직선 나오는 패턴 + 통통 튀는 탄환 패턴 
                {
                    temp[0] = Instantiate(barragebox[i % 2], new Vector3(7, -3, 0), Quaternion.identity, barrageParent);
                    temp[1] = Instantiate(barragebox[(i + 1) % 2], new Vector3(7, -3, 0), Quaternion.identity, barrageParent);

                    temp[0].GetComponent<Enemy>().Uturn(new Vector3(-20, 3, 0), 250f, 1f);
                    temp[1].GetComponent<Enemy>().MoveEnemy(new Vector3(-20, -3, 0), 250);
                    yield return new WaitForSecondsRealtime(1.45f);
                }


                yield return new WaitForSecondsRealtime(2f);
            }
        } while (true);
 // 2번째 보스 패턴 코루틴 (수정중)
 IEnumerator PattenStage2Cor()
    {
        do
        {
            if (!gameManager.battle) continue;//전투 상태가 아닐때는 패턴을 멈춘다.(ex 일시정지, 2페이즈 진입상태등)

            if (mainBoss.GetComponent<Enemy>().hp > 250)
            {//1페이즈 
             for (int i = 0; i < 2; i++) // 하늘에서 다수  탄환 내려오는  패턴  (개선하는 중 2페이지 용)
                {
                    temp[i] = Instantiate(barragebox[0], new Vector3(-5 + i * 5, 5, 0), Quaternion.identity, barrageParent);
                    temp[i].GetComponent<Enemy>().MoveEnemy(mainBoss.transform.position + new Vector3(-5, -5, 0), 100f);
                    temp[i] = Instantiate(barragebox[0], new Vector3(-5 + i * 5, 5, 0), Quaternion.identity, barrageParent);
                    temp[i].GetComponent<Enemy>().MoveEnemy(mainBoss.transform.position + new Vector3(-10, -5, 0), 100f);
                    temp[i] = Instantiate(barragebox[0], new Vector3(-5 + i * 5, 5, 0), Quaternion.identity, barrageParent);
                    temp[i].GetComponent<Enemy>().MoveEnemy(mainBoss.transform.position + new Vector3(-14, -5, 0), 100f);
                    temp[i] = Instantiate(barragebox[1], new Vector3(-5 + i * 5, 5, 0), Quaternion.identity, barrageParent);
                    temp[i].GetComponent<Enemy>().MoveEnemy(mainBoss.transform.position + new Vector3(-17, -5, 0), 100f);
                }
                yield return new WaitForSecondsRealtime(3f);//3초 뒤에 다음 패턴

                for (int i = 0; i < 3; i++) // 뒤에 있던 탄환이 빠르게 앞으로 오면서 공격하는  패턴  (아직 수정중 보스 2페이지 들어갈 예정 여기에서 너프 된 상태로 1페이지)
                {
                    temp[0] = Instantiate(barragebox[i % 2], new Vector3(7, 0, 0), Quaternion.identity, barrageParent);
                    temp[1] = Instantiate(barragebox[(i + 1) % 2], new Vector3(7, 0, 0), Quaternion.identity, barrageParent);

                    temp[0].GetComponent<Enemy>().Uturn(new Vector3(-10, 3, 0), 70f, 1.6f);
                    temp[1].GetComponent<Enemy>().Uturn(new Vector3(-10, -3, 0), 70f, 1.6f);
                    
                    temp[0] = Instantiate(barragebox[i % 2], new Vector3(9, 0, 0), Quaternion.identity, barrageParent);
                    temp[1] = Instantiate(barragebox[(i + 1) % 2], new Vector3(9, 0, 0), Quaternion.identity, barrageParent);

                    temp[0].GetComponent<Enemy>().Uturn(new Vector3(-12, 4, 0), 70f, 1.6f);
                    temp[1].GetComponent<Enemy>().Uturn(new Vector3(-12, -4, 0), 70f, 1.6f);
                    
                    temp[0] = Instantiate(barragebox[i % 2], new Vector3(12, 0, 0), Quaternion.identity, barrageParent);
                    temp[1] = Instantiate(barragebox[(i + 1) % 2], new Vector3(12, 0, 0), Quaternion.identity, barrageParent);

                    temp[0].GetComponent<Enemy>().Uturn(new Vector3(-15, 6, 0), 70f, 1.6f);
                    temp[1].GetComponent<Enemy>().Uturn(new Vector3(-15, -6, 0), 70f, 1.6f);





                    yield return new WaitForSecondsRealtime(2f);//1초 뒤에 다음 패턴
                }
                for (int i = 0; i < 4; i++) //빠른 직선 패턴 

                {
                    temp[i] = Instantiate(barragebox[0], mainBoss.transform.position + new Vector3(0, -1.3f, 0), Quaternion.identity, barrageParent);
                    temp[i].GetComponent<Enemy>().MoveEnemy(mainBoss.transform.position + new Vector3(-20, -1.3f, 0), 50f);
                    yield return new WaitForSecondsRealtime(1f);//1초 뒤에 다음 패턴

                }
                temp[4] = Instantiate(barragebox[1], mainBoss.transform.position + new Vector3(0, -1.3f, 0), Quaternion.identity, barrageParent);
                temp[4].GetComponent<Enemy>().MoveEnemy(mainBoss.transform.position + new Vector3(-20, -1.3f, 0), 50f);

                yield return new WaitForSecondsRealtime(3f);//3초 뒤에 다음 패턴

               
                for (int i = 0; i < 3; i++) // 엇갈리는 패턴 
                {
                    temp[0] = Instantiate(barragebox[i % 2], new Vector3(7, 0, 0), Quaternion.identity, barrageParent);
                    temp[1] = Instantiate(barragebox[(i + 1) % 2], new Vector3(7, 0, 0), Quaternion.identity, barrageParent);

                    temp[0].GetComponent<Enemy>().Uturn(new Vector3(-10, 3, 0), 200f, 1.6f);
                    temp[1].GetComponent<Enemy>().Uturn(new Vector3(-10, -3, 0), 200f, 1.6f);

                    yield return new WaitForSecondsRealtime(1f);//1초 뒤에 다음 패턴
                }
       
                for (int i = 0; i < 3; i++)  // 직선 나오는 패턴 + 통통 튀는 탄환 패턴 
                {
                    temp[0] = Instantiate(barragebox[0], new Vector3(7, -1.7f, 0), Quaternion.identity, barrageParent);
                    temp[1] = Instantiate(barragebox[1], new Vector3(7, -2, 0), Quaternion.identity, barrageParent);

                    temp[0].GetComponent<Enemy>().Uturn(new Vector3(-20, 0, 0), 250f, 3);
                    temp[1].GetComponent<Enemy>().MoveEnemy(new Vector3(-20, -2, 0), 250);

                    yield return new WaitForSecondsRealtime(1f);//1초 뒤에 다음 패턴
                }
                yield return new WaitForSecondsRealtime(3f);//1초 뒤에 다음 패턴
            }
            else//2페이즈
            {


                for (int i = 0; i < 3; i++) // 유도 탄환 패턴 
                {
                    temp[i] = Instantiate(barragebox[0], new Vector3(-5 + i * 5, 5, 0), Quaternion.identity, barrageParent);
                    temp[i].GetComponent<Enemy>().MoveEnemy(temp[i].transform.position + new Vector3(0, -3, 0), 100f);
                }
                yield return new WaitForSecondsRealtime(2f);

                for (int i = 0; i < 3; i++)
                {
                    if (temp[i] == false) continue;
                    temp[i].GetComponent<Enemy>().reflex(gameManager.PlayerManager.Player.transform.position + new Vector3(0, 0.5f, 0), 30f);

                    yield return new WaitForSecondsRealtime(0.77f);
                }

                for (int i = 0; i < 3; i++)
                {
                    if (temp[i] == false) continue;
                    temp[i].GetComponent<Enemy>().MoveEnemy(new Vector3(-10, temp[i].transform.position.y, 0), 100f);
                }
                yield return new WaitForSecondsRealtime(0.5f);
                for (int i = 0; i < 3; i++) // 2개 겹쳐서 나오는 패턴 점프해서 피하는 패턴 
                {
                    temp[0] = Instantiate(barragebox[i % 2], new Vector3(10, -3, 0), Quaternion.identity, barrageParent);
                    temp[1] = Instantiate(barragebox[(i + 1) % 2], new Vector3(20, -3, 0), Quaternion.identity, barrageParent);

                    temp[0].GetComponent<Enemy>().Uturn(new Vector3(-25, -2, 0), 250f, 1f);
                    temp[1].GetComponent<Enemy>().Uturn(new Vector3(-20, -3, 0), 250f, 1f);


                    yield return new WaitForSecondsRealtime(1f);
                }
                yield return new WaitForSecondsRealtime(2f);//2초 뒤에 다음 패턴
                for (int i = 0; i < 3; i++)  // 직선 나오는 패턴 + 통통 튀는 탄환 패턴 
                {
                    temp[0] = Instantiate(barragebox[i % 2], new Vector3(7, -3, 0), Quaternion.identity, barrageParent);
                    temp[1] = Instantiate(barragebox[(i + 1) % 2], new Vector3(7, -3, 0), Quaternion.identity, barrageParent);

                    temp[0].GetComponent<Enemy>().Uturn(new Vector3(-20, 3, 0), 250f, 1f);
                    temp[1].GetComponent<Enemy>().MoveEnemy(new Vector3(-20, -3, 0), 250);
                    yield return new WaitForSecondsRealtime(1.45f);
                }


                yield return new WaitForSecondsRealtime(2f);
            }
        } while (true);

    }

    public void Clearbullet()
    {
        var bullet = GameObject.FindGameObjectsWithTag("barrage");
        for (int i = 0; i < bullet.Length; i++)
        {
            Destroy(bullet[i]);
        }
        bullet = GameObject.FindGameObjectsWithTag("parringbarrage");
        for (int i = 0; i < bullet.Length; i++)
        {
            Destroy(bullet[i]);
        }

    }

    public void BossDie()
    {

        Clearbullet();
        StopCoroutine(BossPattenCor);//보스가 죽을 경우 코루틴 종료
        Destroy(mainBoss);//신을 이동하더라고 보스가 안사라지는 것을 방지
    }

   

   


}
