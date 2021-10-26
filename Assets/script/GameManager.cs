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

    public GameObject startTime;

    [SerializeField]
    private PlayerManager playerManager;

    public Sprite reddysprite;
    public Sprite startsprite;

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
        startTime.GetComponent<Image>().sprite = reddysprite;
        yield return new WaitForSecondsRealtime(3f);
        startTime.GetComponent<Image>().sprite = startsprite;
        yield return new WaitForSecondsRealtime(0.3f);
        play = true;
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
            GameOver.Invoke();//게임오버라는 이벤트를 실행
            
        }
    }

    public void Gameclear()
    {

    }

    public void GameEnd()
    {
        play = false;

    }


}
