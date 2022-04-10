using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Animator animator;
    [SerializeField] Enemy enemy;
    public float invulnerabilityFrames;
    bool canTakeDamage = true;

    public bool hitPriority { get; set; }
    public bool died { get; set; }

    private void OnEnable()
    {
        died = false;
    }

    void Update()
    {
        IFrames();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && canTakeDamage)
        {
            hitPriority = true;
            collision.GetComponent<Movement>().SetCanJump(true);
            collision.GetComponent<Movement>().ApplyLittleJump();
            animator.SetTrigger("IsHit");
            canTakeDamage = false;
            enemy.startMoving = false;
            enemy.HitPoints -= 1;
            StartCoroutine(JumpTime());
        }
    }

    IEnumerator JumpTime()
    {
        yield return new WaitForSeconds(0.6f);
        enemy.DisableBoxCollider();
    }

    void IFrames()
    {
        if (enemy.HitPoints <= 0)
        {
            died = true;
        }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hit") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        {
            if (died)
            {
                enemy.gameObject.SetActive(false);
            }

            enemy.ActivateBoxCollider();

            canTakeDamage = true;
            enemy.startMoving = true;
        }

    }

}
