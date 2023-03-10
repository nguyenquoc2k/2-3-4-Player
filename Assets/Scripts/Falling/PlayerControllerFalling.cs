using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerFalling : MonoBehaviour
{
    private float speed = 4.5f;
    private Rigidbody2D rb;
    Vector2 moveDirection;
    public FixedJoystick FixedJoystick;
    private Vector2 lastMovement;
    private bool isMoving = false;
    bool hasHitObstacle = false;
    bool hasHitHole = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        CheckAndRemovePlayer();
    }

    private void CheckAndRemovePlayer()
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, 0.2f);
        if (hitCollider!= null && hitCollider.CompareTag("Obstacle"))
        {
            hasHitObstacle = true;
        }

        if (hitCollider != null && hitCollider.CompareTag("Hole"))
        {
            hasHitHole = true;
        }

        if (hasHitHole && !hasHitObstacle)
        {
            Destroy(gameObject);
        }
        // Draw the circle in the Scene view
        float radius = 0.2f;
        int segments = 32;
        float angle = 0f;
        float angleStep = (2f * Mathf.PI) / segments;
        for (int i = 0; i < segments; i++) {
            Vector3 pos1 = transform.position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f);
            angle += angleStep;
            Vector3 pos2 = transform.position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f);
            Debug.DrawLine(pos1, pos2, Color.green);
        }
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
        }
        else
        {
            isMoving = false;
        }

        if (isMoving == false)
        {
        }
    }
}