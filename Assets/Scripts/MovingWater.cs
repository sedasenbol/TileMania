using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWater : MonoBehaviour
{
    private const float movingSpeed = 0.5f;
    private Rigidbody2D rb;
    private void ChangePosition()
    {
        transform.position = new Vector3(0f, -6.3f, 0f);
    }
    private void OnEnable()
    {
        Player.PlayerIsDead += ChangePosition;
    }
    private void OnDisable()
    {
        Player.PlayerIsDead -= ChangePosition;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, movingSpeed);
    }
    private void Update()
    {
        if (transform.position.y >= 16)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
