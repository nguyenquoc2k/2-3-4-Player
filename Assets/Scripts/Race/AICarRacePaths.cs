using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarRacePaths : MonoBehaviour
{
    public List<Transform> points = new List<Transform>() {  };
    public float speed = 5.0f; // tốc độ di chuyển của xe
    public int loopCount = 3; // số lần xe sẽ di chuyển trên chuỗi điểm
    public float driftFactor = 0.95f; 
    private int currentPoint = 0; // vị trí của điểm đang được di chuyển đến
    private int currentLoop = 0; // số lần xe đã di chuyển trên chuỗi điểm
    public Transform transformHandler;
    private Rigidbody2D rb;
    // Update is called once per frame
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Portal"))
        {
            Invoke("RemovePoint",0.1f);
        }
    }

    void RemovePoint()
    {
        points.RemoveAt(10);
    }
    void Update()
    {
        if (currentLoop >= loopCount) return; // đã di chuyển đủ số lần trên chuỗi điểm

        // tính toán khoảng cách giữa xe và điểm tiếp theo
        float distanceToNextPoint = Vector3.Distance(transform.position, points[currentPoint].position);

        // nếu xe đã đến gần điểm tiếp theo, chuyển sang điểm đó
        if (distanceToNextPoint < 0.1f)
        {
            currentPoint++;

            // kiểm tra nếu đã đi đến điểm cuối cùng của chuỗi điểm
            if (currentPoint >= points.Count)
            {
                currentPoint = 0; // đặt lại vị trí hiện tại về điểm đầu tiên
                points.Insert(10,transformHandler);
                currentLoop++; // tăng biến đếm số lần di chuyển trên chuỗi điểm lên 1
            }
        }

        // di chuyển xe đến điểm tiếp theo
        transform.position =
            Vector3.MoveTowards(transform.position, points[currentPoint].position, speed * Time.deltaTime);
        // xoay đầu xe hướng về điểm tiếp theo
        Vector3 direction = points[currentPoint].position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));
    }
}