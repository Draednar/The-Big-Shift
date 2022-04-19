using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoors : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] SaveData data;
    [SerializeField] List<GameObject> gates = new List<GameObject>();

    private void Update()
    {
        data.ReadXmlFile();

        for (int i = 0; i < data.levels.Length; i++)
        {
            if (data.levels[i].unlock == 1)
            {
                gates[i].SetActive(false);
            }
        }
    }

}
