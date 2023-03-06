using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class CannonStickBall : MonoBehaviour
{
    [SerializeField] int maxBallNumber;
    [SerializeField] float force;
    [SerializeField] Transform[] spawnBulletPos;
    [SerializeField] GameObject bullet;


    public Transform bulletHolder;

    public static CannonStickBall instance;

    private void Awake()
    {
        instance = this;
        StartCoroutine(DropBallAuto());
    }
    

    public int GetBulletNumber()
    {
        if (Object.ReferenceEquals(bulletHolder, null))
        {
            return 0;
        }

        return bulletHolder.childCount;
        //return bulletHolder.childCount;
    }


    IEnumerator DropBallAuto()
    {
        yield return new WaitForSeconds(3.5f);

        while (true)
        {
            if (GetBulletNumber() < maxBallNumber)
            {
                DropABullet();
            }

            yield return new WaitForSeconds(Random.Range(1f, 5f));
        }
    }


    public void DropABullet()
    {
        //AudioManagerStickBall.instance.Play(AudioManagerStickBall.instance.ballSpawn);
        int r = Random.Range(0, spawnBulletPos.Length);

        GameObject b = Instantiate(bullet, spawnBulletPos[r].position, Quaternion.identity, bulletHolder);
        b.GetComponent<BallStickBall>().indexBall = -1;
        b.GetComponent<Rigidbody2D>().velocity = spawnBulletPos[r].up * force;
    }
}