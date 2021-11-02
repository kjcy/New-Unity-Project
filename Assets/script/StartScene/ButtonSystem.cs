using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ButtonSystem : MonoBehaviour
{
    [SerializeField]
    AudioClip[] buttonSound;

    float ButtonTime = 0;

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
            StartCoroutine(SceneLoad());
        }
    }

    IEnumerator SceneLoad()
    {
        ButtonSound_Play();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("SelectScene");
    }
    IEnumerator SceneClose()
    {
        ButtonSound_Play();
        yield return new WaitForSeconds(3f);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif

    }
    public void QuitGame()
    {
        if (buttonAble == true)
        {
            StartCoroutine(SceneClose());
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
