using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BotFlappyBird : MonoBehaviour
{
    public int indexPlayer;
    public float strength = 5f;
    public float gravity = -9.81f;
    public float tilt = 5f;
    public bool isFlyUp;
    public Vector3 direction;
    public bool isReverse;

    private void Awake()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
        if (isReverse) gravity = 9.81f;
        InvokeRepeating("Flying", Random.Range(0.3f, 0.6f), Random.Range(0.7f, 1.5f));
    }

    private void Update()
    {
        if (isFlyUp == true)
        {
            Debug.Log("Fly");
            if (isReverse)
            {
                direction = Vector3.down * strength;
            }
            else
            {
                direction = Vector3.up * strength;
            }

            isFlyUp = false;
        }

        // Apply gravity and update the position
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        // Tilt the bird based on the direction
        Vector3 rotation = transform.eulerAngles;
        rotation.z = direction.y * tilt;
        transform.eulerAngles = rotation;
    }

    void Flying()
    {
        
        if (isReverse)
        {
            direction = Vector3.down * strength;
        }
        else
        {
            direction = Vector3.up * strength;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Vector2 center = other.GetComponent<Collider2D>().bounds.center;
            Vector2 topCenter = new Vector2(center.x, GetComponent<Collider2D>().bounds.max.y);
            float distanceToTopCenter = Vector2.Distance(transform.position, topCenter);
            Debug.Log("Distance: " + distanceToTopCenter);
            if (distanceToTopCenter > 1.3f)
            {
                if (isReverse && other.GetComponent<PipeController>().isTop)
                    isFlyUp = true;
                else if (!isReverse && !other.GetComponent<PipeController>().isTop)
                    isFlyUp = true;
            }
        }
        else if (other.gameObject.CompareTag("Scoring"))
        {
        }
    }

    void SetActiveTrueFlyUp()
    {
        isFlyUp = true;
    }
}