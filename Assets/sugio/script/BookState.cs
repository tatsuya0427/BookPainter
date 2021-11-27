using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BookState : CanChangeColorObjectTemplate{
    
    Rigidbody2D rbody;
    Renderer renderer;
    [SerializeField] protected GameObject GamaManager;
    [SerializeField] protected internal GameObject bookDesign;//本の柄を描画するためのオブジェクト

    [SerializeField] protected internal AudioClip paintedSound;
    [SerializeField] protected internal AudioClip bookMoveSound;
    protected AudioSource _audioSource;
    private static BookCreater _bookCreater = null;//次の本を生成するためのオブジェクト
    private static BookScoreManager _bookScoreManager = null;
    private SpriteRenderer designRender;
    private int bookType = 0;
    private int truePattern;

    void Start(){
        rbody = this.GetComponent<Rigidbody2D>();
        if(_bookCreater == null){
            Debug.Log("create");
            _bookCreater = GameObject.Find("BookCreater").GetComponent<BookCreater>();
        }
        if(_bookScoreManager == null){
            Debug.Log("score");
            _bookScoreManager = GameObject.Find("BookScoreManager").GetComponent<BookScoreManager>();
        }
        SetChangeColorFlag(false);

        _audioSource = GetComponent<AudioSource>();
    }

    protected override void SwitchColor(colorType nowColor){
        //Debug.Log("object " + GetObjectColor())
        _audioSource.PlayOneShot(paintedSound);

        if((int)nowColor == truePattern){
            _bookScoreManager.AddScore(100, true);
            //addScore(100);
        }else{
            _bookScoreManager.AddScore(100, false);
            //addScore(-100);
        }
        
        switch(GetObjectColor()){
            case colorType.white:
                Debug.Log("switch white");
                //design.GetComponent<Renderer>().material.color = Color.white;
                //design.GetComponent<Renderer>().material = _material[0];
            break;
            case colorType.black:
                Debug.Log("switch brack");
                //design.GetComponent<Renderer>().material.color = Color.black;
                designRender.color = Color.black;
                SetChangeColorFlag(false);
                Invoke("MovePainted", 0.3f);
            break;
            case colorType.red:
                Debug.Log("switch red");
                //design.GetComponent<Renderer>().material = _material[1];
                designRender.color = Color.red;
                SetChangeColorFlag(false);
                Invoke("MovePainted", 0.3f);
            break;
            case colorType.blue:
                Debug.Log("switch blue");
                //design.GetComponent<Renderer>().material = _material[2];
                designRender.color = Color.blue;
                SetChangeColorFlag(false);
                Invoke("MovePainted", 0.3f);
            break;
            case colorType.green:
                Debug.Log("switch green");
                //design.GetComponent<Renderer>().material = _material[3];
                designRender.color = Color.green;
                SetChangeColorFlag(false);
                Invoke("MovePainted", 0.3f);
            break;
        }
    }

    public void SetDesign(Sprite design, int bookType, int truePattern){
        designRender = bookDesign.GetComponent<SpriteRenderer>();
        designRender.sprite = design;
        this.bookType = bookType;
        this.truePattern = truePattern;
        MoveCreate();
    }

    public void MoveCreate(){
        switch(this.bookType){
            case 0: 
                transform.DOLocalMove(new Vector3(-0.141f, -1.014f, 2.078f), 0.3f)
                    .OnComplete(CreateEnd);
            break;
            case 1:
                transform.DOLocalMove(new Vector3(-0.2f, -1.014f, 2.38f), 0.3f)
                    .OnComplete(CreateEnd);
            break;
        }
        
    }

    private void CreateEnd(){
        SetChangeColorFlag(true);
    }

    public void MovePainted(){
        _audioSource.PlayOneShot(bookMoveSound);
        switch(this.bookType){
            case 0:
                transform.DOLocalMove(new Vector3(-3f, -1.014f, 2.078f), 0.3f)
                    .OnComplete(PaintedEnd);
            break;
            case 1:
                transform.DOLocalMove(new Vector3(-3f, -1.014f, 2.38f), 0.3f)
                    .OnComplete(PaintedEnd);
            break;
        }
        _bookCreater.CreateBook();
    }

    private void PaintedEnd(){
        float x, y, z;
        x = Random.Range(-1.5f, 1.5f);
        z = Random.Range(-3.9f, -3.3f);
        y = Random.Range(1.50f, 3.0f);
        transform.position = new Vector3(x, y, z);
        transform.parent = GameObject.Find ("FallingBooks").transform;
    }

    private void addScore(int score){
        if(GamaManager != null){
            //GameManager.GetComponent<GameManager>().addScore(score);
        }else{
            Debug.Log(score);
        }
    }
}




//1 ~ -0.07 ~ -1