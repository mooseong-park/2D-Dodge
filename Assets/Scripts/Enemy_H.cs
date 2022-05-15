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

    ESpanwPosition sp;

    [SerializeField]
    float moveSpeed = 3f;

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
    }

    void Update()
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
        if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Wall")
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
