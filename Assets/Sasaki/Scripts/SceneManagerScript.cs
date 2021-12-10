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
    private GameObject gameManager;
    private GameManagerScript script;
    private AudioSource titleBGM;
    private int score;
    private string tweetMessage;


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
        titleBGM = GameObject.Find("TitleManager").GetComponent<AudioSource>();
        Debug.Log("ゲームシーンへ移動します");
        while (alpha < 1f)
        {
            alpha += speed;
            panelImage.color = new Color(0f, 0f, 0f, alpha);
            titleBGM.volume -= speed;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("GamePlayScene");
    }

    public void toTweet()
    {
        gameManager = GameObject.Find("GameManager");
        script = gameManager.GetComponent<GameManagerScript>();
        score = script.score;
        tweetMessage = "BookPainterで" + score.ToString() + "点獲得しました!";
        //naichilab.UnityRoomTweet.Tweet("bookpainter", tweetMessage, "unityroom", "bookpainter");
    }

    public void Ranking()
    {
        gameManager = GameObject.Find("GameManager");
        script = gameManager.GetComponent<GameManagerScript>();
        score = script.score;
        //naichilab.RankingLoader.Instance.SendScoreAndShowRanking(score);
    }
}
