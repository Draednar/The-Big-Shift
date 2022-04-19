using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CountDown : MonoBehaviour
{
    float currentTime=0f;
    float startingTime=0f;

    [SerializeField] Text countdownText;
    [SerializeField] Movement player;


    void Start()
    {
        currentTime = startingTime;
        
    }

    void Update()
    {
        currentTime += 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        
    }


}
