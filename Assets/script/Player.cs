using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private PlayerManager manager;

    public void Update()
    {
        //github 실험중2
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{

    //    Debug.Log("일단 충돌중");
    //    if (collision.gameObject.CompareTag("barrage"))
    //    {
    //        Debug.Log("barrage");
    //        manager.gameManager.PlayerHpDown();
    //    }

    //    if (collision.gameObject.CompareTag("parringbarrage"))
    //    {
    //        Debug.Log("parringbarrage");
    //        manager.gameManager.PlayerParringCheck();
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("일단 충돌중 trigger");
        if (collision.gameObject.CompareTag("barrage"))
        {
            Debug.Log("barrage");
            manager.gameManager.PlayerHpDown();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("parringbarrage"))
        {
            Debug.Log("parringbarrage");
            manager.gameManager.PlayerParringCheck();
            Destroy(collision.gameObject);
        }
    }

}
