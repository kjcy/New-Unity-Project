using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public int damge;
    private float speed;
    public PlayerManager manager;

    // Start is called before the first frame update
    void Start()
    {
        speed = 30;
        Destroy(this.gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.up*speed*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("boss"))
        {
            Debug.Log("총알 충돌");
            //총알의 데미지로 어빌리티를 충전한다면 어빌리티를 사용한 것으로 다시 일정양을 충전하기 때문에 생각을 해봐야 한다.
            //manager.GetabilityBar();
            collision.GetComponent<boss>().Hpdonw(damge, collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
