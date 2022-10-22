using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Item_HMissile : MonoBehaviour
{
    public float speed = 5f;
    public float rotateSpeed = 200f;

    private Rigidbody2D rb;

    [SerializeField]
    private Transform target;

    public Vector2 top_right_corner;
    public Vector2 bottom_left_corner;

    private Collider2D[] results;
    private Collider2D[] ETargets;

    public GameObject DefaultTarget;

    public int cnt = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        results = new Collider2D[10];
        ETargets = new Collider2D[10];

        
    }

    void Update()
    {
        // 타겟이 정해진 경우 타겟으로 이동
        if (target != null)
        {
            Move();
        }

        // 타겟이 없거나 || 플레이어로 지정 되어있거나(디폴트값) || 타겟이 비활성화 상태(오브젝트풀링)인 적일 경우 새로 검사
        //
        if (target == null || target == DefaultTarget.transform || target.gameObject.activeSelf == false)
        {
            SearchEnemy();
        }
    }

    // 화면 지정 범위내에 적 검색하는 함수
    void SearchEnemy()
    {
        // 화면 내에 범위 지정하고 범위 내에 오브젝트를 담아냄
        int num_colliders = Physics2D.OverlapAreaNonAlloc(top_right_corner, bottom_left_corner, results);

        // 적 배열을 카운트 하기 위한 변수
        

        // 화면내에 검색된 배열 중 적을 필터링함
        for (int i = 0; i < num_colliders; i++)
        {
            if (results[i].gameObject.CompareTag("Enemy"))
            {
                // 적을 검사하였을 때 배열의 처음부터 값을 넣기 위함
                ETargets[cnt] = results[i];
                cnt++;
            }
        }

        // 적이 들어왔을 경우 랜덤 선택해서 이동
        if(cnt > 0)
        {
            target = ETargets[Random.Range(0, cnt)].transform;
        }

        // 카운트가 없을 경우 플레이어를 지정함
        else
        {
            target = DefaultTarget.transform;
        }

        cnt = 0;
    }

    void Move()
    {
        Vector2 direction = (Vector2)target.transform.position - rb.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;

        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);    // 한 객체에 한번만 
        CancelInvoke();    // Monobehaviour에 Invoke가 있다면 
    }
}
