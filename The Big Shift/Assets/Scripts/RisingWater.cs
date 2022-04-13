using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingWater : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer sr;
    public float speed, MaxHeight;

    public bool start { get; set; }

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        start = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            sr.size = new Vector2(sr.size.x, sr.size.y + speed);
            sr.size = new Vector2(sr.size.x, Mathf.Clamp(sr.size.y, 0, MaxHeight));
        }
    }
}
