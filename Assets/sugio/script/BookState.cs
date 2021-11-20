using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookState : CanChangeColorObjectTemplate{
    
    Rigidbody2D rbody;
    Renderer renderer;
    [SerializeField] protected Material[] _material;
    [SerializeField] protected internal GameObject bookDesign;//本の柄を描画するためのオブジェクト
    [SerializeField] protected internal GameObject bookCreater;//次の本を生成するためのオブジェクト
    private SpriteRenderer designRender;

    void Start(){
        rbody = this.GetComponent<Rigidbody2D>();
    }

    
    void Update(){

    }

    protected override void SwitchColor(colorType nowColor){
        Debug.Log("object " + GetObjectColor());
        
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
            case colorType.yellow:
                Debug.Log("switch yellow");
                //design.GetComponent<Renderer>().material.color = Color.yellow;
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

    public void SetDesign(Sprite design){
        designRender = bookDesign.GetComponent<SpriteRenderer>();
        designRender.sprite = design;
    }

    public void MoveCreate(){

    }

    public void MovePainted(){
        bookCreater.GetComponent<BookCreater>().CreateBook();
        Destroy(gameObject);
    }
}
