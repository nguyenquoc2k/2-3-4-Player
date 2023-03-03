using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BombBattleSea : MonoBehaviour
{
    [SerializeField] Vector3 shadowAddPos;
    //[SerializeField] Transform shadow;
    void LateUpdate()
    {
       // shadow.position = transform.position + shadowAddPos;
    }

    private void OnDisable()
    {
       DKill(transform);
    }
    void DKill(Transform mtransform)
    {
        mtransform.DOKill();
    }
}
