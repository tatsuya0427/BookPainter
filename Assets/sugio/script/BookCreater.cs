using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookCreater : ColorStorage
{
    [SerializeField] protected GameObject[] bookTemp;
    [SerializeField] protected Sprite[] designs;

    [SerializeField] protected GameObject bookComboManager;
    [SerializeField] protected GameObject nextColorText;
    private BookComboManager bcmComp = null;
    
    private int colorNum;
    private colorType sendColorType;//colorStorageに宣言されている列挙型から正解色を選んで格納する変数
    private int beforeColorNom = 0;//以前生成した本の正解色を保管しておく変数
    private GameObject book;//生成する本のオブジェクトを格納しておく
    private bool reverseColor = false;//trueの時は、指定の色以外が正解になる
    private bool lastBookReverse = false;//前の本の指定がnotだった時はtrue
    private bool lastBookBefore = false;//前の本の指定がbeforeだった時はtrue
    private Text displayColor;
    private string displayString = null;

    public void CreateBook(){//bookTempを元に、正解色を定めたオブジェクトの生成

        if(bcmComp == null){
            bcmComp = bookComboManager.GetComponent<BookComboManager>();
            displayColor = nextColorText.GetComponent<UnityEngine.UI.Text>();
        }

        int r = Random.Range(0, bookTemp.Length);    

        switch(r){
            case 0:
                book = Instantiate(bookTemp[0], new Vector3(2.45f, -1.014f, 2.118f), Quaternion.identity);
                book.transform.Rotate(-180, 0, -90, Space.World);
                break;
            case 1:
                book = Instantiate(bookTemp[1], new Vector3(2.45f, -1.014f, 2.38f), Quaternion.identity);
                book.transform.Rotate(-180, 0, -90, Space.World);
                break;
        }

        colorNum = Random.Range(1, colorType.GetNames(typeof(colorType)).Length);
        displayString = null;
        int reverse = 0;
        int before = 0;

        if(bcmComp.GetComboCount() > 8){
            reverse = Random.Range(0, 2);

            if(!this.lastBookReverse && !this.lastBookBefore){//前の本の指定がnotもしくはbeforeじゃない時はbeforeを出題できる
                before = Random.Range(0, 2);
            }else{
                before = 0;
            }
            
        }

        if(reverse > 0){
            this.reverseColor = true;
            this.lastBookReverse = true;
            displayString += "not ";
        }else{
            this.reverseColor = false;
            this.lastBookReverse = false;
        }

        if(before > 0){
            colorNum = beforeColorNom;
            this.lastBookBefore = true;
            displayString += "before";
        }else{
            sendColorType = (colorType)colorType.ToObject(typeof(colorType), colorNum);
            displayString += sendColorType.ToString();
            this.beforeColorNom = colorNum;
            this.lastBookBefore = false;
        }


        
        Debug.Log(displayString);
        displayColor.text = displayString;
        book.GetComponent<BookState>().SetDesign(designs[Random.Range(0, designs.Length)], r, colorNum, this.reverseColor);

    }


}

//book2 r -90 -360 -270
//book1 r -90  -27 -242