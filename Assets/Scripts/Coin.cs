using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private AudioClip pickUpCoin;
    private bool isPlayerAlive = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 && isPlayerAlive)
        {
            AudioSource.PlayClipAtPoint(pickUpCoin, Camera.main.transform.position);
            Destroy(this.gameObject);
        }
    }
    private void OnEnable()
    {
        Player.PlayerIsDead += IsPlayerDead;  
    }
    private void OnDisable()
    {
        Player.PlayerIsDead -= IsPlayerDead;
    }
    private void IsPlayerDead()
    {
        isPlayerAlive = false;
    }

}
