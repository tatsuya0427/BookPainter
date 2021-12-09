using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BookState : CanChangeColorObjectTemplate{
    
    Rigidbody2D rbody;
    Renderer renderer;

    [SerializeField] protected internal int standardAddPoint;//正解時、不正解時に追加、減少させる基本のスコア
    [SerializeField] protected internal GameObject bookDesign;//本の柄を描画するためのオブジェクト

    [SerializeField] protected internal AudioClip paintedSound;
    [SerializeField] protected internal AudioClip bookMoveSound;
    protected AudioSource _audioSource;
    private static BookCreater _bookCreater = null;//次の本を生成するためのオブジェクト
    private static BookScoreManager _bookScoreManager = null;
    private SpriteRenderer designRender;
    private int bookType = 0;
    private int truePattern;
    private bool setReverse = false;

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

        if(this.setReverse){
            if((int)nowColor != truePattern){
                _bookScoreManager.AddScore(standardAddPoint, true);
            }else{
                _bookScoreManager.AddScore(standardAddPoint, false);
            }
        }else{
            if((int)nowColor == truePattern){
                _bookScoreManager.AddScore(standardAddPoint, true);
            }else{
                _bookScoreManager.AddScore(standardAddPoint, false);
            }
        }
        
        
        switch(GetObjectColor()){
            case colorType.White:
                Debug.Log("switch white");
            break;
            case colorType.Black:
                // Debug.Log("switch brack");
                designRender.color = Color.black;
            break;
            case colorType.Red:
                // Debug.Log("switch red");
                designRender.color = Color.red;
            break;
            case colorType.Blue:
                // Debug.Log("switch blue");
                designRender.color = Color.blue;
            break;
            case colorType.Green:
                // Debug.Log("switch green");
                designRender.color = Color.green;
            break;
        }

        if(GetObjectColor() != colorType.White){
            SetChangeColorFlag(false);
            Invoke("MovePainted", 0.3f);
        }
    }

    public void SetDesign(Sprite design, int bookType, int truePattern, bool reverse){
        designRender = bookDesign.GetComponent<SpriteRenderer>();
        designRender.sprite = design;
        this.bookType = bookType;
        this.truePattern = truePattern;
        this.setReverse = reverse;
        MoveCreate();
    }

    public void MoveCreate(){
        switch(this.bookType){
            case 0: 
                transform.DOLocalMove(new Vector3(-0.141f, -1.014f, 2.155f), 0.3f)
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
        transform.localScale = new Vector3(1,1,1);
        transform.parent = GameObject.Find ("FallingBooks").transform;
    }
}




//1 ~ -0.07 ~ -1