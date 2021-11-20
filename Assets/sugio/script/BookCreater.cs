using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookCreater : MonoBehaviour
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
        Debug.Log(bookTemp.Length);
        Debug.Log(designs.Length);
        GameObject book = Instantiate(bookTemp[Random.Range(0, bookTemp.Length)], new Vector3(0, 0, 0.5f), Quaternion.identity);
        book.transform.Rotate(-246, 0, -90, Space.World);
        book.GetComponent<BookState>().SetDesign(designs[Random.Range(0, designs.Length)]);

    }
}

//book2 r -90 -360 -270
//book1 r -90  -27 -242