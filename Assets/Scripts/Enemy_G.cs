using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_G : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform target;

    [SerializeField]
    float moveSpeed = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);    // 한 객체에 한번만 
        CancelInvoke();    // Monobehaviour에 Invoke가 있다면 
    }
}
