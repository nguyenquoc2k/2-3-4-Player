using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingController : MonoBehaviour
{
    public AstarPath astarPath;
    void Awake()
    {
        Transform transform = GameObject.Find("PathFinding").transform;
        astarPath = transform.GetComponent<AstarPath>();
    }

    // Update is called once per frame
    public void Scan()
    {
        astarPath.Scan();
    }
}
