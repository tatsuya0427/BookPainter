using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManagerScript : MonoBehaviour
{
    private Image startButtonImage;
    private float alpha = 0f;
    private float speed = 1.1f;

    // Start is called before the first frame update
    void Start()
    {
        startButtonImage = GameObject.Find("StartButton").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        alpha += speed * Time.deltaTime;
        startButtonImage.color = new Color(1f, 1f, 1f, 0.6f*Mathf.Abs(Mathf.Sin(alpha)));
    }
}
