using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public int score;
    private float timer = 0.999f;
    private int timerInt = 0;
    private float countdownTimer = 3.999f;
    private int countdownTimerInt = 3;
    private bool beforeGame = true;
    private bool gameover = false;

    private float alpha = 0f;
    private bool resultShown = false;

    private GameObject mainCamera;
    private Camera camOfmainCamera;
    [SerializeField] private GameObject gameObjects;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timerText;
    [SerializeField] private Text countdownText;
    [SerializeField] private GameObject countdownTextField;
    [SerializeField] private Text ResultScoreText;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject resultObjects;
    [SerializeField] private GameObject[] candles;
    private CanvasGroup resultObjectsCanvas;
    private GameObject fallingBooks;
    private SkinnedMeshRenderer resultBookShape;

    public Texture2D brushCursor;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        camOfmainCamera = mainCamera.GetComponent<Camera>();
        fallingBooks = GameObject.Find("FallingBooks");
        resultBookShape = GameObject.Find("ResultBook").GetComponent<SkinnedMeshRenderer>();
        resultObjectsCanvas = resultObjects.GetComponent<CanvasGroup>();
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

                if(timer < 20 && candles[0].activeSelf)
                {
                    candles[0].SetActive(false);
                }
                if (timer < 10 && candles[1].activeSelf)
                {
                    candles[1].SetActive(false);
                }
            }
            else
            {
                gameover = true;
                if (candles[2].activeSelf)
                {
                    candles[2].SetActive(false);
                }
            }
        }

        if (gameover && !resultShown)
        {
            showResult();
        }

        if (resultObjects.activeSelf)
        {

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
        gameObjects.SetActive(false);
        //結果表示に必要なUIを表示
        resultObjects.SetActive(true);
        //スコアを表示
        ResultScoreText.text = score.ToString();
        //演出
        //カメラの移動と本の雨と文字表示
        StartCoroutine(CameraMove());

        resultShown = true;
    }

    IEnumerator CameraMove()
    {
        float camRotX = 20f;
        float camRotY = 0f;

        float camPosZ = 0.9f;
        float weight = 100f;

        //後ろを向く
        while (camRotY <= 180f)
        {
            mainCamera.transform.rotation = Quaternion.Euler(camRotX, camRotY, 0f);
            camRotY += 0.5f;
            yield return null;
        }
        //本が降ってくる
        for (int i = 0; i < fallingBooks.transform.childCount; i++)
        {
            fallingBooks.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
        //机に近づく
        while(camPosZ > -2.49f)
        {
            camRotX += 0.15f;
            camPosZ -= 0.01f;
            mainCamera.transform.rotation = Quaternion.Euler(camRotX, camRotY, 0f);
            mainCamera.transform.position = new Vector3(0f, 0f, camPosZ);
            yield return null;
        }
        //本が開く, カメラがズームする
        while(resultBookShape.GetBlendShapeWeight(0) > 0f)
        {
            weight--;
            resultBookShape.SetBlendShapeWeight(0, weight);
            camOfmainCamera.fieldOfView -= 0.4f;
            yield return null;
        }
        
        //文字が浮かび上がる
        while(alpha <= 1f)
        {
            alpha += 0.01f;
            resultObjectsCanvas.alpha = alpha;
            yield return null;
        }
    }
}
