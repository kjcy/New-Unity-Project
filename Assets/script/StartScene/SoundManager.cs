using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public AudioClip StartBgm;

    AudioSource soundSource;

    public static bool bgmusicCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        this.soundSource = GetComponent<AudioSource>();
    }


    

    // Update is called once per frame
    void Update()
    {
        soundSource.clip = StartBgm;
        soundSource.Play();
        soundSource.loop = true;
        bgmusicCheck = true;
    }
}
