using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AISoccerController : MonoBehaviour
{
    public Transform ballTransform;
    public bool team1, team2;
    public Transform ballHolder;
    private Vector2 lastMovement;
    private bool isMoving = false, isProactive = false, isHoldingBall = false;
    private float range = 3f;
    private float moveSpeed = 1f;
    private bool moveToBall;
    private Vector3 randomDirection;
    public Vector3 centerPos;
    private float width = 7f;
    private float height = 5f;
    private float moveTimer = 0f;
    private float moveDelay = 1f;

    public Transform target;

    // Update is called once per frame
    private void Awake()
    {
        ballHolder = transform.GetChild(1);
        randomDirection = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
        transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 0);
        transform.GetChild(1).localPosition = new Vector3(1.1f, 0, 0);
    }

    void Update()
    {
        if (GKSoccerController.isGKKeeping || BallController.Instances.isShooting) return;

        if (isHoldingBall)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            Vector2 lookDir = target.transform.position - transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            GetComponent<Rigidbody2D>().rotation = angle;
        }
        else if (moveToBall)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, ballTransform.position, moveSpeed * Time.deltaTime);
            Vector2 lookDir = ballTransform.position - transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            GetComponent<Rigidbody2D>().rotation = angle;
        }
        else
        {
            moveTimer -= Time.deltaTime;
            if (moveTimer <= 0f)
            {
                randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                moveTimer = moveDelay;
            }

            transform.position += randomDirection * moveSpeed * Time.deltaTime;
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, centerPos.x - width / 2, centerPos.x + width / 2),
                Mathf.Clamp(transform.position.y, centerPos.y - height / 2, centerPos.y + height / 2),
                transform.position.z);
            
        }
        float distance = Vector3.Distance(transform.position, ballTransform.position);
        if (distance < 0.5f)
        {
            moveToBall = true;
        }
        else if (distance < 1.5f)
        {
            moveToBall = true;
            transform.position = Vector3.MoveTowards(transform.position, ballTransform.position,
                moveSpeed * Time.deltaTime);
            Vector2 lookDir = ballTransform.position - transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            GetComponent<Rigidbody2D>().rotation = angle;
        }
        else
        {
            moveToBall = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (isMoving == false)
            {
                isProactive = true;
            }
            else
            {
                isProactive = false;
            }

            isHoldingBall = true;

            SoccerPlayerController player = collision.transform.parent.parent.GetComponent<SoccerPlayerController>();
            if (player != null && player != this)
            {
                player.ballHolder.transform.GetChild(0).parent = ballHolder;
            }

            BallController.Instances.isShooting = false;
            collision.transform.GetComponent<BallController>().SetSpeedBall();
            collision.transform.SetParent(ballHolder);
            collision.transform.localPosition = Vector3.zero;
            collision.transform.GetComponent<Rigidbody2D>().isKinematic = true;
            collision.transform.GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("ShotZone") && isHoldingBall == true)
        {
            if ((team1 && transform.position.x > 0) || (team2 && transform.position.x < 0))
            {
                float angle = transform.rotation.eulerAngles.z;
                // Tính toán vector vận tốc của quả bóng
                Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                BallController.Instances.FireBall(direction);
                isHoldingBall = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            if (isMoving == false)
            {
                isProactive = true;
            }
            else
            {
                isProactive = false;
            }

            isHoldingBall = true;
            SoccerPlayerController player = other.transform.parent.parent.GetComponent<SoccerPlayerController>();
            if (player != null && player != this)
            {
                player.ballHolder.transform.GetChild(0).parent = ballHolder;
            }

            other.transform.GetComponent<BallController>().SetSpeedBall();
            other.transform.SetParent(ballHolder);
            other.transform.localPosition = Vector3.zero;
            other.transform.GetComponent<Rigidbody2D>().isKinematic = true;
            other.transform.GetComponent<CircleCollider2D>().isTrigger = true;
        }
    }
}