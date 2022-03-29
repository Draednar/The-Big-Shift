using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Death : MonoBehaviour
{ 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(GameObject.FindWithTag("Player"))
        {
            Debug.Log("you are dead,surprise big not");
        }
        else { }
       
    }
}
