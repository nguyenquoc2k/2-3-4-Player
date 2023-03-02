using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowSeaBattleController : MonoBehaviour
{
    float fireCountdown = 2f;
    private float rotationTime = 0.6f;
    [SerializeField] FixedJoystick Joystick;
    Rigidbody2D bullet;
    [SerializeField] Transform bulletSpawn;
    float bulletSpeed = 10;
    [SerializeField] Transform towerAxis;
    [SerializeField] GameObject bulletDestroyEffect;

    [SerializeField] Animator anim;

    bool fireReady = true;
    bool dragTowerInLastFrame = false;


    private void Awake()
    {
        bulletSpawn = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(3);
        towerAxis = transform.GetChild(0).GetChild(0);
        anim = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Animator>();
        

        Transform parentTransform = UIInGameController.Instances.transform;
        if (transform.name == "Player1")
        {
            Joystick = parentTransform.GetChild(0).GetComponent<FixedJoystick>();
            HandleJoystick();
            parentTransform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector3(230, 265, 10);
            bullet = FindObjectOfType<BulletHolderSeaBattle>().transform.GetChild(0).GetComponent<Rigidbody2D>();
        }
        else if (transform.name == "Player2")
        {
            Joystick = parentTransform.GetChild(1).GetComponent<FixedJoystick>();
            HandleJoystick();
            parentTransform.GetChild(1).GetComponent<RectTransform>().anchoredPosition = new Vector3(230, -215, 10);
            bullet = FindObjectOfType<BulletHolderSeaBattle>().transform.GetChild(1).GetComponent<Rigidbody2D>();
        }
        else if (transform.name == "Player3")
        {
            Joystick = parentTransform.GetChild(2).GetComponent<FixedJoystick>();
            HandleJoystick();
            parentTransform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector3(-240, -215, 10);
            bullet = FindObjectOfType<BulletHolderSeaBattle>().transform.GetChild(2).GetComponent<Rigidbody2D>();
        }
        else if (transform.name == "Player4")
        {
            Joystick = parentTransform.GetChild(3).GetComponent<FixedJoystick>();
            HandleJoystick();
            parentTransform.GetChild(3).GetComponent<RectTransform>().anchoredPosition = new Vector3(-240, 265, 10);
            bullet = FindObjectOfType<BulletHolderSeaBattle>().transform.GetChild(3).GetComponent<Rigidbody2D>();
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