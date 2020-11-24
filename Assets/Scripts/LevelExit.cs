using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    private const int LAST_SCENE_INDEX = 2;
    private const float LEVEL_LOAD_DELAY = 2f;
    private const float LEVEL_EXIT_SLOW_MOTION_FACTOR = 0.2f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            StartCoroutine(LoadNextLevel());
        }
    }
    private IEnumerator LoadNextLevel()
    {
        Time.timeScale = LEVEL_EXIT_SLOW_MOTION_FACTOR;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        yield return new WaitForSecondsRealtime(LEVEL_LOAD_DELAY);
        Time.timeScale = 1f;
        if (currentSceneIndex != LAST_SCENE_INDEX)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

}
