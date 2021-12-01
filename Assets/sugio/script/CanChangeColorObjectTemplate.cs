using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class CanChangeColorObjectTemplate : ColorStorage{//このクラスを継承して、色を変えることのできるギミックを作成してもらう。

    [SerializeField] protected internal colorType objectColor;//CanChangeColorObjectTemplateを継承したスクリプトを持つオブジェクトの色を格納する
    [SerializeField] protected internal bool changeColorFlag = true;//CanChangeColorObjectTemplateを継承したスクリプトを持つオブジェクトが現在色を変えることのできるオブジェクトかどうか



    abstract protected void SwitchColor(colorType nowColor);//colorStrageにて、setColorが行われた時に実行されるメソッド

    protected internal void SetObjectColor(colorType targetColor){//このクラスが継承されているクラスが所持する色(nowColor)を変更するメソッド
            //this.objectColor = (colorType)Enum.ToObject(typeof(colorType), targetColor);
            if(this.changeColorFlag){
                this.objectColor = targetColor;
                SwitchColor(targetColor);
                //Debug.Log(gameObject.name + "は、" + this.objectColor + "に設定されました");
            }
    }

    protected internal colorType GetObjectColor(){//このクラスが継承されているクラスが、現在設定されている色(nowColor)を列挙型で取得するためのメソッド。
        return this.objectColor;
    }

    protected internal void SetChangeColorFlag(bool setChange){//このオブジェクトが現在色を変えることができる状態かどうかを切り替えるメソッド
        this.changeColorFlag = setChange;
    }
    protected internal bool GetChangeColorFlag(){//このオブジェクトが現在色を変えることができる状態かどうかを取得するメソッド
        return this.changeColorFlag;
    }

    void SetDesign(){
        
    }
}