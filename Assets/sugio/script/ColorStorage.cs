using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColorStorage : MonoBehaviour{
    protected internal enum colorType{//色の種類を列挙型で定義しておく。(間違った値が入らないように)このクラスを継承したクラスを使用し、色の管理はこの列挙型で行う。
            white,
            black,
            red,
            blue,
            yellow,
            green

    }
}

