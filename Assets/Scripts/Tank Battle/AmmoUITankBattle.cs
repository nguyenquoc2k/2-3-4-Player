using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUITankBattle : MonoBehaviour
{
    public static AmmoUITankBattle Instances;
    public GameObject listAmmo1, listAmmo2, listAmmo3, listAmmo4;
    public int currentGameObjectCount;

    private void Awake()
    {
        Instances = this;
        AmmoTankBattleController.HandleAmmoEvent += HandleListAmmo;
    }

    private void HandleListAmmo(string nameCar, int numberAmmo)
    {
        if (nameCar == "Player1" || nameCar == "Player1(Clone)")
        {
            HandleEachPlayer(listAmmo1, numberAmmo);
        }

        if (nameCar == "Player2"||nameCar == "Player2(Clone)")
        {
            HandleEachPlayer(listAmmo2, numberAmmo);
        }

        if (nameCar == "Player3"||nameCar == "Player3Clone)")
        {
            HandleEachPlayer(listAmmo3, numberAmmo);
        }

        if (nameCar == "Player4"||nameCar == "Player4(Clone)")
        {
            HandleEachPlayer(listAmmo4, numberAmmo);
        }
    }

    void HandleEachPlayer(GameObject player, int numberAmmo)
    {
        int disabledGameObjectsCount = 0;
        if (numberAmmo > player.transform.childCount)
        {
            Debug.Log("Invalid child object count parameter.");
            return;
        }

        if (numberAmmo == currentGameObjectCount)
        {
            return; // không cần làm gì nếu đã đủ số lượng gameobject active rồi
        }

        // Disable các gameobject con thừa
        for (int i = numberAmmo; i < player.transform.childCount; i++)
        {
            player.transform.GetChild(i).gameObject.SetActive(false);
        }

        // Enable các gameobject con cần thiết
        for (int i = 0; i < numberAmmo; i++)
        {
            if (!player.transform.GetChild(i).gameObject.activeSelf)
            {
                player.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        currentGameObjectCount = numberAmmo;
    }

    private void OnDestroy()
    {
        AmmoTankBattleController.HandleAmmoEvent -= HandleListAmmo;
    }
}