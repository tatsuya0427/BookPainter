using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookScoreManager : MonoBehaviour
{
    [SerializeField] protected GameObject BookComboManager;
    private BookComboManager _bookComboManager;
    void Start()
    {
        if(_bookComboManager == null){
            _bookComboManager = BookComboManager.GetComponent<BookComboManager>();
        }
    }

    public void AddScore(int value, bool ans){
        int score = _bookComboManager.CheckCombo(value, ans);
        Debug.Log(score);
    }
}
