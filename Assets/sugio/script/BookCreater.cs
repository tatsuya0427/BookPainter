using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookCreater : ColorStorage
{
    [SerializeField] protected GameObject[] bookTemp;
    [SerializeField] protected Sprite[] designs;
    void Start()
    {
        CreateBook();
    }
    void Update()
    {
        
    }

    public void CreateBook(){
        int r = Random.Range(0, bookTemp.Length);
        GameObject book;
        switch(r){
            case 0:
                book = Instantiate(bookTemp[0], new Vector3(1, 0, 0.5f), Quaternion.identity);
                book.transform.Rotate(-246, 0, -90, Space.World);
                book.GetComponent<BookState>().SetDesign(designs[Random.Range(0, designs.Length)], 0, (int)colorType.red);
                break;
            case 1:
                book = Instantiate(bookTemp[1], new Vector3(1, 0, 0.8f), Quaternion.identity);
                book.transform.Rotate(-246, 0, -90, Space.World);
                book.GetComponent<BookState>().SetDesign(designs[Random.Range(0, designs.Length)], 1, (int)colorType.red);
                break;
        }

        //GameObject book = Instantiate(bookTemp[], new Vector3(0, 0, 0.5f), Quaternion.identity);
        
        

    }
}

//book2 r -90 -360 -270
//book1 r -90  -27 -242