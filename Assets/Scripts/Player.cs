using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float RUN_SPEED = 5f;
    private const float JUMP_SPEED = 25f;
    private const float CLIMB_SPEED = 5f;
    private bool isJumping = false;
    private bool isAlive = true;
    private Rigidbody2D rb;
    private Animator anim;
    private CapsuleCollider2D capsuleCollider;
    private BoxCollider2D boxCollider;
    private void Run()
    {
        float runSpeed = Input.GetAxis("Horizontal") * RUN_SPEED;
        rb.velocity = new Vector2(runSpeed, rb.velocity.y);
        bool isRunning = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        anim.SetBool("Running", isRunning);
        if (isRunning)
        {
            transform.localScale = new Vector3(Mathf.Sign(rb.velocity.x), 1, 1);
        }
    }
    private void Jump()
    {
        rb.velocity += new Vector2(0f, JUMP_SPEED);
        isJumping = false;
    }
    private void ClimbLadder()
    {
        if (capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rb.velocity = new Vector2(rb.velocity.x, Input.GetAxis("Vertical") * CLIMB_SPEED);
            rb.gravityScale = 0;
            bool isClimbing = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
            anim.SetBool("Climbing", isClimbing);
        }
        else
        {
            anim.SetBool("Climbing", false);
            rb.gravityScale = 1;
        }
    }
    private void Die()
    {
        anim.SetTrigger("Die");
        isAlive = false;
        //event
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (!isAlive)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && boxCollider.IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {
            isJumping = true;
        }
    }
    private void FixedUpdate()
    {
        if (!isAlive)
        {
            return;
        }
        Run();
        if(isJumping)
        {
            Jump();
        }
        ClimbLadder();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (new List<int> {12,13,14,4}.Contains(collision.gameObject.layer))
        {
            Die();
        }
    }
}
