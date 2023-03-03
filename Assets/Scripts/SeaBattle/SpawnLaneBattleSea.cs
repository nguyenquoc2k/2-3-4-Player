using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnLaneBattleSea : MonoBehaviour
{
    public List<ShipBattleSea> ships;

    public bool isTop;
    private ShipBattleSea shipBattleSea;
    public Route route;
    public Transform parentSpawn;
    public int indexSpawnLane;

    private void Awake()
    {
        SpawnRandomObjects();
        ShipBattleSea.ShipDestroyed += HandleShipDestroyed;
    }

    void SpawnRandomObjects()
    {
        int index = Random.Range(0, ships.Count);
        shipBattleSea = Instantiate(ships[index], transform.position, Quaternion.identity);
        shipBattleSea.SetIndexShip(indexSpawnLane);
        shipBattleSea.routes[0] = route.transform;
        shipBattleSea.transform.SetParent(parentSpawn);
        if (isTop) shipBattleSea.transform.localRotation = Quaternion.Euler(0, 0, -90);
        else shipBattleSea.transform.localRotation = Quaternion.Euler(0, 0, 90);
        // Đăng ký phương thức HandleShipDestroyed khi sự kiện ShipDestroyed được kích hoạt.
        
    }

    void HandleShipDestroyed(int idShip)
    {
        // Tạo một ShipBattleSea mới khi một ShipBattleSea bị hủy bỏ.
        if (idShip == indexSpawnLane)
            SpawnRandomObjects();
    }

    private void OnDestroy()
    {
        ShipBattleSea.ShipDestroyed -= HandleShipDestroyed;
    }
    
}