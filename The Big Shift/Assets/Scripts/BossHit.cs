using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHit : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Animator animator;
    [SerializeField] Boss enemy; // need to change to boss
    [SerializeField] GameObject flag;
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

            Debug.Log("took dmg");

            hitPriority = true;
            collision.GetComponent<Movement>().SetCanJump(true);
            collision.GetComponent<Movement>().ApplyLittleJump();
            animator.SetTrigger("IsHit");
            canTakeDamage = false;
            //enemy.startMoving = false;
            //enemy.HitPoints -= 1;
            enemy.hitPoint -= 1;
            enemy.AttackMelee_2();
            StartCoroutine(JumpTime());
        }
    }

    IEnumerator JumpTime()
    {
        yield return new WaitForSeconds(invulnerabilityFrames);
        //canTakeDamage = true;
        //enemy.DisableBoxCollider();
    }

    void IFrames()
    {
        if (enemy.hitPoint <= 0)
        {
            died = true;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hit") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        {
            if (died)
            {
                enemy.gameObject.SetActive(false);
                flag.gameObject.SetActive(true);
            }

            //    //enemy.ActivateBoxCollider();

            canTakeDamage = true;
            //    //enemy.startMoving = true;
            //}

        }

    }
}
