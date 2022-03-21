using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update

    public InputMgr PlayerInput;
    Rigidbody2D rb;
    bool canJump = true;
    public float gravityForce, jumpForce, speed;

    Vector2 gravityDir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ApplyGravity();
        gravityDir = Vector2.down;
    }

    private void OnEnable()
    {
        PlayerInput.JumpEvent += ApplyJump;
        PlayerInput.GravityEvent += ChangeDirGravity;
    }

    private void OnDisable()
    {
        PlayerInput.JumpEvent -= ApplyJump;
        PlayerInput.GravityEvent -= ChangeDirGravity;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        ApplyGravity();
        MovePlayer();
    }

    void MovePlayer()
    {
        if (PlayerInput.MoveDir.normalized.magnitude > 0.01f)
        {
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
        }
    }

    void ApplyJump()
    {
        rb.velocity = transform.up * jumpForce;
    }

    void ChangeDirGravity(Vector2 dir)
    {
        gravityDir = dir;
        transform.up = -gravityDir;
    }

    void ApplyGravity()
    {
        rb.AddForce(gravityDir * gravityForce);
    }

}
