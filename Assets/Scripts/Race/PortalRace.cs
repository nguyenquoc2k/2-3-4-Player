using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalRace : MonoBehaviour
{
    private Transform destination;
    public bool isDown;
    public float distance =0.2f;
    void Awake()
    {
        if (isDown==false)
        {
            destination = transform.parent.GetChild(1).GetComponent<Transform>();
        }
        else
        {
            destination = transform.parent.GetChild(0).GetComponent<Transform>();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (Vector2.Distance(transform.position, col.transform.position) > distance)
        {
            col.transform.position = new Vector2(destination.position.x, destination.position.y);
        }
    }
}
