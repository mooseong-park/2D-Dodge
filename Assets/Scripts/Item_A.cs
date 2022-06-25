using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_A : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 0.6f;
    public float randomX, randomY;
    public Vector2 lastVeloctiy;
    public Transform itemA;
    
    void Start()
    {
        #region Random Move
        rb = gameObject.GetComponent<Rigidbody2D>();
        randomX = Random.Range(-1f, 1f);
        randomY = Random.Range(-1f, 1f);

        rb.velocity = new Vector2(randomX * moveSpeed, randomY * moveSpeed);
        #endregion
    }

    void Update()
    {
        lastVeloctiy = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        #region Reflection
        var speed = lastVeloctiy.magnitude;
        var dir = Vector2.Reflect(lastVeloctiy.normalized, collision.contacts[0].normal);
        rb.velocity = dir * Mathf.Max(speed, 0f);
        #endregion

        #region Player Collision
        if (collision.gameObject.name == "Player")
        {
            gameObject.SetActive(false);
            ObjectPooler.SpawnFromPool<Item_Bomb>("Item_Bomb", itemA.position);
        }
        #endregion
    }

    void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);    // 한 객체에 한번만 
        CancelInvoke();    // Monobehaviour에 Invoke가 있다면 
    }
}
