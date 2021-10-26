using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeMove : MonoBehaviour
{
    public float speed;
    public Transform[] backgrounds;

    float leftPosX = 0f;
    float rightPosX = 0f;

    float backgroundWidth;

    // Start is called before the first frame update
    void Start()
    {
        backgroundWidth = backgrounds[1].position.x;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].position += new Vector3(-speed, 0, 0) * Time.deltaTime;

            if (backgrounds[i].position.x < -backgroundWidth)
            {
                Vector3 nextPos = backgrounds[i].position;
                nextPos = new Vector3(backgroundWidth, nextPos.y, nextPos.z);
                backgrounds[i].position = nextPos;
            }
        }

    }
}
