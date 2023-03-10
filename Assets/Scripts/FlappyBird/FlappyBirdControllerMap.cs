using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlappyBirdControllerMap : MonoBehaviour
{
    [SerializeField] private GameObject player2, player3, player4;
    public static FlappyBirdControllerMap Instances;
    public List<bool> listCheckPlayer = new List<bool>();

    void Awake()
    {
        Invoke("HandleComponentPlayer", 0.01f);
        Instances = this;
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

    private void HandleComponentPlayer()
    {
        foreach (Transform button in ReverseGravityController.Instances.transform)
        {
            if (button.gameObject.activeInHierarchy && button.transform.name == "ButtonPlayer1")
            {
                Destroy(player2.transform.GetComponent<BirdController>());
                Destroy(player3.transform.GetComponent<BirdController>());
                Destroy(player4.transform.GetComponent<BirdController>());
            }
            else if (button.gameObject.activeInHierarchy && button.transform.name == "ButtonPlayer2")
            {
                player2.transform.AddComponent<BirdController>();
                Destroy(player2.transform.GetComponent<BotFlappyBird>());
            }
            else if (button.gameObject.activeInHierarchy && button.transform.name == "ButtonPlayer3")
            {
                player3.transform.AddComponent<BirdController>();
                Destroy(player3.transform.GetComponent<BotFlappyBird>());
            }

            else if (button.gameObject.activeInHierarchy && button.transform.name == "ButtonPlayer4")
            {
                player4.transform.AddComponent<BirdController>();
                Destroy(player4.transform.GetComponent<BotFlappyBird>());
            }
        }
    }
}