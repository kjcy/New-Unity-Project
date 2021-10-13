using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    static public GameManager gameManager;

    public UnityEvent GameOver;

    [SerializeField]
    private EnemyManager enemyManager;
    public EnemyManager EnemyManager
    {
        get { return enemyManager; }
    }
    [SerializeField]
    public GameObject bossPripab;

    public Text startText;

    [SerializeField]
    private PlayerManager playerManager;

    public PlayerManager PlayerManager
    {
        get { return playerManager; }
    }

    public bool play = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = this;
        playerManager.gameManager = this;
        enemyManager.gameManager = this;
        StartCoroutine(StartAni());
    }

    IEnumerator StartAni()
    {
        for(int i = 0; i < 3; i++) {
            startText.text =  string.Format("{0}",3-i);

             yield return new WaitForSecondsRealtime(1f);
        }
        startText.gameObject.SetActive(false);
        
        play = true;
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
            GameOver.Invoke();//게임오버라는 이벤트를 실행
            
        }
    }

    public void GameEnd()
    {
        play = false;

    }


}
