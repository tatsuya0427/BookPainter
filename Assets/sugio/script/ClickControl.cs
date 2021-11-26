using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ClickControl : ColorStorage{
    GameObject clickObject;//画面をクリックした際に、Rayを飛ばし、Rayにぶつかったオブジェクトを格納しておく
    [SerializeField] static colorType setClickColor;//CanChangeColorObjectTemplateを継承したスクリプトを持つオブジェクトをクリックした際にここに設定されている色に変更する

    [SerializeField] protected Texture2D[] cursorImage;
    
    void Start(){
        Cursor.SetCursor(cursorImage[0], new Vector2(95,95), CursorMode.Auto);
    }

    void Update(){
        if(!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()　&& Input.GetMouseButtonDown(0)){//マウスの左クリックを押した際にRayを飛ばして当たったオブジェクトを取得する
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            
            Debug.Log(Physics.RaycastAll(ray));
 
            if (Physics.Raycast(ray, out hit)) {
                clickObject = hit.collider.gameObject;
            }

            //オブジェクトを取得できた＆tagがcanChangeColor出会った場合に、そのオブジェクトの持つ色を自身の持つ色(ClickControl.csのnowColor)に変更する
            if(clickObject != null && !(clickObject.CompareTag("UIObject"))){//ここでUIObjectを持つタグは除外している理由として、UIをクリックした際に誤作動を起こさないため。そのためクリックするUIにはUIObjectのタグ付けをする必要がある
                if(clickObject.CompareTag("canChangeColor")){
                    clickObject.GetComponent<CanChangeColorObjectTemplate>().SetObjectColor(setClickColor);
                }
            }
        }
    }

    protected internal void SetClickControlColor(colorType targetColor){//このクラスが継承されているクラスが所持する色(nowColor)を変更するメソッド
            //setClickColor = (colorType)Enum.ToObject(typeof(colorType), targetColor);
            setClickColor = targetColor;
            Cursor.SetCursor(cursorImage[(int)targetColor], new Vector2(95,95), CursorMode.Auto);
            Debug.Log(setClickColor　+ "色を装備しました");
    }
}
