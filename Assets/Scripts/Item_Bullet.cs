using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;

public class Item_Bullet : MonoBehaviour
{
    //public float speed = 30f;
    //Vector2 movement;
    public Rigidbody2D rigid;
    public Vector2 movDir, defDir;
    private float bulletSpeed = 4.5f;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        movDir = PlayerController.getInstance.movementDir;

        Transform[] myChilds = PlayerController.getInstance.GetComponentsInChildren<Transform>();
        defDir = myChilds[1].transform.position - myChilds[2].transform.position;
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (movDir != Vector2.zero)
        {
            rigid.velocity = movDir.normalized * bulletSpeed;
        }
        else
        {
            rigid.velocity = defDir.normalized * bulletSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Wall"))
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
