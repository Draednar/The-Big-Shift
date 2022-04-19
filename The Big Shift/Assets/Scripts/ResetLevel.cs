using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class ResetLevel : MonoBehaviour
{
    List<Enemy> enemies = new List<Enemy>();
    [SerializeField] Animator animator;
    bool start = false;
    public float timeTransition;
    [SerializeField] Movement m;
    public int buildIndex = 0;

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

    private void Update()
    {
        //Debug.Log(TimeSpan.FromSeconds(m.timer));
    }


    public void ChangeNextLevel(int value)
    {
        buildIndex = value;
        StartCoroutine(Transition(buildIndex));
    }

    IEnumerator Transition(int index)
    {
        animator.SetTrigger("Start");

        //if (animator.GetCurrentAnimatorStateInfo(0).IsName("FadeIn") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        //{
        //    start = true;
        //}

        //yield return new WaitUntil(() => start);

        //start = false;

        yield return new WaitForSeconds(2f);

        Debug.Log("entered");

        SceneManager.LoadScene(index);
    }

    

    public static void ChangeSpecificScene(int index)
    {
        SceneManager.LoadScene(index);
    }

}
