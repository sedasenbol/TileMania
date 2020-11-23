using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const float HORIZONTAL_VELOCITY = 5f;
    private const float JUMP_VELOCITY = 20f;
    private bool isJumping = false;
    private Rigidbody2D rb;
    private Animator anim;
    private CapsuleCollider2D capsuleCollider;
    private void MoveHorizontally()
    {
        float horizontalVelocity = Input.GetAxis("Horizontal") * HORIZONTAL_VELOCITY;
        rb.velocity = new Vector2(horizontalVelocity, rb.velocity.y);
        bool iAmMovingHorizontally = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        anim.SetBool("Running", iAmMovingHorizontally);
        if (iAmMovingHorizontally)
        {
            transform.localScale = new Vector3(Mathf.Sign(rb.velocity.x), 1, 1);
        }
    }
    private void Jump()
    {
        rb.velocity += new Vector2(0f, JUMP_VELOCITY);
        isJumping = false;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {
            isJumping = true;
        }
    }
    private void FixedUpdate()
    {
        MoveHorizontally();
        if(isJumping)
        {
            Jump();
        }
    }
}
