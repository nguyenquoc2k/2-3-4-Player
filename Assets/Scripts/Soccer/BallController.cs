using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    public static BallController Instances;
    public float speed = 4f;
    private Rigidbody2D rb;
    private Vector2 lastMovement;
    public bool isShooting = false;
    public GameObject goalLine1, goalLine2;
    public static event Action IncreaseScoreSoccerTeam1, IncreaseScoreSoccerTeam2;
    public static event Action HandleResetGame;
    public GameObject GK1, GK2;

    private void Awake()
    {
        Instances = this;
        rb = GetComponent<Rigidbody2D>();
    }

    public void FireBall(Vector2 vector2)
    {
        isShooting = true;
        GetComponent<CircleCollider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = vector2 * speed;
        transform.SetParent(CoreGameController.Instances.transform);
        Invoke("SetStateShoot", 1f);
    }

    public IEnumerator GKFireBall(Vector2 direction)
    {
        yield return new WaitForSeconds(2);
        if (GK1.transform.GetChild(1).childCount > 0 || GK2.transform.GetChild(1).childCount > 0)
        {
            isShooting = true;
            GetComponent<CircleCollider2D>().isTrigger = false;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            float
                randomAngle =
                    Random.Range(-45f, 45f); // Tạo giá trị ngẫu nhiên trong khoảng 0-30 độ chia đều về trái và phải
            direction = Quaternion.Euler(0f, 0f, randomAngle) * direction;
            Vector2 velocity = direction.normalized * speed;
            rb.velocity = velocity;
            transform.SetParent(CoreGameController.Instances.transform);
            Invoke("SetStateShoot", 1.5f);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GoalLine") && other.gameObject == goalLine1)
        {
            HandleIncreaseScoreTeam2();
        }
        else if (other.CompareTag("GoalLine") && other.gameObject == goalLine2)
        {
            HandleIncreaseScoreTeam1();
        }
    }

    private void HandleIncreaseScoreTeam1()
    {
        IncreaseScoreSoccerTeam1?.Invoke();
        Invoke("CallHanldeResetGame", 2f);
    }

    private void HandleIncreaseScoreTeam2()
    {
        IncreaseScoreSoccerTeam2?.Invoke();
        Invoke("CallHanldeResetGame", 2f);
    }

    void CallHanldeResetGame()
    {
        HandleResetGame?.Invoke();
    }

    void SetStateShoot()
    {
        isShooting = false;
    }

    public void SetSpeedBall()
    {
        rb.velocity = Vector2.zero;
    }
}