using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundTimer : MonoBehaviour
{

    public TMP_Text timerText;
    public float currentTime;
    public bool timerOn = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timerOn)
        {
            if(currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                updateTimer(currentTime);
            } 
            else
            {

            }
        }
    }

    public void updateTimer(float currentTime)
    {
        
        timerText.text = currentTime.ToString("00");
    }

    public float getCurrentTime()
    {
        return currentTime;
    }
}
