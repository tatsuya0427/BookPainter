using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EventOn()
    {
        this.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
    }

    public void EventOff()
    {
        this.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
    }
}
