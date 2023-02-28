using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowSeaBattleController : MonoBehaviour
{
    private bool canRotate = false;
    public GameObject BowString;
    private LineRenderer bowStringLineRenderer1;
    private LineRenderer bowStringLineRenderer2;
    public GameObject TopAnchor;
    public GameObject BottomAnchor;
    private bool drawingBow;
    private Vector3 bowStringStartPos;
    private float drawDistance = 0f;
    private float startAngle;
    private Vector3 topAnchorStartPos;
    public float MaxDrawDistance = 2f;
    public float DrawSpeed = 2f;
    public GameObject BowShaft;
    private GameObject currentArrow;
    private void Start()
    {
        // assign component references for fast use later
        bowStringLineRenderer1 = BowString.transform.GetChild(0).GetComponent<LineRenderer>();
        bowStringLineRenderer2 = BowString.transform.GetChild(1).GetComponent<LineRenderer>();
        RenderBowString(Vector3.zero);
        //startAngle = BowShaft.transform.eulerAngles.z;
        startAngle = BowShaft.transform.eulerAngles.z;
       
        
    }

    private void OnMouseDown()
    {
    
        if (gameObject.CompareTag("Player"))
        {
            canRotate = true;
        }
    }

    private void OnMouseUp()
    {
        canRotate = false;
    }

    private void Update()
    {
        if (canRotate)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector2 direction = new Vector2(mousePosition.x - transform.position.x,
                mousePosition.y - transform.position.y);
            transform.up = direction;
            if (Input.GetMouseButton(0))
            {
                BeginBowDraw();
            }
        }
        else
        {
            drawingBow = false;
            drawDistance = 0f;
            TopAnchor.transform.position = topAnchorStartPos;

            RenderBowString(Vector3.zero);
        }
    }

    private void BeginBowDraw()
    {
        //Vector3 pos = GetArrowPositionForDraw();
       // currentArrow.transform.position = pos;
       // drawingBow = true;
    }

    private void RenderBowString(Vector3 arrowPos)
    {
        Vector3 startPoint = TopAnchor.transform.position;
        Vector3 endPoint = BottomAnchor.transform.position;

        if (drawingBow)
        {
            bowStringLineRenderer2.gameObject.SetActive(true);
            bowStringLineRenderer1.SetPosition(0, startPoint);
            bowStringLineRenderer1.SetPosition(1, arrowPos);
            bowStringLineRenderer2.SetPosition(0, arrowPos);
            bowStringLineRenderer2.SetPosition(1, endPoint);
        }
        else
        {
            bowStringLineRenderer2.gameObject.SetActive(false);
            bowStringLineRenderer1.SetPosition(0, startPoint);
            bowStringLineRenderer1.SetPosition(1, endPoint);
        }
    }
}