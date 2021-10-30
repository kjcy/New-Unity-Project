using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToPalyer : MonoBehaviour
{
    public PlayerManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = PlayerManager.playerManager;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, manager.Player.transform.position, 0.001f);
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("어빌리티볼 충돌");
            Destroy(this.gameObject);
            //여기에 어빌리티볼이랑 플레이어가 만날때 임팩트, 효과등을 작성
        }
    }


}
