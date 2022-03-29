using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    enum GravityDirection { UP, DOWN, LEFT, RIGHT }

    [SerializeField] Transform origin;
    [SerializeField] GravityDirection direction;
    [SerializeField] LayerMask PlatformMask;

    public float speed, waitForMovement, HitPoints;
    float movementDir = -1;

    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    Animator animator;

    bool wasOnGround = false;
    public bool startMoving { get; set; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        startMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Animations()
    {
        animator.SetBool("IsMoving", startMoving);
    }

    void Movement()
    {
        Animations();

        if (!startMoving)
        {
            return;
        }

        FlipSprite();

        CheckChangeDirection();

        if (direction == GravityDirection.RIGHT)
        {
            rb.position += new Vector2(0, movementDir) * speed * Time.deltaTime;
            return;
        }

        else if (direction == GravityDirection.LEFT)
        {
            rb.position += new Vector2(0, -movementDir) * speed * Time.deltaTime;
            return;
        }

        rb.position += new Vector2(movementDir, 0) * speed * Time.deltaTime;
    }

    void CheckChangeDirection()
    {
        RaycastHit2D raycastHitDown = Physics2D.Raycast(origin.position, -transform.up, 3f, PlatformMask);

        RaycastHit2D raycastHitFront = Physics2D.Raycast(origin.position, -transform.forward, 0.3f, PlatformMask);

        if (raycastHitFront)
        {
            startMoving = false;
            StartCoroutine(WaitForMovement());
            movementDir = -movementDir;
            return;
        }

        else if (raycastHitDown)
        {
            wasOnGround = true;
            return;
        }

        if (wasOnGround)
        {
            startMoving = false;
            wasOnGround = false;
            StartCoroutine(WaitForMovement());
            movementDir = -movementDir;
            return;
        }

    }

    void FlipSprite()
    {
        switch (direction)
        {
            case GravityDirection.UP:

                if (-movementDir == 1)
                {
                    rb.transform.rotation = Quaternion.Euler(0, 0, 0);
                }

                else
                {
                    rb.transform.rotation = Quaternion.Euler(0, 180, 0);
                }

                break;

            case GravityDirection.DOWN:

                if (-movementDir == 1)
                {
                    rb.transform.rotation = Quaternion.Euler(-180, 0, 0);
                }

                else
                {
                    rb.transform.rotation = Quaternion.Euler(-180, 180, 0);
                }

                break;

            case GravityDirection.LEFT:

                if (-movementDir == 1)
                {
                    rb.transform.rotation = Quaternion.Euler(0, 0, -90);
                }

                else
                {
                    rb.transform.rotation = Quaternion.Euler(-180, 0, -90);
                }

                break;

            case GravityDirection.RIGHT:

                if (-movementDir == 1)
                {
                    rb.transform.rotation = Quaternion.Euler(0, 0, 90);
                }

                else
                {
                    rb.transform.rotation = Quaternion.Euler(-180, 0, 90);
                }

                break;
        }
    }

    public void DisableBoxCollider()
    {
        boxCollider.enabled = false;
    }

    public void ActivateBoxCollider()
    {
        boxCollider.enabled = true;
    }

    IEnumerator WaitForMovement()
    {
        yield return new WaitForSeconds(waitForMovement);
        startMoving = true;

    }



}
