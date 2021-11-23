using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toGameScene()
    {
        SceneManager.LoadScene("GameUIScene");
    }

    public void toTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
