using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform origin, groundCenter, groundLeft, groundRight;
    public LayerMask PlatformMask;
    Animator animator;
    public InputMgr PlayerInput;
    Rigidbody2D rb;
    bool canJump = true;
    public float gravityForce, jumpForce, speed, forceCurve;

    Vector2 gravityDir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ApplyJumpInstant();
        gravityDir = Vector2.down;
    }

    private void OnEnable()
    {
        PlayerInput.JumpEvent += ApplyJumpInstant;
        PlayerInput.GravityEvent += ChangeDirGravity;
    }

    private void OnDisable()
    {
        PlayerInput.JumpEvent -= ApplyJumpInstant;
        PlayerInput.GravityEvent -= ChangeDirGravity;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        IsGrounded();
        ApplyGravityForce();
        MovePlayer();
    }

    void MovePlayer()
    {
        if (PlayerInput.MoveDir.normalized.magnitude > 0.01f)
        {
            FlipSprite();

            if (IsFacingWall())
            {
                return;
            }

            animator.SetBool("IsMoving", true);

            if (gravityDir == Vector2.right)
            {
                rb.position += new Vector2(0, PlayerInput.MoveDir.x) * speed * Time.deltaTime;
                return;
            }

            else if (gravityDir == -Vector2.right)
            {
                rb.position += new Vector2(0, -PlayerInput.MoveDir.x) * speed * Time.deltaTime;
                return;
            }

            rb.position += new Vector2(PlayerInput.MoveDir.x, 0) * speed * Time.deltaTime;

            return;
        }

        animator.SetBool("IsMoving", false);
    }

    void FlipSprite()
    {
        switch (gravityDir)
        {
            case Vector2 v when v.Equals(Vector2.down):

                if (PlayerInput.MoveDir.x == 1)
                {
                    rb.transform.rotation = Quaternion.Euler(0, 0, 0);
                }

                else
                {
                    rb.transform.rotation = Quaternion.Euler(0, 180, 0);
                }

                break;

            case Vector2 v when v.Equals(Vector2.up):

                if (PlayerInput.MoveDir.x == 1)
                {
                    rb.transform.rotation = Quaternion.Euler(-180, 0, 0);
                }

                else
                {
                    rb.transform.rotation = Quaternion.Euler(-180, 180, 0);
                }

                break;

            case Vector2 v when v.Equals(Vector2.left):

                if (PlayerInput.MoveDir.x == 1)
                {
                    rb.transform.rotation = Quaternion.Euler(0, 0, -90);
                }

                else
                {
                    rb.transform.rotation = Quaternion.Euler(-180, 0, -90);
                }

                break;

            case Vector2 v when v.Equals(Vector2.right):

                if (PlayerInput.MoveDir.x == 1)
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

    bool IsFacingWall()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(origin.position, transform.right, 0.2f, PlatformMask);

        if (raycastHit)
        {
            return true;
        }

        return false;

    }

    void ApplyJumpInstant()
    {
        if (canJump)
        {
            rb.velocity = transform.up * jumpForce;
        }
    }


    void IsGrounded()
    {
        RaycastHit2D raycastHitCenter = Physics2D.Raycast(groundCenter.position, -transform.up, 0.4f, PlatformMask);

        RaycastHit2D raycastHitLeft = Physics2D.Raycast(groundLeft.position, -transform.up, 0.4f, PlatformMask);

        RaycastHit2D raycastHitRight = Physics2D.Raycast(groundRight.position, -transform.up, 0.4f, PlatformMask);

        if (raycastHitCenter || raycastHitLeft || raycastHitRight)
        {
            canJump = true;
            return;
        }

        canJump = false;
        return;
    }


    void ChangeDirGravity(Vector2 dir)
    {
        gravityDir = dir;
        transform.up = -gravityDir;
        rb.velocity = new Vector2(rb.velocity.x / forceCurve, rb.velocity.y / forceCurve);
    }

    void ApplyGravityForce()
    {
        rb.AddForce(gravityDir * gravityForce, ForceMode2D.Impulse);
    }

    void ApplyGravityInstant()
    {
        if (!canJump)
        {
            rb.velocity = gravityDir * gravityForce;
        }
    }

}
