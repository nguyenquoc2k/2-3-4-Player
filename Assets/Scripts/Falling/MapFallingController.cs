using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapFallingController : MonoBehaviour
{
    [SerializeField] private GameObject player2, player3, player4;
    public static MapFallingController Instances;
    public List<bool> listCheckPlayer = new List<bool>();
    void Awake()
    {
        Invoke("HandleComponentPlayer", 0.01f);
        Instances = this;
    }
    private void HandleComponentPlayer()
    {
        foreach (Transform button in UIInGameController.Instances.transform)
        {
            if (button.gameObject.activeInHierarchy && button.transform.name == "FixedJoystick")
            {
                Destroy(player2.transform.GetComponent<PlayerControllerFalling>());
                Destroy(player3.transform.GetComponent<PlayerControllerFalling>());
                Destroy(player4.transform.GetComponent<PlayerControllerFalling>());
            }
            else if (button.gameObject.activeInHierarchy && button.transform.name == "FixedJoystick1")
            {
                player2.transform.AddComponent<PlayerControllerFalling>();
                Destroy(player2.transform.GetComponent<AIPlayerFalling>());
            }
            else if (button.gameObject.activeInHierarchy && button.transform.name == "FixedJoystick2")
            {
                player3.transform.AddComponent<PlayerControllerFalling>();
                Destroy(player3.transform.GetComponent<AIPlayerFalling>());
            }

            else if (button.gameObject.activeInHierarchy && button.transform.name == "FixedJoystick3")
            {
                player4.transform.AddComponent<PlayerControllerFalling>();
                Destroy(player4.transform.GetComponent<AIPlayerFalling>());
            }
        }
    }
    
    public void CheckPlayerDeath(int id)
    {
        if (id == 0)
        {
            listCheckPlayer[0] = true;
        }
        else if (id == 1)
        {
            listCheckPlayer[1] = true;
        }
        else if (id == 2)
        {
            listCheckPlayer[2] = true;
        }
        else
        {
            listCheckPlayer[3] = true;
        }
        ShowResultFlappyBird();
    }

    private void ShowResultFlappyBird()
    {
        List<string> listPlayerNames = new List<string>{"Player1", "Player2", "Player3", "Player4"};
        int falseCount = 0;
        int falseIndex = -1;

        for (int i = 0; i < listCheckPlayer.Count; i++)
        {
            if (!listCheckPlayer[i])
            {
                falseCount++;
                falseIndex = i;
                if (falseCount > 1)
                {
                    break;
                }
            }
        }

        if (falseCount == 1)
        {
            CoreGameController.Instances.HandleShowResult(listPlayerNames[falseIndex],null);
        }
    }
}
