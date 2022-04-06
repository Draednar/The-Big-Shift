using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    Vector2 positionToFollow, direction;
    bool startAtk = false;
    Rigidbody2D rb;
    float speed = 10f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        direction = Vector2.zero;
        StartCoroutine(StartAttack());
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(0.8f);
        startAtk = true;
        TakePosition();
    }

    void TakePosition()
    {
        positionToFollow = player.transform.position;
        direction = positionToFollow - rb.position;
    }

    void Locomotion()
    {
        rb.position += direction.normalized * speed * Time.deltaTime;

        if (transform.localPosition.x >= 15.71f || transform.localPosition.x <= -12.49 || transform.localPosition.y >= 9.52f || transform.localPosition.y <= -7.4)
        {
            startAtk = false;
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (startAtk)
        {
            Locomotion();
        }
    }
}
