using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwompMovement : MonoBehaviour
{
    public Vector3 back; 
    public Vector3 forth; 
    float phase = 0;
    public float speed;
    float phaseDirection = 1; 

    void Update()
    {
        transform.position = Vector3.Lerp(back, forth, phase); 
        phase += Time.deltaTime * speed * phaseDirection;
        if (phase >= 1 || phase <= 0) phaseDirection *= -1; 
    }
}
