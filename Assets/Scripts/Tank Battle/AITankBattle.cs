using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using Random = UnityEngine.Random;

public class AITankBattle : MonoBehaviour
{
    public TankController tankController;
    private bool reverse = false;
    private Coroutine reverseCoroutine;

    void Awake()
    {
        StartCoroutine(HandleReverse());
    }

    private void Update()
    {
    }

    private IEnumerator HandleReverse()
    {
        float time = Random.Range(0.5f, 4f);
        yield return new WaitForSeconds(time);
        reverse = !reverse;
        tankController.ReverseTank(reverse);
        StartCoroutine(HandleReverse());
    }
    
}