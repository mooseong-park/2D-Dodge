using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    private float rotationSpeed = 720f;

    public Rigidbody2D rigid;
    public Transform player;

    public Vector2 movementDir;
    public Vector2 LimitMin, LimitMax;

    public bool ShieldTrigger;

    public static bool modeTrigger = false;

    private static PlayerController _instance;
    public static PlayerController getInstance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerController>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<PlayerController>();
                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        #region Move
        float movement_x = Input.GetAxis("Horizontal");

        float movement_y = Input.GetAxis("Vertical");

        movementDir = new Vector2(movement_x, movement_y);

        float inputMagnitude = Mathf.Clamp01(movementDir.magnitude);

        movementDir.Normalize();

        if (GameController.instance.gamePlaying)
        {
            transform.Translate(movementDir * moveSpeed * inputMagnitude * Time.deltaTime, Space.World);

            if (movementDir != Vector2.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, movementDir);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
        #endregion
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, LimitMin.x, LimitMax.x),
                                         Mathf.Clamp(transform.position.y, LimitMin.y, LimitMax.y));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            gameObject.SetActive(false);
            GameController.instance.EndGame();
        }
    }
}
