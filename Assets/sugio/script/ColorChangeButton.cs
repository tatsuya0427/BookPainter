using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeButton : ColorStorage{
    [SerializeField] protected GameObject clickControl;//ClickControlに、押されたボタンの色(colorType)を送るための変数
    [SerializeField] protected colorType sendColor;//ボタンが押された時に、ClickControlの持つ色(setClickColor)をColorChangeButtonの持つ色(sendColor)に更新するための変数

    [SerializeField] protected internal AudioSource _changeColorSound;

    void Start(){
        //_changeColorSound = GetComponent<AudioSource>();
    }
    public void OnClick() {//ボタンが押された時に、ClikControlが持つ色(setClickColor)をsendColorに設定された値に変更する
        //clickControl.GetComponent<ClickControl>().SetClickControlColor((int)sendColor);
        clickControl.GetComponent<ClickControl>().SetClickControlColor(sendColor);
        //_changeColorSound.PlayOneShot(_changeColorSound.clip);
    }
}
