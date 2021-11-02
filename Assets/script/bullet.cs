using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public int damge;
    private float speed;
    public PlayerManager manager;
    private bool go;
    public Sprite[] bulletSprite = new Sprite[3];
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        go = true;
        count = 0;
        speed = 30;
        Destroy(this.gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if (go) { 
        this.gameObject.GetComponent<SpriteRenderer>().sprite = bulletSprite[count++ % 2];
        
        this.transform.Translate(Vector3.up*speed*Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("boss"))
        {
            Debug.Log("총알 충돌");
            //총알의 데미지로 어빌리티를 충전한다면 어빌리티를 사용한 것으로 다시 일정양을 충전하기 때문에 생각을 해봐야 한다.
            //manager.GetabilityBar();
            go = false;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = bulletSprite[2];
            collision.GetComponent<boss>().Hpdonw(damge, collision.gameObject);
            Destroy(this.gameObject,0.06f);
        }
    }
}
