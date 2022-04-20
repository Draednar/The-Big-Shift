using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UpdateScore : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Text time, deaths;
    [SerializeField] SaveData data;
    public int index;
    

    // Update is called once per frame
    void Update()
    {
        data.ReadXmlFile();

        if (data.levels[index].time == 999999)
        {
            time.text = "00:00";
            deaths.text = "0";
        }

        else
        {
            string s = TimeSpan.FromMinutes(data.levels[index].time).ToString();

            string[] value = s.Split(':');

            time.text = value[0] + ":" + value[1];

            deaths.text = data.levels[index].deaths.ToString();
        }
    }
}
