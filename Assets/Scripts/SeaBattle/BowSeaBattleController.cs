using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowSeaBattleController : MonoBehaviour
{
    [SerializeField] float fireCountdown = 1f;
    [SerializeField] float rotationTime;
    [SerializeField] FixedJoystick Joystick;
    [SerializeField] Rigidbody2D bullet;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] float bulletSpeed;
    [SerializeField] Transform towerAxis;
    [SerializeField] GameObject bulletDestroyEffect;

    [SerializeField] Animator anim;

    bool fireReady = true;
    bool dragTowerInLastFrame = false;


    private void Awake()
    {
        Transform parentTransform = UIInGameController.Instances.transform;
        if (transform.name == "Player1")
        {
            Joystick = parentTransform.GetChild(0).GetComponent<FixedJoystick>();
            HandleJoystick();
            parentTransform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(490, 265, 10);
        }
        else if (transform.name == "Player2")
        {
            Joystick = parentTransform.GetChild(1).GetComponent<FixedJoystick>();
            HandleJoystick();
        }
        else if (transform.name == "Player3")
        {
            Joystick = parentTransform.GetChild(2).GetComponent<FixedJoystick>();
            HandleJoystick();
        }
        else if (transform.name == "Player4")
        {
            Joystick = parentTransform.GetChild(3).GetComponent<FixedJoystick>();
            HandleJoystick();
        }
    }

    void HandleJoystick()
    {
        Joystick.gameObject.SetActive(true);
        Joystick.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }

    private void Update()
    {
        if (Joystick.Direction.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(Joystick.Direction.y, Joystick.Direction.x) * Mathf.Rad2Deg;
            towerAxis.rotation = Quaternion.Lerp(Quaternion.Euler(new Vector3(0, 0, angle)), towerAxis.rotation,
                rotationTime);

            if (!dragTowerInLastFrame && fireReady)
            {
                //AudioManagerBattleSea.instance.Play(AudioManagerBattleSea.instance.pullString);
                dragTowerInLastFrame = true;
                anim.Play("BlueReload");
            }
        }
        else
        {
            if (dragTowerInLastFrame && fireReady) //shoot
            {
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
        }
    }

    IEnumerator DisableBullet()
    {
        yield return new WaitForSeconds(1.2f);

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