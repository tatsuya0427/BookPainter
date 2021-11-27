using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookCreater : ColorStorage
{
    [SerializeField] protected GameObject[] bookTemp;
    [SerializeField] protected Sprite[] designs;
    

    public Text test;
    private int colorNum;

    private colorType sendColorType;
    // void Start()
    // {
    //     CreateBook();
    // }

    public void CreateBook(){
        int r = Random.Range(0, bookTemp.Length);

        
        
        GameObject book;
        switch(r){
            case 0:
                book = Instantiate(bookTemp[0], new Vector3(2.45f, -1.014f, 2.078f), Quaternion.identity);
                book.transform.Rotate(-180, 0, -90, Space.World);
                colorNum = Random.Range(1, colorType.GetNames(typeof(colorType)).Length);

                

                book.GetComponent<BookState>().SetDesign(designs[Random.Range(0, designs.Length)], 0, colorNum);
                sendColorType = (colorType)colorType.ToObject(typeof(colorType), colorNum);


                test.text = sendColorType.ToString();
                break;
            case 1:
                book = Instantiate(bookTemp[1], new Vector3(2.45f, -1.014f, 2.38f), Quaternion.identity);
                book.transform.Rotate(-180, 0, -90, Space.World);
                colorNum = Random.Range(1, colorType.GetNames(typeof(colorType)).Length);
                book.GetComponent<BookState>().SetDesign(designs[Random.Range(0, designs.Length)], 1, colorNum);
                sendColorType = (colorType)colorType.ToObject(typeof(colorType), colorNum);
                test.text = sendColorType.ToString();
                break;
        }

        //GameObject book = Instantiate(bookTemp[], new Vector3(0, 0, 0.5f), Quaternion.identity);
        
        //0 0 0↓
        //114 0 -90
        //-294 0 0 
        //-180 0 -90
    }


}

//book2 r -90 -360 -270
//book1 r -90  -27 -242