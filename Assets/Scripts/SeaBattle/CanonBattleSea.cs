using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CanonBattleSea : MonoBehaviour
{
    [SerializeField] float bombSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] Vector2 timeAim;
    [SerializeField] float afterCollisionWaterForce;
    [SerializeField] float afterCollisionWaterAngular;

    [SerializeField] List<Transform> boats = new List<Transform>();

    //[SerializeField] ParticleSystem fireBack;
    [SerializeField] Transform bombSpawnPos;

    [SerializeField] Transform cannonAxis;
    //[SerializeField] GameObject explodeEffect;

    [SerializeField] Rigidbody2D[] bombStore;
    [SerializeField] Transform cannon;
    [SerializeField] Vector3 cannonShakeTo;
    [SerializeField] private bool isLeft;
    [SerializeField] float rotation;

    void Awake()
    {
        StartCoroutine(AutoAim());
        rotation = isLeft ? 90f : -270f;
    }


    public void SuffleBoatList()
    {
        Transform temp;
        for (int i = 0; i < 10; i++)
        {
            int r = Random.Range(0, boats.Count);
            temp = boats[r];
            boats[r] = boats[0];
            boats[0] = temp;
        }
    }

   

    IEnumerator AutoAim()
    {
        yield return new WaitForSeconds(2);

        yield return new WaitForSeconds(Random.Range(0, 0.5f));
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(timeAim.x, timeAim.y));


            for (int i = 0; i < boats.Count; i++)
            {
                if (boats[i]!=null)
                {
                    yield return StartCoroutine(FireProcess(boats[i]));
                    break;
                }
            }

            
        }
    }

    IEnumerator FireProcess(Transform force)
    {
        //fireBack.Play();

        float fireDelayTime = 2;
        while (fireDelayTime > 0)
        {
            yield return new WaitForFixedUpdate();
            fireDelayTime -= Time.fixedDeltaTime;
            if (force == null)
            {
                SuffleBoatList();
                yield break; // exit the method if force is null
            }
            else
            {
                Vector2 direction = force.position - cannonAxis.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                cannonAxis.rotation = Quaternion.Lerp(Quaternion.Euler(new Vector3(0, 0, angle+rotation)),
                    cannonAxis.rotation,
                    rotationSpeed);
            }
            
        }

        //fireBack.Stop();

        Rigidbody2D boom = null;

        foreach (var b in bombStore)
        {
            if (!b.gameObject.activeSelf)
            {
                Debug.Log("name: " + b.name);
                boom = b;
                boom.transform.position = bombSpawnPos.position;
                boom.velocity = Vector2.zero;
                boom.gameObject.SetActive(true);
                b.transform.GetChild(0).gameObject.SetActive(true);

                break;
            }
        }


        if (boom != null)
        {
            //BattleSea.AudioManagerBattleSea.instance.Play(BattleSea.AudioManagerBattleSea.instance.shootBullet);

            Vector3 originalCannonPos = cannon.localPosition;
            DLocalMove(cannon, cannonShakeTo, 0.18f, DG.Tweening.Ease.OutCubic,
                () => { DLocalMove(cannon, originalCannonPos, 0.4f, DG.Tweening.Ease.OutSine); });
            DShakePosition(Camera.main.transform, 0.2f, 1, 30, 40);
            Vector2 forceAfterTween = ((Vector2)force.position - boom.position) * afterCollisionWaterForce;

            DMove(boom.transform, force.position, Vector2.Distance(force.position, boom.transform.position) * bombSpeed,
                DG.Tweening.Ease.Linear, () =>
                {
                    if (boom.gameObject.activeSelf)
                    {
                        //BattleSea.AudioManagerBattleSea.instance.Play(BattleSea.AudioManagerBattleSea.instance.hitWater);
                        //Instantiate(explodeEffect, boom.transform.position, Quaternion.identity);
                        boom.velocity = forceAfterTween;
                        boom.angularVelocity = afterCollisionWaterAngular;
                        boom.transform.GetChild(0).gameObject.SetActive(false);

                        StartCoroutine(DisableDelay(boom.gameObject, 8));
                    }
                });
        }
    }

    void DMove(Transform mtransform, Vector3 position, float duration, Ease eas, System.Action action = null)
    {
        mtransform.DOMove(position, duration).SetEase(eas).OnComplete(() => { action?.Invoke(); });
    }

    void DLocalMove(Transform mtransform, Vector3 position, float duration, Ease eas, System.Action action = null)
    {
        mtransform.DOLocalMove(position, duration).SetEase(eas).OnComplete(() => { action?.Invoke(); });
    }

    void DShakePosition(Transform mtranform, float duration, float strength, int vibrato, float randomNess,
        bool snapping = false, bool fadeout = true, System.Action action = null)
    {
        mtranform.DOShakePosition(duration, strength, vibrato, randomNess).SetEase(Ease.InOutSine);
    }

    IEnumerator DisableDelay(GameObject obj, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        if (obj.gameObject.activeSelf)
        {
            //BattleSea.AudioManagerBattleSea.instance.Play(BattleSea.AudioManagerBattleSea.instance.hitWater);
            obj.gameObject.SetActive(false);

            // Instantiate(explodeEffect, obj.transform.position, Quaternion.identity);
            DShakePosition(Camera.main.transform, 0.2f, 1, 30, 40);
        }
    }
}