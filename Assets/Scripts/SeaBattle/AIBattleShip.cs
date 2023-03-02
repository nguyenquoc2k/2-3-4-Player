using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIBattleShip : MonoBehaviour
{
    [SerializeField] int myIndex;
    [SerializeField] float fireCountdown = 1f;
    [SerializeField] Rigidbody2D bullet;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] float bulletSpeed;
    [SerializeField] Transform towerAxis;
    [SerializeField] GameObject bulletDestroyEffect;

    [SerializeField] Animator anim;

    bool fireReady = true;
    bool dragTowerInLastFrame = false;

    /// <summary>
    /// //////////////////////////// AI //////////////////////////////
    /// </summary>
    [SerializeField] float rotationSpeed;

    [SerializeField] Vector2 timeAim;

    [SerializeField] List<Transform> shipsCanReach = new List<Transform>();

    private void Start()
    {
        StartCoroutine(FindShipAuto());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spawn"))
        {
            shipsCanReach.Add(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Spawn"))
        {
            shipsCanReach.Remove(other.transform);
        }
    }

    IEnumerator FindShipAuto()
    {
        yield return new WaitForSeconds(3);

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(timeAim.x, timeAim.y));

            //if (!ScoreManagerBattleSea.instance.isInGame) yield break;
            foreach (var ship in shipsCanReach)
            {
                if (ship.gameObject.activeSelf)
                {
                    yield return StartCoroutine(ShootProcess(ship));
                    break;
                }
            }
        }
    }

    IEnumerator ShootProcess(Transform target)
    {
        float timeLookAt = 2;
        while (true)
        {
            yield return null;

            timeLookAt -= Time.deltaTime;
            if (target == null)
            {
                if (shipsCanReach.Count > 0)
                {
                    int randomIndex = Random.Range(0, shipsCanReach.Count);
                    target = shipsCanReach[randomIndex];
                }
            }
            else
            {
                Vector2 directionToTarget =
                    (Vector2)towerAxis.position - ((Vector2)target.position + ((Vector2)target.right * 5));
                float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
                towerAxis.transform.rotation = Quaternion.Lerp(Quaternion.Euler(new Vector3(0, 0, angle)),
                    towerAxis.rotation, rotationSpeed);

                if (!target.gameObject.activeSelf)
                {
                    yield break;
                }

                if (timeLookAt < 0)
                {
                    Shoot();
                    yield break;
                }
            }
        }
    }

    void Shoot()
    {
        // if (!ScoreManagerBattleSea.instance.isInGame) return;
        // AudioManagerBattleSea.instance.Play(AudioManagerBattleSea.instance.shootBullet);
        bullet.transform.position = bulletSpawn.position;
        bullet.gameObject.SetActive(true);
        bullet.velocity = -towerAxis.right * bulletSpeed;

        fireReady = false;
        StartCoroutine(DisableBullet());
        StartCoroutine(ReloadBullet());


        dragTowerInLastFrame = false;
        anim.Play("BlueShoot");
    }

    IEnumerator DisableBullet()
    {
        yield return new WaitForSeconds(1.2f);

        anim.Play("BlueReload");

        if (bullet.gameObject.activeSelf)
        {
            //AudioManagerBattleSea.instance.Play(AudioManagerBattleSea.instance.hitWater);
            //Instantiate(bulletDestroyEffect, bullet.transform.position, Quaternion.identity);
            bullet.gameObject.SetActive(false);
        }
    }

    IEnumerator ReloadBullet()
    {
        yield return new WaitForSeconds(fireCountdown);
        fireReady = true;
    }
}