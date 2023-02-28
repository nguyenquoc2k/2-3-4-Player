using System.Collections;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public GameObject[] listWall;
    private float timeToInstantiate;
    private GameObject currentWall;
    public GameObject firtWall;
    public PathFindingController pathFindingController;
    public Transform parentWall;
    private int previousIndex = -1;

    private void Awake()
    {
        currentWall = firtWall;
        timeToInstantiate = 5f;
    }

    private void Update()
    {
        timeToInstantiate -= Time.deltaTime;
        if (timeToInstantiate <= 0)
        {
            Destroy(currentWall);
            int randomIndex = GetRandomIndex();
            currentWall = Instantiate(listWall[randomIndex], parentWall);
            timeToInstantiate = 5f;
            pathFindingController.Scan();
        }
    }

    private int GetRandomIndex()
    {
        int randomIndex = Random.Range(0, listWall.Length);
        if (randomIndex == previousIndex)
        {
            randomIndex = (randomIndex + 1) % listWall.Length;
        }

        previousIndex = randomIndex;
        return randomIndex;
    }

    public void DestroyWall()
    {
        if (parentWall.childCount > 0) Destroy(parentWall.GetChild(0).gameObject);
    }
}