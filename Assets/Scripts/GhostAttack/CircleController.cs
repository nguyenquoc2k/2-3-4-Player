using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleController : MonoBehaviour
{
    public static bool PointerDown = false;
    public FixedJoystick Joystick;
    private Rigidbody2D rb;
    private Vector2 move;
    public float moveSpeed = 200f;

    void Awake()
    {
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
    }

    public void AddJoystick()
    { 
        Transform parentTransform = UIInGameController.Instances.transform;
        if (transform.name == "GhostP1(Clone)")
        {
            Joystick = parentTransform.GetChild(0).GetComponent<FixedJoystick>();
        }
        else if (transform.name == "GhostP2(Clone)")
        {
            Joystick = parentTransform.GetChild(1).GetComponent<FixedJoystick>();
        }
        else if (transform.name == "GhostP3(Clone)")
        {
            Joystick = parentTransform.GetChild(2).GetComponent<FixedJoystick>();
        }
        else if (transform.name == "GhostP4(Clone)")
        {
            Joystick = parentTransform.GetChild(3).GetComponent<FixedJoystick>();
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        move.x = Joystick.Horizontal;
        move.y = Joystick.Vertical;
        float hAxis = move.x;
        float vAxis = move.y;
        moveDir = new Vector2(hAxis, vAxis);
    }
    private Vector2 moveDir;
    private void FixedUpdate()
    {
        rb.velocity = moveDir * moveSpeed * Time.fixedDeltaTime;
        if (rb.velocity != Vector2.zero)
        {
            transform.up = rb.velocity;
        }
    }
}