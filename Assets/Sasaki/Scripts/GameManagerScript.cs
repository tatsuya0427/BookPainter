using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public int score;
    private float timer = 3.999f;
    private int timerInt = 3;
    private float countdownTimer = 3.999f;
    private int countdownTimerInt = 3;
    private bool beforeGame = true;
    private bool gameover = false;
    private float alpha = 0f;
    private bool resultShown = false;

    [SerializeField] private GameObject text_s;
    [SerializeField] private GameObject text_t;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timerText;
    [SerializeField] private Text countdownText;
    [SerializeField] private Text Result_Text_s;
    [SerializeField] private Text ResultScoreText;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject resultPanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(beforeGame)
        {
            //開始前カウントダウン処理
            countdownTimer -= Time.deltaTime;
            countdownTimerInt = (int)countdownTimer;
            countdownText.text = countdownTimerInt.ToString();
            if(countdownTimer <= 0f)
            {
                countdownText.text = "";
                beforeGame = false;
            }else if(countdownTimer <= 0.99f)
            {
                countdownText.text = "Start!";
                //幕開けみたいな演出
                panel.gameObject.transform.localPosition = Vector3.Lerp(panel.transform.localPosition, new Vector3(-700, 0, 0), Time.deltaTime*2.5f);
            }
        }
        else
        {
            //時間制限処理
            if(timer >= 0f)
            {
                timer -= Time.deltaTime;
                timerInt = (int)timer;
                timerText.text = timerInt.ToString();
            }
            else
            {
                gameover = true;
            }
        }

        if (gameover && !resultShown)
        {
            showResult();
        }
    }

    public void addScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }

    void showResult()
    {
        //演出
        //本を開くアニメーション入れてその上に表示してもいいかも
        StartCoroutine(PanelOpen());
        //不要なものを非表示
        text_s.SetActive(false);
        text_t.SetActive(false);
        //スコアを表示
        ResultScoreText.text = score.ToString();
        while (alpha <= 1)
        {
            Result_Text_s.color = new Color(0, 0, 0, alpha);
            ResultScoreText.color = new Color(0, 0, 0, alpha);
            alpha += 0.005f;
        }
        resultShown = true;
    }

    IEnumerator PanelOpen()
    {
        var size = 0f;
        var speed = 0.005f;

        while (size <= 1.0f)
        {
            countdownText.text = "Game End";
            resultPanel.transform.localScale = Vector3.Lerp(new Vector3(0, 1, 1), new Vector3(1, 1, 1), size);
            size += speed;
            yield return null;
        }
        countdownText.text = "";
    }
}
