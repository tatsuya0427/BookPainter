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

    private float camRot = 0f;
    private float alpha = 0f;
    private bool resultShown = false;

    private GameObject mainCamera;
    [SerializeField] private GameObject paperImage;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timerText;
    [SerializeField] private Text countdownText;
    [SerializeField] private GameObject countdownTextField;
    [SerializeField] private Text Result_Text_s;
    [SerializeField] private Text ResultScoreText;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject resultPanel;
    private GameObject fallingBooks;

    public Texture2D brushCursor;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        fallingBooks = GameObject.Find("FallingBooks");
        //カーソルを絵筆に変更
        Cursor.SetCursor(brushCursor, new Vector2(100f, 100f), CursorMode.ForceSoftware);
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
                countdownTextField.SetActive(false);
                beforeGame = false;
            }else if(countdownTimer <= 0.99f)
            {
                countdownText.text = "Start!";
                //幕開けみたいな演出
                panel.gameObject.transform.localPosition = Vector3.Lerp(panel.transform.localPosition, new Vector3(-1070, 0, 0), Time.deltaTime*2.5f);
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
        //カーソルをデフォルトに戻す
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        //不要なものを非表示
        paperImage.SetActive(false);
        //演出
        //カメラが後ろを向く
        StartCoroutine(CameraRotate());
        //カメラの移動と時間調整を作成する予定
        //本が降ってくる
        for (int i = 0; i < fallingBooks.transform.childCount; i++){
            fallingBooks.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
        //本を開くアニメーション入れてその上にscore表示したい
        StartCoroutine(PanelOpen());
        //スコアを表示
        ResultScoreText.text = score.ToString();
        StartCoroutine(MojiAppear());
        if(camRot >= 180f && alpha >= 1f)
        {
            resultShown = true;
        }
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

    IEnumerator CameraRotate()
    {
        while (camRot <= 180f)
        {
            mainCamera.transform.rotation = Quaternion.Euler(20f, camRot, 0f);
            camRot += 0.05f;
            yield return null;
        }
    }

    IEnumerator MojiAppear()
    {
        while (alpha <= 1f)
        {
            Result_Text_s.color = new Color(0, 0, 0, alpha);
            ResultScoreText.color = new Color(0, 0, 0, alpha);
            alpha += 0.01f;
            yield return null;
        }
    }
}
