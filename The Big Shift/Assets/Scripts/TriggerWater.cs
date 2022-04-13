using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWater : MonoBehaviour
{

    [SerializeField] RisingWater water;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            water.start = true;
        }
    }

}
