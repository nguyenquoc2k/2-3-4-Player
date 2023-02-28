using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPools : MonoBehaviour
{
    public static ObjectPools Instances;
    private List<GameObject> pooledObjects = new List<GameObject>();
    private int amountToPool = 10;
    [SerializeField] private GameObject bulletPrefab;
    private Vector2 Direction;
    private void Awake()
    {
        if (Instances == null)
            Instances = this;
    }

    private void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject( )
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}