using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ButtonSystem : MonoBehaviour
{
    [SerializeField]
    AudioClip[] buttonSound;

    int ButtonTime = 0;

    AudioSource bsSource;

    private bool buttonAble = false;
    // Start is called before the first frame update
    void Start()
    {
        bsSource = GetComponent<AudioSource>();
    }

    private void ButtonSound_Play()
    {
        bsSource.clip = buttonSound[0];
        bsSource.Play();
    }

    public void NextScene()
    {
        if (buttonAble == true)
        {
            ButtonSound_Play();
            SceneManager.LoadScene("SelectScene");
        }
    }

    public void QuitGame()
    {
        ButtonSound_Play();
        if (buttonAble == true)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        if (SpriteManager.spriteCheck == true)
        {
            buttonAble = true;
        }


    }
}
