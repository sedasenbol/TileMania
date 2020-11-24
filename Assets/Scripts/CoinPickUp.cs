using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField]
    private AudioClip pickUpCoin;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            AudioSource.PlayClipAtPoint(pickUpCoin, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
    }
}
