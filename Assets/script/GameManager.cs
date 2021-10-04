using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private EnemyManager enemyManager;
    [SerializeField]
    private PlayerManager playerManager;

    public bool play = true;

    // Start is called before the first frame update
    void Start()
    {
        playerManager.gameManager = this;
        enemyManager.gameManager = this;
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
        if (playerManager.hpDown(1))
        {
            //게임 오버
        }
    }
}
