using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerPlayerController : MonoBehaviour
{
    private float speed = 4.5f;
    private Rigidbody2D rb;
    Vector2 moveDirection;
    public FixedJoystick FixedJoystick;
    public Transform ballHolder;
    private Vector2 lastMovement;
    private bool isMoving = false, isProactive = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ballHolder = transform.GetChild(1);
        Transform parentTransform = UIInGameController.Instances.transform;
        if (transform.name == "Player1")
        {
            FixedJoystick = parentTransform.GetChild(0).GetComponent<FixedJoystick>();
        }
        else if (transform.name == "Player2")
        {
            FixedJoystick = parentTransform.GetChild(1).GetComponent<FixedJoystick>();
        }
        else if (transform.name == "Player3")
        {
            FixedJoystick = parentTransform.GetChild(2).GetComponent<FixedJoystick>();
        }
        else if (transform.name == "Player4")
        {
            FixedJoystick = parentTransform.GetChild(3).GetComponent<FixedJoystick>();
        }
    }

    void Update()
    {
        MyInput();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        rb.velocity = moveDirection * speed;
        if (rb.velocity != Vector2.zero)
        {
            transform.up = rb.velocity;
        }
    }

    private void MyInput()
    {
        float x = FixedJoystick.Horizontal;
        float y = FixedJoystick.Vertical;
        moveDirection = new Vector2(x, y);
        if (moveDirection != Vector2.zero)
        {
            lastMovement = moveDirection;
            isMoving = true;
            isProactive = false;
        }
        else
        {
            isMoving = false;
        }

        if (isMoving == false && ballHolder.transform.childCount > 0 && isProactive == false)
        {
            BallController.Instances.FireBall(lastMovement);
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

        if (other.gameObject.CompareTag("AreaLimited"))
        {
            other.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("AreaLimited"))
        {
            other.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    
}