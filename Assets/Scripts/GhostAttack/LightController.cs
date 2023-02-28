using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LightController : MonoBehaviour
{
    public Light2D light2D;
    public float duration = 0.4f;

    private void Awake()
    {
        light2D = GetComponent<Light2D>();
        StartCoroutine(Decrease());
    }

    private IEnumerator Decrease()
    {
        yield return new WaitForSeconds(1f);
        float currentTime = 0f;
        float startingIntensity = light2D.intensity;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            light2D.intensity = Mathf.Lerp(startingIntensity, 0f, currentTime / duration);
            yield return null;
        }
    }
    
}