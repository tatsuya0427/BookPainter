using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookScoreManager : MonoBehaviour
{
    [SerializeField] protected GameObject BookComboManager;


    private BookComboManager _bookComboManager;
    private GameManagerScript _gameManagerScript;
    void Start()
    {
        if(_bookComboManager == null){
            _bookComboManager = BookComboManager.GetComponent<BookComboManager>();
        }
        if(_gameManagerScript == null){
            _gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        }
    }

    public void AddScore(int value, bool ans){
        int score = _bookComboManager.CheckCombo(value, ans);
        _gameManagerScript.addScore(score);
    }
}
