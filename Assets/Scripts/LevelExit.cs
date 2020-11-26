using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public delegate void NextLevel();
    public static event NextLevel ExitLevel;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10 && ExitLevel != null)
        {
            ExitLevel();
        }
    }
}
