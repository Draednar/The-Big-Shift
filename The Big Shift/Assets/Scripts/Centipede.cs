using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centipede : Boss
{

    [SerializeField] float MeleeRange, Speed;
    [SerializeField] Transform Origin;
    [SerializeField] LayerMask PlatformMask;
    bool startNextAtk = false, coroutineRunning = false, stopMoving = true;
    float delayAtk = 1f, movementDir = -1;


    public override void Update()
    {
        if (startNextAtk && stopMoving)
        {
            CheckDistanceToPlayer();
            return;
        }

        else if (!stopMoving)
        {
            Locomotion();
            return;
        }

        if (!coroutineRunning && !startNextAtk)
        {
            StartCoroutine(DelayAttack());
        }
    }

    IEnumerator DelayAttack()
    {
        coroutineRunning = true;

        startNextAtk = false;

        yield return new WaitForSeconds(delayAtk);

        coroutineRunning = false;

        startNextAtk = true;
    }

    public override void CheckDistanceToPlayer()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < MeleeRange)
        {
            AttackMelee();
            startNextAtk = false;
            return;
        }

        AttackRanged();
        startNextAtk = false;

    }

    public override void AttackMelee()
    {
        int chance = Random.Range(0, 18);

        if (chance >= 0 && chance <= 8)
        {
            AttackMelee_1();
            return;
        }

        AttackMelee_2();
    }

    public override void AttackRanged()
    {
        int chance = Random.Range(0, 18);

        if (chance >= 0 && chance <= 10)
        {
            AttackRanged_1();
            return;
        }

        AttackRanged_2();
    }

    public override void AttackMelee_1()
    {
        animator.SetTrigger("Atk_Melee_1");
        delayAtk = Random.Range(1.3f, 2f);
    }

    public override void AttackMelee_2()
    {
        animator.SetBool("Atk_Melee_2 0", true);
        delayAtk = Random.Range(1.5f, 2f);
        stopMoving = false;
    }

    public override void AttackRanged_1()
    {
        animator.SetTrigger("Atk_Range_1");
        delayAtk = Random.Range(1.5f, 3f);
    }

    public override void AttackRanged_2()
    {
        animator.SetTrigger("Atk_Range_2");
        delayAtk = Random.Range(2f, 3.5f);
    }

    void Locomotion()
    {
        rb.position += new Vector2(movementDir, 0) * Speed * Time.deltaTime;
        FlipSprite();
        CheckWall();
    }

    void FlipSprite()
    {
        if (-movementDir == 1)
        {
            rb.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        else
        {
            rb.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void CheckWall()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(Origin.position, -transform.forward, 1f, PlatformMask);

        if (raycastHit)
        {
            movementDir = -movementDir;
            stopMoving = true;
            Debug.Log("hit something");
            animator.SetBool("Atk_Melee_2 0", false);
            FlipSprite();
        }
    }
}
