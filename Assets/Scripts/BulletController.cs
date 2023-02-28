using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class BulletController : MonoBehaviour
{
    private float speed = 10f;
    private Vector3 shootDir;
    private TankController.Faction factionBullet;
    public string nameOwnerBullet;
    public static event Action IncreaseScorePlayer1, IncreaseScorePlayer2, IncreaseScorePlayer3, IncreaseScorePlayer4;

    private void FixedUpdate()
    {
        transform.position += shootDir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        HandleTrigger(col);
        HandleDestroyObject(col);
    }


    private void HandleDestroyObject(Collider2D col)
    {
        if (col.gameObject.CompareTag("Portal"))
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }

    public void SetBulletOwnerName(string nameOwner)
    {
        this.nameOwnerBullet = nameOwner;
    }

    public void SetFaction(TankController.Faction faction)
    {
        factionBullet = faction;
    }

    void HandleTrigger(Collider2D col)
    {
        if (ToggleGroupController.Instances.gameMode2.isOn == true)
        {
            TankController tankController = col.GetComponent<TankController>();
            if (tankController != null)
            {
                TankController.Faction colFaction = tankController.faction;
                if (colFaction == factionBullet)
                {
                    return;
                }
            }
        }

        if (col.gameObject.CompareTag("Shield"))
        {
            if (col.transform.parent.name != nameOwnerBullet)
            {
                Destroy(gameObject);
                Debug.Log("Shield");
            }
        }
        else if (col.CompareTag("Player") && col.transform.GetChild(1).gameObject.activeSelf == true &&
                 col.transform.name != nameOwnerBullet)
        {
            if (ToggleGroupController.Instances.gameMode3.isOn == true)
            {
                HandleIncreaseScore();
            }

            TankController tankController = GetComponent<TankController>();
            if (tankController != null) tankController.SetSpeedToZero();
            col.GetComponent<DestroyTankBattle>().SetTimeToDestroy();
            col.transform.GetChild(1).gameObject.SetActive(false);
            if (ToggleGroupController.Instances.gameMode1.isOn == true)
            {
                col.GetComponent<DestroyTankBattle>().IncreaseTheNumberOfDeathPlayers();
            }

            Destroy(col.GetComponent<TankController>());
            Destroy(col.GetComponent<AmmoTankBattleController>());
            if (col.GetComponent<AITankBattle>() != null) Destroy(col.GetComponent<AITankBattle>());
             Debug.Log("Destroy");
            Destroy(gameObject);
        }
        else if (col.CompareTag("Enviroment"))
        {
            Destroy(gameObject);
        }
    }

    void HandleIncreaseScore()
    {
        if (nameOwnerBullet == "Player1" || nameOwnerBullet == "Player1(Clone)")
        {
            IncreaseScorePlayer1?.Invoke();
        }
        else if (nameOwnerBullet == "Player2" || nameOwnerBullet == "Player2(Clone)")
        {
            IncreaseScorePlayer2?.Invoke();
        }
        else if (nameOwnerBullet == "Player3" || nameOwnerBullet == "Player3(Clone)")
        {
            IncreaseScorePlayer3?.Invoke();
        }
        else if (nameOwnerBullet == "Player4" || nameOwnerBullet == "Player4(Clone)")
        {
            IncreaseScorePlayer4?.Invoke();
        }
    }
}