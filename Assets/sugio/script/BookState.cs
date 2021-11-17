using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookState : CanChangeColorObjectTemplate{
    
    Rigidbody2D rbody;
    Renderer renderer;
    [SerializeField] protected GameObject design;
    [SerializeField] protected Material[] _material;

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
                design.GetComponent<Renderer>().material = _material[0];
            break;
            case colorType.black:
                Debug.Log("switch brack");
                design.GetComponent<Renderer>().material.color = Color.black;
            break;
            case colorType.red:
                Debug.Log("switch red");
                design.GetComponent<Renderer>().material = _material[1];
            break;
            case colorType.blue:
                Debug.Log("switch blue");
                design.GetComponent<Renderer>().material = _material[2];
            break;
            case colorType.yellow:
                Debug.Log("switch yellow");
                design.GetComponent<Renderer>().material.color = Color.yellow;
            break;
            case colorType.green:
                Debug.Log("switch green");
                design.GetComponent<Renderer>().material = _material[3];
            break;
        }
    }
}
