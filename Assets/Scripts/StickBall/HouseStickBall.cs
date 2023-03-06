using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseStickBall : MonoBehaviour
{
    [SerializeField] int myIndex;
    [SerializeField] Collider2D cl2;

    [SerializeField] AIStickBallMap1 botStickBall;
    [SerializeField] PlayerStickBall playerStickBar;
    [SerializeField] Transform listPlayer;
    public static event Action<int, int, Collider2D> IncreaseOrDecreaseScoreStickBall;
    public static event Action<int, int> IncreaseOrDecreaseScoreStickBallMap2;

    private void Awake()
    {
        Invoke("HandleAssignComponent", 0.3f);
        ScoreAndTimeStickBall.NoticeDeathPlayer += HandleDeathPlayer;
    }

    private void HandleDeathPlayer(int idPlayer, Collider2D collision)
    {
        if (idPlayer == myIndex)
        {
            cl2.isTrigger = false;
            if (botStickBall != null) botStickBall.Dead();
            else if (playerStickBar != null) playerStickBar.Dead();
            Destroy(collision.gameObject);
        }
    }

    void HandleAssignComponent()
    {
        if (listPlayer.GetChild(myIndex).GetComponentInChildren<AIStickBallMap1>() != null)
            botStickBall = listPlayer.GetChild(myIndex).GetComponentInChildren<AIStickBallMap1>();
        else if (listPlayer.GetChild(myIndex).GetComponentInChildren<PlayerStickBall>() != null)
            playerStickBar = listPlayer.GetChild(myIndex).GetComponentInChildren<PlayerStickBall>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ball"))
        {
            if (MapModeStickBall.Instances.isCountdown == true)
                IncreaseOrDecreaseScoreStickBall?.Invoke(myIndex, -1, collision);
            if (MapModeStickBall.Instances.isCountdown == false)
            {
                if (collision.GetComponent<BallStickBall>().indexBall >= 0)
                {
                    IncreaseOrDecreaseScoreStickBall?.Invoke(myIndex, -1, collision);
                    IncreaseOrDecreaseScoreStickBallMap2?.Invoke(collision.GetComponent<BallStickBall>().indexBall, 1);
                }
            }

            ;
        }
    }

    private void OnDestroy()
    {
        ScoreAndTimeStickBall.NoticeDeathPlayer -= HandleDeathPlayer;
    }
}