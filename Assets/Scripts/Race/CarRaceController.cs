using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRaceController : MonoBehaviour
{
    public FixedJoystick Joystick;
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 4;
    private float dashingTime = 0.3f;
    private float moveSpeed = 4.5f;
    private float dashingCooldown = 1f;

    public RaceController raceController;

    //Laps
    public Collider2D[] checkPoints;
    private int currentCheckpoint = 0;
    private int lapCounter = 0;

    public static event Action<string, int> LapsCounting;

    //Components
    Rigidbody2D rb;

    void Awake()
    {
        RaceController.isDone3Lap = false;
        rb = GetComponent<Rigidbody2D>();
        Transform parentTransform = UIInGameController.Instances.transform;
        if (transform.name == "Player1")
        {
            Joystick = parentTransform.GetChild(0).GetComponent<FixedJoystick>();
        }
        else if (transform.name == "Player2")
        {
            Joystick = parentTransform.GetChild(1).GetComponent<FixedJoystick>();
        }
        else if (transform.name == "Player3")
        {
            Joystick = parentTransform.GetChild(2).GetComponent<FixedJoystick>();
        }
        else if (transform.name == "Player4")
        {
            Joystick = parentTransform.GetChild(3).GetComponent<FixedJoystick>();
        }

        LapsCounting?.Invoke(transform.name, lapCounter);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Dash")
        {
            StartCoroutine(Dash(col));
        }

        if (checkPoints.Length < 1) return;
        if (col == checkPoints[currentCheckpoint])
        {
            Debug.Log(currentCheckpoint);
            currentCheckpoint++;
        }

        if (currentCheckpoint == checkPoints.Length)
        {
            lapCounter++;
            LapsCounting?.Invoke(transform.name, lapCounter);
            if (raceController.classicMode == true)
            {
            }
            else if (raceController.knockoutMode == true)
            {
                raceController.HandleListCar(transform.name);
            }

            currentCheckpoint = 0;

            if (lapCounter == 3)
            {
                if (RaceController.isDone3Lap == false)
                {
                    CoreGameController.Instances.HandleShowResult(transform.name,null);
                    RaceController.isDone3Lap = true;
                }
            }
        }
    }

    public IEnumerator Dash(Collider2D other)
    {
        canDash = false;
        isDashing = true;
        moveSpeed = 10;
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        moveSpeed = 4.5f;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        move.x = Joystick.Horizontal;
        move.y = Joystick.Vertical;
        float hAxis = move.x;
        float vAxis = move.y;
        moveDir = new Vector2(hAxis, vAxis);
        KillOrthogonalVelocity();
    }

    private Vector2 moveDir;
    private Vector2 move;
    public float accelerationTime = 1f; // Thời gian tăng tốc từ 0 đến tốc độ tối đa
    public float decelerationTime = 1f; // Thời gian giảm tốc từ tốc độ tối đa về 0
    private Vector2 currentVelocity; // Tốc độ hiện tại của xe hơi

    void FixedUpdate()
    {
        if (isDashing)
        {
            currentVelocity = Vector2.Lerp(currentVelocity, moveDir * moveSpeed, Time.deltaTime / 0.01f);
            if (moveDir == Vector2.zero)
            {
                currentVelocity = Vector2.Lerp(currentVelocity, Vector2.zero, Time.deltaTime / 0.01f);
            }
        }
        else
        {
            currentVelocity = Vector2.Lerp(currentVelocity, moveDir * moveSpeed, Time.deltaTime / accelerationTime);
            if (moveDir == Vector2.zero)
            {
                currentVelocity = Vector2.Lerp(currentVelocity, Vector2.zero, Time.deltaTime / decelerationTime);
            }
        }

        rb.velocity = currentVelocity;
        if (rb.velocity != Vector2.zero)
        {
            transform.up = rb.velocity;
        }
    }


    void KillOrthogonalVelocity()
    {
        // Get forward, right, and up velocity of the car
        Vector3 forwardVelocity = transform.forward * Vector3.Dot(rb.velocity, transform.forward);
        Vector3 rightVelocity = transform.right * Vector3.Dot(rb.velocity, transform.right);
        Vector3 upVelocity = transform.up * Vector3.Dot(rb.velocity, transform.up);

        // Kill the orthogonal velocity (side velocity) based on how much the car should drift. 
        rb.velocity = forwardVelocity + rightVelocity * 0.95f + upVelocity;
    }
}