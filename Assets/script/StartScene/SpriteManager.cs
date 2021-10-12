using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour
{

    public static bool spriteCheck = false;
    // Start is called before the first frame update
    void Start()
    {
        GameObject spImage = this.gameObject;
        Color color = spImage.GetComponent<Image>().color;
        color.a = 0;
        spImage.GetComponent<Image>().color = color;
    }

    private void SpritePlus()
    {
        GameObject spImage = this.gameObject;
        Color color = spImage.GetComponent<Image>().color;
        color.a += Time.deltaTime * 0.5f;
        spImage.GetComponent<Image>().color = color;
    }
    // Update is called once per frame
    void Update()
    {
        if (SoundManager.crowCheck == true)
        {
            if (this.gameObject.name.Contains("Logo"))
            {
                SpritePlus();
            }

        }

        if(SoundManager.bgmusicCheck==true)
        {
            if(this.gameObject.name.Contains("Button"))
            {
                SpritePlus();
                spriteCheck = true;
            }
        }



    }
}
