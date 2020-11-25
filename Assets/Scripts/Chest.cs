using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private const float DESTROY_DELAY = 1f;
    private Animator anim;
    private GameObject chestCoinPrefab;
    private bool isClosed = true;
    void Start()
    {
        anim = GetComponent<Animator>();
        chestCoinPrefab = Resources.Load<GameObject>("Prefabs/ChestCoin");
    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.layer == 10 && isClosed)
        {
            isClosed = false;
            anim.SetTrigger("Open");
            int random = Random.Range(5, 10);
            for (int i = 0; i < random; i++)
            {
                GameObject chestCoin = Instantiate(chestCoinPrefab, transform.position + new Vector3(0, 2, 0), Quaternion.identity, transform.parent) as GameObject;
            }
            Destroy(this.gameObject, anim.GetCurrentAnimatorStateInfo(0).length + DESTROY_DELAY);
        }   
    }
}
