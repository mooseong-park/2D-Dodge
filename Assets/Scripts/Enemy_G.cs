using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_G : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform target;

    [SerializeField]
    float moveSpeed = 3f;

    bool canMove;
    CircleCollider2D col;

    void Awake()
    {
        col = gameObject.GetComponent<CircleCollider2D>();
    }

    void OnEnable()
    {
        StartCoroutine(Delay());
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if(canMove)
        {
            FollowTarget();
        }
    }

    IEnumerator Delay()
    {
        canMove = false;
        col.enabled = false;

        yield return new WaitForSeconds(1.0f);
        col.enabled = true;
        canMove = true;
    }

    void FollowTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (canMove && collision.gameObject.CompareTag("Player"))
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
