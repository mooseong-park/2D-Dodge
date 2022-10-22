using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Shield : MonoBehaviour
{
    public static float activeCountdown = 0f;
    SpriteRenderer sr;
    CircleCollider2D cc;

    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        cc = gameObject.GetComponent<CircleCollider2D>();
    }
    void Update()
    {
        // 일정 시간을 0이 될 때 까지 체크
        // 아이템을 먹을 경우 시간을 추가 할 수 있도록  (아이템 d에서 관리)
        if(activeCountdown <= 0)
        {
            sr.color = new Color(0, 0, 0, 0);
            cc.enabled = false;
            return;
        }
        else
        {
            cc.enabled = true;
            sr.color = new Color(0, 1, 0, 0.5f);
            activeCountdown -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
