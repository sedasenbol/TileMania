using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private AudioClip pickUpCoin;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 10)
        {
            AudioSource.PlayClipAtPoint(pickUpCoin, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
    }
    private void Update()
    {

    }
}
