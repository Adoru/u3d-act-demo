using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    public void OnStartBtn()
    {
        //Application.LoadLevel(1); //进入下一个场景
        SceneManager.LoadScene("Scene2");  //进入下一个场景
        print("next");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
