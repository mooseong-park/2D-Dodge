using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_V : MonoBehaviour
{
    
    private enum ESpanwPosition
    {
        TOP,
        BOTTOM,
    }
    [SerializeField]
    ESpanwPosition sp;

    [SerializeField]
    float moveSpeed = 3f;

    void OnEnable()
    {
        if (transform.position.y < 0)
        {
            sp = ESpanwPosition.BOTTOM;
        }

        else if (transform.position.y > 0)
        {
            sp = ESpanwPosition.TOP;
        }
    }

    void Update()
    {
        switch (sp)
        {
            case ESpanwPosition.BOTTOM:
                {
                    MoveUp();
                    break;
                }
            case ESpanwPosition.TOP:
                {
                    MoveDown();
                    break;
                }
        }
    }

    void MoveUp()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }

    void MoveDown()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
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
