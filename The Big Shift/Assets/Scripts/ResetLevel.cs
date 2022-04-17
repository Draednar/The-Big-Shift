using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    List<Enemy> enemies = new List<Enemy>();

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            enemies.Add(transform.GetChild(i).GetComponent<Enemy>());
        }
    }

    private void OnEnable()
    {
        Movement.resetLevel += ResetEnemies;
    }

    private void OnDisable()
    {
        Movement.resetLevel -= ResetEnemies;
    }

    void ResetEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].gameObject.SetActive(true);
            enemies[i].ResetEnemy();
        }
    }

    public static void ChangeNextLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        index++;
        SceneManager.LoadScene(index);
    }

    public static void ChangeSpecificScene(int index)
    {
        SceneManager.LoadScene(index);
    }

}
