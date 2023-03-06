using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StickBallMap1 : MonoBehaviour
{
    [SerializeField] private GameObject player2, player3, player4;
    void Awake()
    {
        Invoke("HandleComponentPlayer", 0.01f);
    }
    
    private void HandleComponentPlayer()
    {
        foreach (Transform button in ReverseGravityController.Instances.transform)
        {
            if (button.gameObject.activeInHierarchy && button.transform.name == "ButtonPlayer1")
            {
                Destroy(player2.transform.GetChild(0).GetComponent<PlayerStickBall>());
                Destroy(player3.transform.GetChild(0).GetComponent<PlayerStickBall>());
                Destroy(player4.transform.GetChild(0).GetComponent<PlayerStickBall>());
            }
            else if (button.gameObject.activeInHierarchy && button.transform.name == "ButtonPlayer2")
            {
                player2.transform.GetChild(0).AddComponent<PlayerStickBall>();
                Destroy(player2.transform.GetChild(0).GetComponent<AIStickBallMap1>());
            }
            else if (button.gameObject.activeInHierarchy && button.transform.name == "ButtonPlayer3")
            {
                player3.transform.GetChild(0).AddComponent<PlayerStickBall>();
                Destroy(player3.transform.GetChild(0).GetComponent<AIStickBallMap1>());
            }

            else if (button.gameObject.activeInHierarchy && button.transform.name == "ButtonPlayer4")
            {
                player4.transform.GetChild(0).AddComponent<PlayerStickBall>();
                Destroy(player4.transform.GetChild(0).GetComponent<AIStickBallMap1>());
            }
        }
    }
}
