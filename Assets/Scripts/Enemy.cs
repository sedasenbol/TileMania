using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const float SPEED = 2f;
    private Rigidbody2D rb;
    private GameManager gameManager;
    private void Move()
    {
        rb.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * SPEED, 0f);
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (!gameManager.StateOfTheGame.IsAlive)
        {
            rb.velocity = new Vector2(0,0);
            return;
        }
            Move();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            transform.localScale = new Vector2(-Mathf.Sign(rb.velocity.x), 1f);
        }
    }
}
