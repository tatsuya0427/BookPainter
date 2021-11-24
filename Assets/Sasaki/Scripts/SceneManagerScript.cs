using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Image panelImage;
    private float alpha = 0f;
    private float speed = 0.01f;

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
        panel.SetActive(true);
        StartCoroutine(SceneMove());
    }

    public void toTitleScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

    IEnumerator SceneMove()
    {
        Debug.Log("ゲームシーンへ移動します");
        while (alpha < 1f)
        {
            alpha += speed;
            panelImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GameUIScene");
    }
}
