using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update

    protected Animator animator;
    protected Rigidbody2D rb;
    [SerializeField] protected GameObject player;
    [SerializeField] protected float HP;

    public float hitPoint { get; set; }

    void Start()
    {
        hitPoint = HP;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void CheckDistanceToPlayer()
    {

    }

    public virtual void AttackRanged()
    {

    }

    public virtual void AttackMelee()
    {

    }

    public virtual void AttackMelee_1()
    {

    }

    public virtual void AttackMelee_2()
    {

    }

    public virtual void AttackRanged_1()
    {

    }

    public virtual void AttackRanged_2()
    {

    }


}
