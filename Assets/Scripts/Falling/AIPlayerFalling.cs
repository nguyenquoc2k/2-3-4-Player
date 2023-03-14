using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIPlayerFalling : MonoBehaviour
{
    public int indexPlayer = 0;
    public float speed;
    private Rigidbody2D rb;
    public bool isRandomDir;
    public RandomColorFalling randomColorFalling;
    public GridManager gridManager;
    private Vector2 currentPos;
    private Vector2 prevPos;
    private bool isMoving;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        SetRandomDirection();
        StartCoroutine(CheckIfMoving());
        RandomColorFalling.RandomTile += SetFalseRandomTile;
    }

    private void OnDestroy()
    {
        RandomColorFalling.RandomTile -= SetFalseRandomTile;
    }

    private void SetFalseRandomTile()
    {
        hasRandomTile = false;
    }

    void FixedUpdate()
    {
        CheckAndRemovePlayer();
        if (randomColorFalling.isSetColor == false)
        {
            Move();
            Rotate();
        }
        else if (hasRandomTile == false) // call RandomTile() only if no random tile has been selected yet
        {
            RandomTile();
        }
        else
        {
           FindGround();
        }
    }

    private Collider2D[] previousHitColliders;

    private void CheckAndRemovePlayer()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        bool hasObstacle = false;
        bool hasHole = false;
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Obstacle"))
            {
                hasObstacle = true;
            }
            else if (hitCollider.CompareTag("Hole"))
            {
                hasHole = true;
            }
        }

        if (hasHole && !hasObstacle)
        {
            MapFallingController.Instances.CheckPlayerDeath(indexPlayer);
            Destroy(gameObject);
        }

        // Lưu danh sách hitColliders để sử dụng so sánh lần tiếp theo
        previousHitColliders = hitColliders;
        // Draw the circle in the Scene view
        float radius = 0.3f;
        int segments = 32;
        float angle = 0f;
        float angleStep = (2f * Mathf.PI) / segments;
        for (int i = 0; i < segments; i++)
        {
            Vector3 pos1 = transform.position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f);
            angle += angleStep;
            Vector3 pos2 = transform.position + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f);
            Debug.DrawLine(pos1, pos2, Color.green);
        }
    }
    IEnumerator CheckIfMoving()
    {
        while (true)
        {
            // Lưu giữ vị trí hiện tại của bot
            currentPos = transform.position;

            // Kiểm tra nếu bot đang đứng yên
            if (currentPos == prevPos)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }

            // Lưu giữ vị trí hiện tại của bot cho việc so sánh với vị trí tiếp theo
            prevPos = currentPos;

            // Chờ trong 0.5 giây
            yield return new WaitForSeconds(0.5f);

            // Kiểm tra nếu bot đang đứng yên trong 0.5 giây và chưa được gọi hàm SetRandomDirection
            if (!isMoving)
            {
                SetRandomDirection();
            }
        }
    }

    private void FindGround()
    {
        Vector2 direction = transform.position - randomTile.transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        transform.position =
            Vector2.MoveTowards(this.transform.position, randomTile.transform.position, 2 * Time.deltaTime);
        Debug.Log(randomTile.transform.position);
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle));
        //randomColorFalling.isSetColor = false;
    }

    private bool hasRandomTile = false;
    private Tile randomTile;

    private void RandomTile()
    {
        hasRandomTile = false; // reset hasRandomTile
        List<Tile> tilesToMove = new List<Tile>();

        foreach (var tile in gridManager._tiles.Values)
        {
            if (tile.GetComponent<Tile>().idTile == randomColorFalling.idColor)
            {
                tilesToMove.Add(tile.GetComponent<Tile>());
            }
        }

        if (tilesToMove.Count > 0) // check if there are any tiles with the same color as the falling object
        {
            int randomIndex = Random.Range(0, tilesToMove.Count);
            randomTile = tilesToMove[randomIndex];
            Debug.Log(randomTile.name);
            hasRandomTile = true; // set hasRandomTile to true after selecting a random tile
        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.tag == "Player" || col.gameObject.tag == "Enviroment") &&
            randomColorFalling.isSetColor == false)
        {
            SetRandomDirection();
        }
    }

    void SetRandomDirection()
    {
        float angle = Mathf.Atan2(-transform.position.y, -transform.position.x) * Mathf.Rad2Deg;
        angle += Random.Range(-60f, 60f);
        Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        rb.velocity = direction * speed;
    }

    void Move()
    {
        rb.MovePosition(rb.position + rb.velocity * Time.fixedDeltaTime);
    }

    void Rotate()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}