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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ApplyGravity();
    }

    private void OnEnable()
    {
        PlayerInput.jumpEvent += ApplyJump;
    }

    private void OnDisable()
    {
        PlayerInput.jumpEvent -= ApplyJump;
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
            rb.position += new Vector2(PlayerInput.MoveDir.x, PlayerInput.MoveDir.y) * speed * Time.deltaTime;
        }
    }

    void ApplyJump()
    {
        canJump = false;

        rb.velocity = Vector2.up * jumpForce;
    }

    void IsGrounded()
    {
        canJump = true;
    }

    void ApplyGravity()
    {
        rb.AddForce(Vector2.down * gravityForce);
    }

}
