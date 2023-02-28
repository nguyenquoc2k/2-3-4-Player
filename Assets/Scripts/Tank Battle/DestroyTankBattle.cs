using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTankBattle : MonoBehaviour
{
    public void SetTimeToDestroy()
    {
        if (ToggleGroupController.Instances.gameMode2.isOn == true)
        {
            TankBattleMapController.Instances.HandleSpawnTank(transform.name,5f);
            ReSpawnFlag();
            Invoke("DestroyTank", 4f);
        }
        if (ToggleGroupController.Instances.gameMode3.isOn == true)
        {
            TankBattleMapController.Instances.HandleSpawnTank(transform.name,2.5f);
            Invoke("DestroyTank", 2f);
        }
    }
    public void IncreaseTheNumberOfDeathPlayers()
    {
        TankBattleMapController.Instances.numberPlayerDestroyed++;
        TankBattleMapController.Instances.HandleClassicMap();
    }
    void DestroyTank()
    {
        Destroy(gameObject);
    }
    private void ReSpawnFlag()
    {
        TankController tankController = GetComponent<TankController>();
        if (tankController == null) return;
        if (tankController.faction == TankController.Faction.Ally && tankController.flag.activeSelf == true)
        {
            foreach (Transform flag in TankBattleMapController.Instances.flag.transform)
            {
                TankController.Faction colFactionFlag = flag.GetComponent<FlagTankBattle>().flagFaction;
                if (colFactionFlag == TankController.Faction.Enemy)
                {
                    flag.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            foreach (Transform flag in TankBattleMapController.Instances.flag.transform)
            {
                TankController.Faction colFactionFlag = flag.GetComponent<FlagTankBattle>().flagFaction;
                if (colFactionFlag == TankController.Faction.Ally)
                {
                    flag.gameObject.SetActive(true);
                }
            }
        }
    }
}