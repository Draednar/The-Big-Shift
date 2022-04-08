using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centipede : Boss
{

    [SerializeField] float MeleeRange, Speed, MaxSpawnTime, Delay;
    [SerializeField] Transform Origin, Parent;
    [SerializeField] LayerMask PlatformMask;
    [SerializeField] Animator wallLeft, wallRight, wallUp, wallDown;
    List<GameObject> Minions = new List<GameObject>();
    bool startNextAtk = false, coroutineRunning = false, spawnMinions, stopMoving = true, castAtkDone = false, canCastAtk = true;
    float delayAtk = 1f, movementDir = -1;

    int count = 0;

    int countCast = 0;

    enum WallActivation { Left = 1, Right, Up, Down}

    private void Awake()
    {
        for (int i = 0; i < Parent.childCount; i++)
        {
            Minions.Add(Parent.GetChild(i).gameObject);
        }
    }

    public override void Update()
    {
        if (spawnMinions)
        {
            SpawnMinions();
            return;
        }

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
        AttackMelee_2();
    }

    public override void AttackRanged()
    {
        int chance = Random.Range(0, 18);

        if (chance >= 0 && chance <= 10 || castAtkDone || !canCastAtk)
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
        delayAtk = Random.Range(2f, 4f);
        InvokeRepeating("SpawnMinions", MaxSpawnTime, Delay);
    }

    void SpawnMinions()
    {

        if (count >= 3)
        {
            count = 0;
            CancelInvoke();
            return;
        }

        float posX = Random.Range(-6.92f, 11.21f);
        float posY = Random.Range(2.66f, 5.49f);

        for (int i = 0; i < Minions.Count; i++)
        {
            if (!Minions[i].activeSelf)
            {
                Minions[i].SetActive(true);
                Minions[i].transform.localPosition = new Vector2(posX, posY);
                count++;
                break;
            }
        }
    }

    public override void AttackRanged_2()
    {
        animator.SetTrigger("Atk_Range_2");
        delayAtk = Random.Range(3f, 5f);

        for (int i = 0; i < 3; i++)
        {
            int value = Random.Range(1, 4);
            ActivateWall((WallActivation)value);
        }

        canCastAtk = false;
        castAtkDone = true;
        StartCoroutine(DeactivateCastAtk());

    }

    IEnumerator DeactivateCastAtk()
    {
        yield return new WaitForSeconds(8f);
        castAtkDone = false;
        DeactivateWall();
        yield return new WaitForSeconds(5f);
        canCastAtk = true;

    }

    void ActivateWall(WallActivation value)
    {
        switch (value)
        {
            case WallActivation.Left:
                wallLeft.SetBool("Activate", true);
                break;
            case WallActivation.Right:
                wallRight.SetBool("Activate", true);
                break;
            case WallActivation.Up:
                wallUp.SetBool("Activate", true);
                break;
            case WallActivation.Down:
                wallDown.SetBool("Activate", true);
                break;
        }
    }

    void DeactivateWall()
    {
        wallLeft.SetBool("Activate", false);
        wallRight.SetBool("Activate", false);
        wallUp.SetBool("Activate", false);
        wallDown.SetBool("Activate", false);
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
