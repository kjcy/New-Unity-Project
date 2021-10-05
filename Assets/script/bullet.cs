using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public int damge;
    private float speed;
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
            collision.GetComponent<boss>().Hpdonw(damge, collision.gameObject);
        }
    }
}
