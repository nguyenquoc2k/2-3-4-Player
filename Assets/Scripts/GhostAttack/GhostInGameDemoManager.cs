using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostInGameDemoManager : MonoBehaviour
{
    public List<Transform> listPlayer = new List<Transform>();
    private Transform targetObject;

    public float radius = 10f;
    private float closestDistance = float.MaxValue;

    private Collider2D[] colliders;
    private void Awake()
    {
        listPlayer.Clear();

    }

    private void Update()
    {
        FindClosestObject();
        MoveToTarget();
    }

    private void FindClosestObject()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        closestDistance = float.MaxValue;
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.tag == "Player")
            {
                float distance = Vector2.Distance(collider.transform.position, transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    targetObject = collider.transform;
                    listPlayer.Clear(); 
                    AddPlayer(targetObject);
                }
            }
        }

        if (listPlayer.Count > 0)
        {
            targetObject = listPlayer[0];
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            listPlayer.Remove(other.transform);
            Destroy(other.gameObject);
            GameDemoManager.Instances.InitGhostAfterPlayerDie(other.name,other.transform.position);
            colliders = Physics2D.OverlapCircleAll(transform.position, radius);
            FindClosestObject();
        }
    }
    private void MoveToTarget()
    {
        if (targetObject == null)
        {
            return;
        }
        transform.position = Vector2.MoveTowards(transform.position, targetObject.position, Time.deltaTime);
        Vector2 lookDir = targetObject.transform.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        GetComponent<Rigidbody2D>().rotation = angle;
    }

    public void AddPlayer(Transform player)
    {
        if (!listPlayer.Contains(player))
        {
            listPlayer.Add(player);
        }
    }
}
