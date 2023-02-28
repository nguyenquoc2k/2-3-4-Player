using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GKSoccerController : MonoBehaviour
{
    public GameObject point1;
    public GameObject point2;
    private Rigidbody2D rb;
    public float moveSpeed = 3f; // tốc độ di chuyển của player
    private Transform playerTransform;
    private Transform targetBallTransform;
    public Transform ballHolder;
    private bool isMoveingToCenter = false;
    public static bool isGKKeeping = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = transform;
        isGKKeeping = false;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            targetBallTransform = col.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            targetBallTransform = null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            isGKKeeping = true;
            SoccerPlayerController player = collision.transform.parent.parent.GetComponent<SoccerPlayerController>();
            if (player != null && player != this)
            {
                player.ballHolder.transform.GetChild(0).parent = ballHolder;
            }

            BallController.Instances.isShooting = false;
            collision.transform.GetComponent<BallController>().SetSpeedBall();
            collision.transform.SetParent(ballHolder);
            collision.transform.localPosition = Vector3.zero;
            collision.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            collision.transform.GetComponent<CircleCollider2D>().isTrigger = true;  
            float angle = transform.rotation.eulerAngles.z;

            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            StartCoroutine(FireBallAndWait(direction));
        }
    }

    
        

    IEnumerator FireBallAndWait(Vector2 direction)
    {
        yield return StartCoroutine(BallController.Instances.GKFireBall(direction));
        isMoveingToCenter = true;
        isGKKeeping = false;
    }

    void Update()
    {
        if (targetBallTransform != null && BallController.Instances.isShooting == true)
        {
            // Tính toán vị trí đích của player theo trục y
            float targetY = Mathf.Clamp(
                targetBallTransform.position.y,
                Mathf.Min(point1.transform.position.y, point2.transform.position.y),
                Mathf.Max(point1.transform.position.y, point2.transform.position.y)
            );

            // Di chuyển player đến vị trí đích theo trục y
            Vector3 targetPosition = new Vector3(
                playerTransform.position.x,
                targetY,
                playerTransform.position.z
            );
            playerTransform.position = Vector3.MoveTowards(
                playerTransform.position,
                targetPosition,
                moveSpeed * Time.deltaTime
            );
        }
        else if (isMoveingToCenter == true)
        {
            // Tính toán vị trí trung tâm trục y giữa point1 và point2
            float centerY = (point1.transform.position.y + point2.transform.position.y) / 2;

            // Đặt vị trí đích của player
            Vector3 targetPosition = new Vector3(playerTransform.position.x, centerY, playerTransform.position.z);

            // Di chuyển player đến vị trí đích theo trục y
            playerTransform.position =
                Vector3.MoveTowards(playerTransform.position, targetPosition, 2 * Time.deltaTime);
            if (playerTransform.position == targetPosition)
                isMoveingToCenter = false;
        }
    }
}