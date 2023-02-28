using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanController : MonoBehaviour
{
    public float speed = 6f;
    private Rigidbody2D rigidbody2D;
    private bool reverse = false;
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 40f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    private float waitTime = 1f;
    private float elapsedTime = 0f;
    private List<GameObject> playersToRemove;

    public ListWallStickmanRunController listWallStickmanRunController;

    //[SerializeField] private TrailRenderer tr;
    void Awake()
    {
        if (transform.name == "Player1") ReverseGravityController.ReversePlayer1 += ReverseStickman;
        else if (transform.name == "Player2") ReverseGravityController.ReversePlayer2 += ReverseStickman;
        else if (transform.name == "Player3") ReverseGravityController.ReversePlayer3 += ReverseStickman;
        else if (transform.name == "Player4") ReverseGravityController.ReversePlayer4 += ReverseStickman;
        playersToRemove = new List<GameObject>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void ReverseStickman(bool buttonState)
    {
        reverse = buttonState;
        if (reverse == true)
        {
            Vector3 angles = transform.eulerAngles;
            angles.y = 180;
            angles.z = 180;
            transform.eulerAngles = angles;
        }
        else
        {
            Vector3 angles = transform.eulerAngles;
            angles.y = 0;
            angles.z = 0;
            transform.eulerAngles = angles;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Dash")
        {
            StartCoroutine(Dash());
        }

        if (col.gameObject.tag == "Spawn")
        {
            listWallStickmanRunController.SpawnMap();
            Destroy(col.gameObject);
        }


        if (col.gameObject.tag == "Trap")
        {
            PlayerDeath();
        }
    }


    public void PlayerDeath()
    {
        foreach (var child in MapStickmanRunController.Instances.listPlayers)
        {
            if (child.name == transform.name)
                playersToRemove.Add(child.gameObject);
        }

        foreach (var player in playersToRemove)
        {
            MapStickmanRunController.Instances.listPlayers.Remove(player.transform);
        }

        CoreGameController.Instances.HandleShowResult(null,null);
        Destroy(gameObject);
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        elapsedTime += Time.deltaTime;
        if (elapsedTime >= waitTime)
        {
            Vector2 velocity = new Vector2(speed, rigidbody2D.velocity.y);
            rigidbody2D.velocity = velocity;
        }

        if (reverse == true)
        {
            rigidbody2D.gravityScale = -30f;
            LimitFallSpeed();
        }
        else
        {
            rigidbody2D.gravityScale = 30;
            LimitFallSpeed();
        }
    }

    public IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rigidbody2D.gravityScale;
        rigidbody2D.gravityScale = 0f;
        rigidbody2D.velocity = new Vector2(transform.localScale.x * dashingPower, rigidbody2D.velocity.y);
        //tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        // tr.emitting = false;
        rigidbody2D.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void LimitFallSpeed()
    {
        float maxFallSpeed = 0.5f;
        if (rigidbody2D.velocity.y < -maxFallSpeed)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -maxFallSpeed);
        }

        if (rigidbody2D.velocity.y > maxFallSpeed)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, maxFallSpeed);
        }
    }

    private void OnDestroy()
    {
        ReverseGravityController.ReversePlayer1 -= ReverseStickman;
        ReverseGravityController.ReversePlayer2 -= ReverseStickman;
        ReverseGravityController.ReversePlayer3 -= ReverseStickman;
        ReverseGravityController.ReversePlayer4 -= ReverseStickman;
    }
}