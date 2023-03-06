using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStickBall : MonoBehaviour
{
    public int indexBall = -1;
    [SerializeField] float force;

    Vector2 lastForce;

    [SerializeField] Rigidbody2D rb2;
    [SerializeField] Transform trail;

    private void FixedUpdate()
    {
        lastForce = rb2.velocity;

        if (Vector2.Distance(transform.position, Vector2.zero) > 25f)
        {
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Collision enter: " + Time.time);

        if (collision.transform.parent.CompareTag("Player"))
        {
            //AudioManagerStickBall.instance.Play(AudioManagerStickBall.instance.contact);
            if (MapModeStickBall.Instances.isCountdown == false)
            {
                if (collision.gameObject.GetComponent<PlayerStickBall>() != null)
                {
                    if (collision.gameObject.GetComponent<PlayerStickBall>().indexPlayer == 0)
                    {
                        GetComponent<SpriteRenderer>().color = Color.green;
                        indexBall = 0;
                    }
                    else if (collision.gameObject.GetComponent<PlayerStickBall>().indexPlayer == 1)
                    {
                        GetComponent<SpriteRenderer>().color = Color.red;
                        indexBall = 1;
                    }
                    else if (collision.gameObject.GetComponent<PlayerStickBall>().indexPlayer == 2)
                    {
                        GetComponent<SpriteRenderer>().color = Color.blue;
                        indexBall = 2;
                    }
                    else if (collision.gameObject.GetComponent<PlayerStickBall>().indexPlayer == 3)
                    {
                        GetComponent<SpriteRenderer>().color = Color.yellow;
                        indexBall = 3;
                    }
                }
                else if (collision.gameObject.GetComponent<AIStickBallMap1>() != null)
                {
                    if (collision.gameObject.GetComponent<AIStickBallMap1>().indexPlayer == 0)
                    {
                        GetComponent<SpriteRenderer>().color = Color.green;
                        indexBall = 0;
                    }
                    else if (collision.gameObject.GetComponent<AIStickBallMap1>().indexPlayer == 1)
                    {
                        GetComponent<SpriteRenderer>().color = Color.red;
                        indexBall = 1;
                    }
                    else if (collision.gameObject.GetComponent<AIStickBallMap1>().indexPlayer == 2)
                    {
                        GetComponent<SpriteRenderer>().color = Color.blue;
                        indexBall = 2;
                    }
                    else if (collision.gameObject.GetComponent<AIStickBallMap1>().indexPlayer == 3)
                    {
                        GetComponent<SpriteRenderer>().color = Color.yellow;
                        indexBall = 3;
                    }
                }
            }
        }

        rb2.velocity = Vector2.Reflect(lastForce, collision.contacts[0].normal).normalized * force;
    }
}