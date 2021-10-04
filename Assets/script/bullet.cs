using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 15;
        Destroy(this.gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.up*speed*Time.deltaTime);
    }
}
