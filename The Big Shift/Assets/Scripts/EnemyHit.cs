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

    void Update()
    {
        IFrames();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && canTakeDamage)
        {
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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hit") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        {
            if (enemy.HitPoints <= 0)
            {
                enemy.gameObject.SetActive(false);
            }

            enemy.ActivateBoxCollider();

            canTakeDamage = true;
            enemy.startMoving = true;
        }

    }

}
