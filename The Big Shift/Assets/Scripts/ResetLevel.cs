using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    List<Enemy> enemies = new List<Enemy>();
    [SerializeField] Animator animator;
    bool start = false;
    public float timeTransition;


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

    public void ChangeNextLevel()
    {
        StartCoroutine(Transition(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator Transition(int index)
    {
        animator.SetTrigger("Start");

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("FadeIn") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            start = true;
        }

        yield return new WaitUntil(() => start);

        start = false;

        SceneManager.LoadScene(index);
    }

    

    public static void ChangeSpecificScene(int index)
    {
        SceneManager.LoadScene(index);
    }

}
