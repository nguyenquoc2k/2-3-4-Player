using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public float rotationSpeed = 100f;
    private bool reverse = false;
    private Rigidbody2D rb;
    private float moveSpeed = 400f;
    public Vector2 moveDirection;
    public bool isMoving = false;
    public Transform bulletPos;
    [SerializeField] public GameObject bulletPrefab, flag;
    public float bulletForce = 10;
    public AmmoTankBattleController ammoController;

    [SerializeField] public Faction faction;

    public enum Faction
    {
        Ally,
        Enemy,
    };

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (transform.name == "Player1" || transform.name == "Player1(Clone)")
            ReverseGravityController.ReversePlayer1 += ReverseTank;
        else if (transform.name == "Player2" || transform.name == "Player2(Clone)")
            ReverseGravityController.ReversePlayer2 += ReverseTank;
        else if (transform.name == "Player3" || transform.name == "Player3(Clone)")
            ReverseGravityController.ReversePlayer3 += ReverseTank;
        else if (transform.name == "Player4" || transform.name == "Player4(Clone)")
            ReverseGravityController.ReversePlayer4 += ReverseTank;
    }

    private void OnDestroy()
    {
        ReverseGravityController.ReversePlayer1 -= ReverseTank;
        ReverseGravityController.ReversePlayer2 -= ReverseTank;
        ReverseGravityController.ReversePlayer3 -= ReverseTank;
        ReverseGravityController.ReversePlayer4 -= ReverseTank;
    }

    public void SetSpeedToZero()
    {
        rb.drag = 70000;
    }

    public void ReverseTank(bool buttonState)
    {
        reverse = buttonState;
        if (reverse == true)
        {
            isMoving = true;
            rotationSpeed *= -1;
            Fire();
            rb.drag = 0;
        }
        else if (reverse == false)
        {
            isMoving = false;
            rb.drag = 70;
        }
    }


    void Fire()
    {
        HandleAmmo();
    }

    void Update()
    {
        if (isMoving == false)
        {
            moveDirection = Vector2.zero;
            rb.velocity = moveDirection * moveSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
        else
        {
            moveDirection = transform.up;
            rb.velocity = moveDirection * moveSpeed * Time.deltaTime;
        }
    }

    private void HandleAmmo()
    {
        if (ammoController.currentBullets > 0)
        {
            // Giảm số lượng đạn hiện có của người chơi đi 1 và bắn súng
            ammoController.currentBullets--;
            ammoController.DispatchNumberAmmo();
            SpawnBullet();
        }
        else if (ammoController.currentBullets <= 0 && !ammoController.reloading) // Nếu hết đạn và không đang nạp đạn
        {
            ammoController.StartReloading(); // Bắt đầu quá trình nạp đạn
        }
    }

    void SpawnBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletPos.position, bulletPos.rotation);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        // Gán giá trị Faction từ trường dữ liệu public vào BulletController
        bulletController.SetFaction(faction);
        bulletController.SetBulletOwnerName(transform.name);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bulletPos.up * bulletForce, ForceMode2D.Impulse);
        Destroy(bullet, 4f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (ToggleGroupController.Instances.gameMode2.isOn == true)
        {
            FlagTankBattle flagController = col.GetComponent<FlagTankBattle>();
            RoundTankBattle roundTankBattle = col.GetComponent<RoundTankBattle>();
            if (flagController != null)
            {
                Faction colFactionFlag = flagController.flagFaction;
                if (colFactionFlag != faction)
                {
                    flagController.gameObject.SetActive(false);
                    flag.gameObject.SetActive(true);
                }
            }
            else if (roundTankBattle != null)
            {
                Faction colFactionRound = roundTankBattle.flagFactionRound;
                if (colFactionRound == faction && flag.gameObject.activeSelf == true)
                {
                    if (faction == Faction.Ally)
                    {
                        CoreGameController.Instances.HandleShowResult(
                            TankBattleMapController.Instances.player1.transform.name,
                            TankBattleMapController.Instances.player2.transform.name);
                    }
                    else
                    {
                        CoreGameController.Instances.HandleShowResult(
                            TankBattleMapController.Instances.player3.transform.name,
                            TankBattleMapController.Instances.player4.transform.name);
                    }
                }
            }
        }
    }
}