using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_H : MonoBehaviour
{
    private enum ESpanwPosition
    {
        RIGHT,
        LEFT,
    }
    bool canMove;
    ESpanwPosition sp;
    CircleCollider2D col;

    [SerializeField]
    float moveSpeed = 3f;

    void Awake()
    {
        col = gameObject.GetComponent<CircleCollider2D>();
    }

    void OnEnable()
    {
        if (transform.position.x < 0)
        {
            sp = ESpanwPosition.LEFT;
        }

        else if (transform.position.x > 0)
        {
            sp = ESpanwPosition.RIGHT;
        }
        StartCoroutine(Delay());
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if(canMove)
        {
            switch (sp)
            {
                case ESpanwPosition.RIGHT:
                    {
                        MoveLeft();
                        break;
                    }
                case ESpanwPosition.LEFT:
                    {
                        MoveRight();
                        break;
                    }
            }
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

    void MoveRight()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }

    void MoveLeft()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(canMove)
        {
            if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Wall"))
            {
                gameObject.SetActive(false);
            }
        }
    }

    void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);    // 한 객체에 한번만 
        CancelInvoke();    // Monobehaviour에 Invoke가 있다면
    }
}
