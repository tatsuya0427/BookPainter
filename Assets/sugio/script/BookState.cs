using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BookState : CanChangeColorObjectTemplate{
    
    Rigidbody2D rbody;
    Renderer renderer;
    [SerializeField] protected GameObject GamaManager;
    [SerializeField] protected internal GameObject bookDesign;//本の柄を描画するためのオブジェクト
    protected internal GameObject bookCreater;//次の本を生成するためのオブジェクト
    private SpriteRenderer designRender;
    private int bookType = 0;
    private int truePattern;

    void Start(){
        rbody = this.GetComponent<Rigidbody2D>();
        bookCreater = GameObject.Find("BookCreater");
    }

    
    void Update(){

    }

    protected override void SwitchColor(colorType nowColor){
        //Debug.Log("object " + GetObjectColor());

        if((int)nowColor == truePattern){
            addScore(100);
        }else{
            addScore(-100);
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
                transform.DOLocalMove(new Vector3(0, 0, 0.5f), 0.3f);
            break;
            case 1:
                transform.DOLocalMove(new Vector3(0, 0, 0.8f), 0.3f);
            break;
        }
        
    }

    public void MovePainted(){
        switch(this.bookType){
            case 0:
                transform.DOLocalMove(new Vector3(-1, 0, 0.5f), 0.3f)
                    .OnComplete(MoveEnd);
            break;
            case 1:
                transform.DOLocalMove(new Vector3(-1, 0, 0.8f), 0.3f)
                    .OnComplete(MoveEnd);
            break;
        }
        bookCreater.GetComponent<BookCreater>().CreateBook();
    }

    private void MoveEnd(){
        //Debug.Log("moveend");
        //Destroy(gameObject);
        transform.parent = GameObject.Find ("BookStrage").transform;
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