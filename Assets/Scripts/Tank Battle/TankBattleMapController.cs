using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBattleMapController : MonoBehaviour
{
    public static TankBattleMapController Instances;
    public int numberPlayerDestroyed;
    public CoreGameController coreGameController;
    public Transform parentListPlayer;
    public Transform pointSpawn1, pointSpawn2, pointSpawn3, pointSpawn4;
    public GameObject player1, player2, player3, player4;
    public GameObject flag;

    private void Awake()
    {
        numberPlayerDestroyed = 0;
        Instances = this;
        Invoke("CallHandleComponentPlayer", 0.4f);
    }

    void CallHandleComponentPlayer()
    {
        HandleComponentPlayer(null);
    }

    private void HandleComponentPlayer(GameObject spawnTank)
    {
        foreach (Transform button in ReverseGravityController.Instances.transform)
        {
            if (spawnTank != null)
            {
                if (button.gameObject.activeInHierarchy && button.transform.name == "ButtonPlayer2")
                {
                    if (spawnTank.name == "Player2" || spawnTank.name == "Player2(Clone)")
                    {
                        Destroy(spawnTank.GetComponent<AITankBattle>());
                    }
                }
                else if (button.gameObject.activeInHierarchy && button.transform.name == "ButtonPlayer3")
                {
                    if (spawnTank.name == "Player3" || spawnTank.name == "Player3(Clone)")
                    {
                        Destroy(spawnTank.GetComponent<AITankBattle>());
                    }
                }

                else if (button.gameObject.activeInHierarchy && button.transform.name == "ButtonPlayer4")
                {
                    if (spawnTank.name == "Player4" || spawnTank.name == "Player4(Clone)")
                    {
                        Destroy(spawnTank.GetComponent<AITankBattle>());
                    }
                }
            }
            else
            {
                if (button.gameObject.activeInHierarchy && button.transform.name == "ButtonPlayer1")
                {
                    parentListPlayer.GetChild(1).GetComponent<AITankBattle>().enabled = true;
                    parentListPlayer.GetChild(2).GetComponent<AITankBattle>().enabled = true;
                    parentListPlayer.GetChild(3).GetComponent<AITankBattle>().enabled = true;
                }
                else if (button.gameObject.activeInHierarchy && button.transform.name == "ButtonPlayer2")
                {
                    Destroy(parentListPlayer.GetChild(1).GetComponent<AITankBattle>());
                }
                else if (button.gameObject.activeInHierarchy && button.transform.name == "ButtonPlayer3")
                {
                    Destroy(parentListPlayer.GetChild(2).GetComponent<AITankBattle>());
                }

                else if (button.gameObject.activeInHierarchy && button.transform.name == "ButtonPlayer4")
                {
                    Destroy(parentListPlayer.GetChild(3).GetComponent<AITankBattle>());
                }
            }
        }
    }

    public void HandleDeathMatchMap()
    {
    }

    public void HandleSpawnTank(string nameTank, float time)
    {
        StartCoroutine(SpawnTank(nameTank, time));
    }

    public IEnumerator SpawnTank(string nameTank, float time)
    {
        yield return new WaitForSeconds(time);
        if (nameTank == "Player1" || nameTank == "Player1(Clone)")
        {
            if (pointSpawn1.childCount < 1)
            {
                GameObject spawnTank = Instantiate(player1, pointSpawn1);
                HandleTankAfterSpawn(spawnTank);
            }
        }

        else if (nameTank == "Player2" || nameTank == "Player2(Clone)")
        {
            if (pointSpawn2.childCount < 1)
            {
                GameObject spawnTank = Instantiate(player2, pointSpawn2);
                HandleTankAfterSpawn(spawnTank);
            }
        }

        else if (nameTank == "Player3" || nameTank == "Player3(Clone)")
        {
            if (pointSpawn3.childCount < 1)
            {
                GameObject spawnTank = Instantiate(player3, pointSpawn3);
                HandleTankAfterSpawn(spawnTank);
            }
        }

        else if (nameTank == "Player4" || nameTank == "Player4(Clone)")
        {
            if (pointSpawn4.childCount <= 1)
            {
                GameObject spawnTank = Instantiate(player4, pointSpawn4);
                HandleTankAfterSpawn(spawnTank);
            }
        }
    }

    void HandleTankAfterSpawn(GameObject spawnTank)
    {
        spawnTank.GetComponent<AmmoTankBattleController>().SetAmountBullet();
        spawnTank.transform.SetParent(parentListPlayer);
        HandleComponentPlayer(spawnTank);
        if (ToggleGroupController.Instances.gameMode3.isOn == true)
        {
            spawnTank.transform.GetChild(4).gameObject.SetActive(true);
            StartCoroutine(DisableShield(spawnTank));
        }
    }

    IEnumerator DisableShield(GameObject spawnTank)
    {
        yield return new WaitForSeconds(3);
        if (spawnTank != null)
            spawnTank.transform.GetChild(4).gameObject.SetActive(false);
    }

    public void HandleClassicMap()
    {
        if (numberPlayerDestroyed == 3)
        {
            foreach (Transform player in parentListPlayer)
            {
                if (player.GetChild(1).gameObject.activeInHierarchy)
                {
                    coreGameController.HandleShowResult(player.name, null);
                }
            }
        }
    }
}