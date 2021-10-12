using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioClip[] mainSound;

    AudioSource soundSource;

    public static bool crowCheck = false;
    public static bool bgmusicCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        soundSource = GetComponent<AudioSource>();
        StartCoroutine("PlayList");
    }


    IEnumerator PlayList()
    {
        soundSource.clip = mainSound[0];
        soundSource.Play();
        crowCheck = true;
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            if (!soundSource.isPlaying)
            {
                soundSource.clip = mainSound[1];
                soundSource.Play();
                soundSource.loop = true;
                bgmusicCheck = true;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
