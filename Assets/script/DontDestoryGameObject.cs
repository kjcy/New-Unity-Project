using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestoryGameObject : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        var obj = GameObject.FindGameObjectsWithTag(this.gameObject.transform.tag); 
        if (obj.Length == 1) { 
            DontDestroyOnLoad(gameObject); 
        } 
        else { 
            Destroy(gameObject);
        }

   
    }
}
