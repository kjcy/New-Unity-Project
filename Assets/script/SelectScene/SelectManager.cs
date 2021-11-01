using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += LoadedsceneEvent;//로드할때 함수가 발생하도록 추가
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("포탈과 충돌중!!!");
            if (Input.GetKeyDown(KeyCode.W))
            {
                SceneManager.LoadScene(this.gameObject.name);//포탈의 이름을 씬의 이름으로해서 이름에 맞는 신으로 이동할 수 있도록

             //   SceneManager.LoadScene("pro");
               
            }
        }
    }

  


    //다른씬을 로드할때 호출되는 이벤트
    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
    {
        GameManager.gameManager.play = true;//신을 이동할때 플레이어가 음직일 수 있도록 변경

        if(scene.name == "pro") {//만약 이동하는 신이 전투 신일 경우 보스를 소환하고 플레이어의 위치를 정해준다.
            GameManager.gameManager.LoadBattleSceneEvent.Invoke();
            PlayerManager.playerManager.Player.transform.position = new Vector3(-5, 0, 0);
            Debug.Log(scene.name + "으로 변경되였습니다.");
        }
        if (scene.name == "pro 1")
        {//만약 이동하는 신이 전투 신일 경우 보스를 소환하고 플레이어의 위치를 정해준다.
            GameManager.gameManager.LoadBattleSceneEvent.Invoke();
            PlayerManager.playerManager.Player.transform.position = new Vector3(-5, 0, 0);
            Debug.Log(scene.name + "으로 변경되였습니다.");
        }
    }
}
