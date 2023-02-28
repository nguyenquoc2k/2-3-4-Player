using System;
using System.Collections;
using UnityEngine;

public class AIStickManController : MonoBehaviour
{
    private bool reverse = true;
    public float speed;
    public float defaulGravity;
    private float waitTime = 1f;
    private float elapsedTime = 0f;
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 40f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    private void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.name != transform.parent.name)
            {
                Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
        }
    }


    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        elapsedTime += Time.deltaTime;
        if (elapsedTime >= waitTime)
        {
            Vector2 velocity = new Vector2(speed, transform.parent.GetComponent<Rigidbody2D>().velocity.y);
            transform.parent.GetComponent<Rigidbody2D>().velocity = velocity;
        }

        LimitFallSpeed();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Player");
        }

        if (col.gameObject.tag == "Dash")
        {
            StartCoroutine(Dash());
        }
        
        StartCoroutine(HandleReverse(col));
    }
    

    public IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = transform.parent.GetComponent<Rigidbody2D>().gravityScale;
        transform.parent.GetComponent<Rigidbody2D>().gravityScale = 0f;
        transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * dashingPower,
            transform.parent.GetComponent<Rigidbody2D>().velocity.y);
        //tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        // tr.emitting = false;
        transform.parent.GetComponent<Rigidbody2D>().gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    IEnumerator HandleReverse(Collider2D col)
    {
        yield return new WaitForSeconds(0.2f);

        if (col.gameObject.tag == "Enviroment")
        {
            if (reverse == true)
            {
                transform.parent.GetComponent<Rigidbody2D>().gravityScale = -defaulGravity;
                Vector3 angles = transform.eulerAngles;
                angles.y = 180;
                angles.z = 180;
                transform.eulerAngles = angles;
                reverse = false;
            }
            else
            {
                transform.parent.GetComponent<Rigidbody2D>().gravityScale = defaulGravity;
                Vector3 angles = transform.eulerAngles;
                angles.y = 0;
                angles.z = 0;
                transform.eulerAngles = angles;
                reverse = true;
            }
        }
    }


    private void LimitFallSpeed()
    {
        float maxFallSpeed = 0.5f;
        if (transform.parent.GetComponent<Rigidbody2D>().velocity.y < -maxFallSpeed)
        {
            transform.parent.GetComponent<Rigidbody2D>().velocity =
                new Vector2(transform.parent.GetComponent<Rigidbody2D>().velocity.x, -maxFallSpeed);
        }

        if (transform.parent.GetComponent<Rigidbody2D>().velocity.y > maxFallSpeed)
        {
            transform.parent.GetComponent<Rigidbody2D>().velocity =
                new Vector2(transform.parent.GetComponent<Rigidbody2D>().velocity.x, maxFallSpeed);
        }
    }
}