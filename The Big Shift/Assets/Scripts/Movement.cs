using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    enum GravityDirection { UP, DOWN, LEFT, RIGHT }

    // Start is called before the first frame update
    [SerializeField] Transform origin, groundCenter, groundLeft, groundRight;
    [SerializeField] GravityDirection gravity;
    [SerializeField] BoxCollider2D boxCollider, jumpCollider;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] ResetLevel reset;
    [SerializeField] AudioMgr clips;
    public GameObject TCamera;
    public int indexLevel;
    public LayerMask PlatformMask;
    Animator animator;
    public InputMgr PlayerInput;
    Rigidbody2D rb;
    bool canJump = true, wasOnGroundBefore = false, coroutineRunning = false, enemyContact = false, hitContact = false, counter = false;
    public float gravityForce, jumpForce, speed, forceCurve, coyoteTime, NchangeGravity;
    float jumpCounter = 0, gravityCounter = 0, maxTimer = 10;
    bool startFallTime = false;
    public float fallTime { get; private set; }
    public float timer { get; private set; }

    public int deathCounter { get; private set; }

    Vector2 gravityDir, startPos;

    public delegate void Reset();
    public static event Reset resetLevel;

    public delegate void Score(string name, int i, float time, int deaths);
    public static event Score playerScore;

    void Start()
    {
        StartGravityDirection();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = 0;
        Debug.Log(TCamera);
        startPos = rb.position;
        fallTime = 0;
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
        //CheckEnemyContact();
        MovePlayer();
    }

    private void Update()
    {
        if (PlayerInput.pressedFirstInput)
        {
            timer += Time.deltaTime;
        }

        if (startFallTime)
        {
            fallTime += Time.deltaTime;
        }
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
        if (canJump && jumpCounter <= 0)
        {
            rb.velocity = transform.up * jumpForce;
            jumpCounter += 1;
            clips.PlayClip(1);
        }
    }

    public void ApplyLittleJump()
    {
        rb.velocity = transform.up * (jumpForce / 2);
    }

    bool IsGrounded()
    {
        RaycastHit2D raycastHitCenter = Physics2D.Raycast(groundCenter.position, -transform.up, 1.2f, PlatformMask);

        RaycastHit2D raycastHitLeft = Physics2D.Raycast(groundLeft.position, -transform.up, 1.2f, PlatformMask);

        RaycastHit2D raycastHitRight = Physics2D.Raycast(groundRight.position, -transform.up, 1.2f, PlatformMask);

        if (!wasOnGroundBefore && raycastHitLeft || raycastHitCenter || raycastHitRight)
        {
            startFallTime = false;
            wasOnGroundBefore = true;

            if (fallTime > 0.35f)
            {
                StartCoroutine(CameraShake(0.1f, fallTime));
            }

            fallTime = 0;
        }

        if (raycastHitLeft)
        {
            canJump = true;
            wasOnGroundBefore = true;
            //animator.SetBool("IsJumping", false);
            jumpCounter = 0;
            gravityCounter = 0;
            return true;
        }

        else if (raycastHitCenter || raycastHitRight)
        {
            canJump = true;
            //animator.SetBool("IsJumping", false);
            jumpCounter = 0;
            gravityCounter = 0;
            return true;
        }

        if (wasOnGroundBefore && !coroutineRunning)
        {
            wasOnGroundBefore = false;
            canJump = true;
            startFallTime = true;
            StartCoroutine(CoyoteTime());
            return false;
        }

        else
        {
            //animator.SetBool("IsJumping", true);
            canJump = false;
            startFallTime = true;
            return false;
        }


    }

    IEnumerator CoyoteTime()
    {
        coroutineRunning = true;
        yield return new WaitForSeconds(coyoteTime);
        canJump = false;
        coroutineRunning = false;
    }

    void StartGravityDirection()
    {
        switch (gravity)
        {
            case GravityDirection.UP:
                gravityDir = Vector2.up;
                break;
            case GravityDirection.DOWN:
                gravityDir = Vector2.down;
                break;
            case GravityDirection.LEFT:
                gravityDir = Vector2.left;
                break;
            case GravityDirection.RIGHT:
                gravityDir = Vector2.right;
                break;
        }

        transform.up = -gravityDir;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Traps")
        {
            PlayerDeath();
            return;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Levels")
        {
            reset.ChangeNextLevel(1);
            playerScore.Invoke(SceneManager.GetActiveScene().name, indexLevel, timer, deathCounter);
        }
    }

    public void PlayerDeath()
    {
        deathCounter++;
        clips.PlayClip(3);
        rb.position = startPos;
        fallTime = 0;
        wasOnGroundBefore = false;
        StartGravityDirection();
        resetLevel.Invoke();
    }

    IEnumerator CameraShake(float duration, float force)
    {
        Vector3 originalPos = TCamera.transform.localPosition;

        float elapsedTime = 0f;

        clips.PlayClip(4);

        while (elapsedTime <= duration)
        {
            float x = Random.Range(-1f, 1f) * force / 8;
            float y = Random.Range(-1f, 1f) * force / 8;

            TCamera.transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsedTime += Time.deltaTime;

            if (Gamepad.current != null)
            {
                Gamepad.current.SetMotorSpeeds(force, force);
            }


            yield return null;
        }

        TCamera.transform.localPosition = originalPos;

        if (Gamepad.current != null)
        {
            Gamepad.current.ResetHaptics();
        }

    }

    void ChangeDirGravity(Vector2 dir)
    {
        gravityCounter++;

        if (gravityCounter <= NchangeGravity)
        {
            gravityDir = dir;
            transform.up = -gravityDir;
            rb.velocity = new Vector2(rb.velocity.x / forceCurve, rb.velocity.y / forceCurve);
            clips.PlayClip(2);
        }

    }

    void ApplyGravityForce()
    {
        rb.AddForce(gravityDir * gravityForce, ForceMode2D.Impulse);
    }

    public void SetCanJump(bool value)
    {
        canJump = value;
    }

    void ApplyGravityInstant()
    {
        if (!canJump)
        {
            rb.velocity = gravityDir * gravityForce;
        }
    }

    public void PlayMovement()
    {
        if (IsGrounded())
        {
            clips.PlayClip(0);
        }
    }

}
