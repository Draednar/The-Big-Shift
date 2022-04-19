using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class testTimer : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Movement player;
    public Text text;

    // Update is called once per frame
    void Update()
    {
        string s = TimeSpan.FromMinutes(player.timer).ToString();

        string[] value = s.Split(':');

        text.text = value[0] + ":" + value[1];
    }
}
