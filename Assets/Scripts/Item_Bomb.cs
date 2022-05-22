using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Bomb : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(Bomb());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
        }
    }

    IEnumerator Bomb()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);    // 한 객체에 한번만 
        CancelInvoke();    // Monobehaviour에 Invoke가 있다면 
    }
}
