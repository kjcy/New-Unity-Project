using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    static public GameManager gameManager;


    //각종 이벤트가 발생할때 사용해야할 함수를 호출하는 이벤트 관리, button 에서 사용하는 것과 같은 방식이다.
    public UnityEvent GameOver;
    public UnityEvent LoadBattleSceneEvent;


    [SerializeField]
    private EnemyManager enemyManager;
    public EnemyManager EnemyManager
    {
        get { return enemyManager; }
    }
    [SerializeField]
    public GameObject[] bossPripab;

    public GameObject startTime;

    [SerializeField]
    private PlayerManager playerManager;

    public Sprite reddysprite;
    public Sprite startsprite;

    public Sprite page2Background;

    public PlayerManager PlayerManager
    {
        get { return playerManager; }
    }
    public bool win = false;
    
    //플레이어의 조종 여부
    public bool play;

    public bool[] portal = new bool[2];

    //전투의 작동 여부
    public bool battle;
    // Start is called before the first frame update
    void Start()
    {
        portal[0] = true;
        play = true;
        gameManager = this;
        playerManager.gameManager = this;
        enemyManager.gameManager = this;
        //StartCoroutine(StartAni());
    }

    private void Update()
    {
        ActivePortal();
    }

    public void ActivePortal()
    {
        if(SceneManager.GetActiveScene().name == "SelectScene")
            for(int i = 0; i < portal.Length; i++)
            {
                GameObject.Find("portal").transform.GetChild(i).gameObject.SetActive(portal[i]);
            }
    }


    //선택 신으로 이동할수 있도록 하는 함수
    public void GameEndLoadScene()
    {

        if (win)
        {
            Debug.Log("승리");
            OnClearPortal();
            //승리했다는 코드
        }
        else
        {
            //패배했다는 코드
        }
        SceneManager.LoadScene("SelectScene");
        //StartCoroutine(LoadSelectScene());
    }
    public void OnClearPortal()
    {
        string name = SceneManager.GetActiveScene().name;

        if (name.Equals("pro"))
        {
            portal[1] = true;
        }

    }
    IEnumerator LoadSelectScene()
    {
        if (win)
        {
            Debug.Log("승리");
            //승리했다는 코드
        }
        else
        {
            //패배했다는 코드
        }


        //yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadScene("SelectScene");
        yield return 0;
    }

    //전투 신을 시작할때 잠시 대기하는 함수
    public void StartSceneAni()
    {

        startTime = GameObject.Find("startTime");
        StartCoroutine(StartAni());
    }

    IEnumerator StartAni()
    {
        battle = false;
        play = false;
        startTime.GetComponent<Image>().sprite = reddysprite;
        yield return new WaitForSecondsRealtime(3f);
        startTime.GetComponent<Image>().sprite = startsprite;
        yield return new WaitForSecondsRealtime(0.3f);
        play = true;
        battle = true;
        startTime.SetActive(false);
    }

    public void EnemyHpDown(Enemy enemy ,int damge)
    {
        enemy.Hpdonw(damge,null);
    }


    public void PlayerParringCheck()
    {
        if (playerManager.ParringPalyer)//플레이어가 패링 상태라면
        {
            playerManager.ParringSuccess();
        }
        else
        {
            PlayerHpDown();
        }
    }

    public void PlayerHpDown()
    {
        if (playerManager.hpDown(1))//플레이어의 체력이 0이하가 된다면 참을 반환한다.
        {
           GameObject temp =  GameObject.Find("Canvas").transform.GetChild(4).gameObject;//주인공이 죽었을때 select 씬으로 돌아가는 UI
            if (!temp)
            {
                Debug.Log("못찾음");
            }
            else
            {
                temp.SetActive(true);//이를 활성화
            }

            GameOver.Invoke();//게임오버라는 이벤트를 실행
            
        }
    }

    //클리어 했을때 사용할 함수
    public void Gameclear()
    {

    }

    //게임이 끝나서 플레이어의 음직임과 패턴의 실행을 정지한다.
    public void GameEnd()
    {
        play = false;
        battle = false;
    }


}
