using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRaceLayerHandle : MonoBehaviour
{
    public SpriteRenderer carOutlineSpriteRenderer;

    public List<SpriteRenderer> defaultLayerSpriteRenderers = new List<SpriteRenderer>();

    public List<Collider2D> overpassColliderList = new List<Collider2D>();
    public List<Collider2D> underpassColliderList = new List<Collider2D>();

    Collider2D carCollider;

    //State
    bool isDrivingOnOverpass = false;

    void Awake()
    {
        overpassColliderList.Clear();
        underpassColliderList.Clear();
        UpdateSortingAndCollisionLayers();
        foreach (SpriteRenderer spriteRenderer in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            if (spriteRenderer.sortingLayerName == "Default")
                defaultLayerSpriteRenderers.Add(spriteRenderer);
        }
        Invoke("AddColliderToList", 0.1f);
        carCollider = GetComponentInChildren<Collider2D>();
    }

    void AddColliderToList()
    {
        foreach (GameObject overpassColliderGameObject in GameObject.FindGameObjectsWithTag("OverpassCollider"))
        {
            overpassColliderList.Add(overpassColliderGameObject.GetComponent<Collider2D>());
        }

        foreach (GameObject underpassColliderGameObject in GameObject.FindGameObjectsWithTag("UnderpassCollider"))
        {
            underpassColliderList.Add(underpassColliderGameObject.GetComponent<Collider2D>());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }


    void UpdateSortingAndCollisionLayers()
    {
        if (isDrivingOnOverpass)
        {
            SetSortingLayer("RaceTrackOverpass");

            carOutlineSpriteRenderer.enabled = false;
        }
        else
        {
            SetSortingLayer("Default");

            carOutlineSpriteRenderer.enabled = true;
        }

        SetCollisionWithOverPass();
    }


    void SetCollisionWithOverPass()
    {
        foreach (Collider2D collider2D in overpassColliderList)
        {
            Physics2D.IgnoreCollision(carCollider, collider2D, !isDrivingOnOverpass);
        }

        foreach (Collider2D collider2D in underpassColliderList)
        {
            if (isDrivingOnOverpass)
                Physics2D.IgnoreCollision(carCollider, collider2D, true);
            else Physics2D.IgnoreCollision(carCollider, collider2D, false);
        }
    }

    void SetSortingLayer(string layerName)
    {
        foreach (SpriteRenderer spriteRenderer in defaultLayerSpriteRenderers)
        {
            spriteRenderer.sortingLayerName = layerName;
        }
    }

    public bool IsDrivingOnOverpass()
    {
        return isDrivingOnOverpass;
    }

    void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (collider2d.CompareTag("UnderpassTrigger"))
        {
            isDrivingOnOverpass = false;

            UpdateSortingAndCollisionLayers();
        }
        else if (collider2d.CompareTag("OverpassTrigger"))
        {
            isDrivingOnOverpass = true;

            UpdateSortingAndCollisionLayers();
        }
    }
}