using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ListWallStickmanRunController : MonoBehaviour
{
    public List<PrefabWallStickman> prefabs = new List<PrefabWallStickman>();
    public float spawnInterval = 230f;
    private int previousIndex = -1;
    [SerializeField] private Transform pos;
    [SerializeField] GameObject camera2D;
    private PrefabWallStickman prefabWallStickman;

    public void SpawnMap()
    {
        int currentIndex = Random.Range(0, prefabs.Count);
        if (currentIndex == previousIndex)
        {
            currentIndex = (currentIndex + 1) % prefabs.Count;
        }

        
        prefabWallStickman = Instantiate(prefabs[currentIndex], pos);
        prefabWallStickman.transform.position = new Vector3(prefabWallStickman.transform.position.x + spawnInterval,
            prefabWallStickman.transform.position.y,
            prefabWallStickman.transform.position.z);
        camera2D.GetComponent<CinemachineConfiner>().m_BoundingShape2D = prefabWallStickman.confinder;
        previousIndex = currentIndex;
        float intervalIncrement = 230;
        spawnInterval += intervalIncrement;
    }
}