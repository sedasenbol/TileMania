using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCoin : MonoBehaviour
{
    [SerializeField]
    private AudioClip pickUpCoin;
    private Rigidbody2D rb;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            AudioSource.PlayClipAtPoint(pickUpCoin, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        rb.velocity = new Vector2(Random.Range(-10,10), Random.Range(5, 10));
    }
}
