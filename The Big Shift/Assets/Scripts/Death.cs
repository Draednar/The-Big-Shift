using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Death : MonoBehaviour
{
    Animator animator;    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(GameObject.FindWithTag("enemy"))
        {
            
            animator.SetBool("Collide", true);
            
        }
        else { }
       
    }
}
