using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public int score;
    [SerializeField] private float timer = 30.999f;
    private int timerInt = 30;
    private float countdownTimer = 3.999f;
    private int countdownTimerInt = 3;
    private bool beforeGame = true;
    private bool gameover = false;
    private int moveCount = 0;
    [SerializeField] private int moveCountMax = 60;
    [SerializeField] private int resultBookOpenSpeed = 1;//スコアが表示される本の開く速度を格納する変数


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
    [SerializeField] private Text ResultGradeText;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject resultObjects;
    [SerializeField] private GameObject[] candles;
    private CanvasGroup resultObjectsCanvas;
    private GameObject fallingBooks;
    private SkinnedMeshRenderer resultBookShape;
    private AudioSource gameBGM;

    public Texture2D brushCursor;

    private static BookCreater _bookCreater = null;
    private bool firstCreate = true;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera");
        camOfmainCamera = mainCamera.GetComponent<Camera>();
        fallingBooks = GameObject.Find("FallingBooks");
        resultBookShape = GameObject.Find("ResultBook").GetComponent<SkinnedMeshRenderer>();
        resultObjectsCanvas = resultObjects.GetComponent<CanvasGroup>();
        gameBGM = this.GetComponent<AudioSource>();

        //カーソルを絵筆に変更
        Cursor.SetCursor(brushCursor, new Vector2(100f, 100f), CursorMode.ForceSoftware);
        if(_bookCreater == null){
            _bookCreater = GameObject.Find("BookCreater").GetComponent<BookCreater>();
        }

        timerInt = (int)timer;
        timerText.text = timerInt.ToString();
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
                gameBGM.Play();
                countdownText.text = "Start!";
                //幕開けみたいな演出
                panel.gameObject.transform.localPosition = Vector3.Lerp(panel.transform.localPosition, new Vector3(-1070, 0, 0), Time.deltaTime*2.5f);
            }
        }
        else
        {

            if(firstCreate){
                _bookCreater.CreateBook();
                firstCreate = false;
            }
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
            gameBGM.volume = 0.2f;
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

        //グレードを表示
        if(score > 1000)
        {
            ResultGradeText.text = "A";
        }else if (score > 600)
        {
            ResultGradeText.text = "B";
        }else if (score > 300)
        {
            ResultGradeText.text = "C";
        }else if(score <= 300)
        {
            ResultGradeText.text = "D";
        }

        //演出
        //カメラの移動と本の雨と文字表示
        StartCoroutine(CameraMove());

        resultShown = true;
    }

    IEnumerator CameraMove()
    {
        float camRotX = 52f;
        float camRotY = 0f;

        float camPosZ = 0.9f;
        float weight = 100f;

        //後ろを向く
        while (camRotY < 180f)
        {
            mainCamera.transform.rotation = Quaternion.Euler(camRotX, camRotY, 0f);
            camRotY += 3f;
            camRotX -= 0.53f;
            yield return null;
        }
        //本が降ってくる
        for (int i = 0; i < fallingBooks.transform.childCount; i++)
        {
            fallingBooks.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
        //机に近づく
        while(moveCount < moveCountMax)
        {
            camRotX += (45.3f / (float)moveCountMax);
            camPosZ -= (4.7f / (float)moveCountMax);
            mainCamera.transform.rotation = Quaternion.Euler(camRotX, camRotY, 0f);
            mainCamera.transform.position = new Vector3(0f, 0f, camPosZ);
            moveCount++;
            yield return null;
        }
        print(moveCount);
        //本が開く, カメラがズームする
        while(resultBookShape.GetBlendShapeWeight(0) > 0f)
        {
            weight -= resultBookOpenSpeed;
            resultBookShape.SetBlendShapeWeight(0, weight);
            camOfmainCamera.fieldOfView -= 0.4f　* resultBookOpenSpeed;
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
