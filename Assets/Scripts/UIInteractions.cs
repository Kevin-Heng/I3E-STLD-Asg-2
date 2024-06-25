using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteractions : MonoBehaviour
{
    public void ClickFunction()
    {
        Debug.Log("Button clicked");
    }

    public void SliderChange(float sliderValue)
    {
        Debug.Log(sliderValue);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
