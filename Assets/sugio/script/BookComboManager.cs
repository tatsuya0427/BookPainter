using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BookComboManager : MonoBehaviour
{
    [SerializeField] protected GameObject timeSlider;
    private Slider _slider;

    [SerializeField] protected float defaultComboContinueTime;

    private bool nowCombo = false;
    private int comboCount = 0;
    private float nowComboTime = 0;
    private float nowMaxComboTime = 0;
    private float comboTimeRimit = 1;
    


    void Start(){
        _slider = timeSlider.GetComponent<Slider>();
    }

    void Update(){
        if(nowCombo){
            nowComboTime -= Time.deltaTime;
            _slider.value = nowComboTime / nowMaxComboTime;
            if(nowComboTime <= 0f){
                ComboRiset();
            }
        }
    }

    public int CheckCombo(int value, bool ans){
        if(nowCombo && ans){
            _slider.value = 1;
            nowMaxComboTime = defaultComboContinueTime * comboTimeRimit;
            nowComboTime = nowMaxComboTime;
            comboCount ++;
            if(((comboCount % 10) == 0) && comboCount <= 30){
                comboTimeRimit *= 0.75f;
            }
            //return Math.Floor(value * (1f + comboCount * 0.01f));
            return value * comboCount;

        }else if(!nowCombo && ans){
            nowCombo = true;
            _slider.value = 1;
            nowMaxComboTime = defaultComboContinueTime * comboTimeRimit;
            nowComboTime = nowMaxComboTime;
            comboCount ++;
            //return Math.Floor(value * (1f + comboCount * 0.01f));
            return value * comboCount;

        }else if(nowCombo && !ans){
            ComboRiset();
            return value * -1;
        }else{
            return value * -1;
        }
    }

    private void ComboRiset(){
        _slider.value = 0;
        nowMaxComboTime = defaultComboContinueTime;
        nowComboTime = 0;
        comboCount = 0;
        comboTimeRimit = 1;
        nowCombo = false;
    }
}
