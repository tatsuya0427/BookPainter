using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : CanChangeColorObjectTemplate{
    
    Rigidbody2D rbody;

    void Start(){
        rbody = this.GetComponent<Rigidbody2D>();
    }

    
    void Update(){
        if(GetObjectColor() == colorType.blue){
            rbody.gravityScale = 1;
        }else{
            rbody.gravityScale = 0;
        }
    }

    protected override void SwitchColor(colorType nowColor){
        Debug.Log("object " + GetObjectColor());
        switch(GetObjectColor()){
            case colorType.white:
                Debug.Log("switch white");
                gameObject.GetComponent<Renderer>().material.color = Color.white;
            break;
            case colorType.black:
                Debug.Log("switch brack");
                gameObject.GetComponent<Renderer>().material.color = Color.black;
            break;
            case colorType.red:
                Debug.Log("switch red");
                gameObject.GetComponent<Renderer>().material.color = Color.red;
            break;
            case colorType.blue:
                Debug.Log("switch blue");
                gameObject.GetComponent<Renderer>().material.color = Color.blue;
            break;
            case colorType.yellow:
                Debug.Log("switch yellow");
                gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            break;
            case colorType.green:
                Debug.Log("switch green");
                gameObject.GetComponent<Renderer>().material.color = Color.green;
            break;
        }
    }
}
