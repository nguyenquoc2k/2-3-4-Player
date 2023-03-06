using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreAndTimeStickBall : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scorePlayer1Text, scorePlayer2Text, scorePlayer3Text, scorePlayer4Text;
    private int scorePlayer1, scorePlayer2, scorePlayer3, scorePlayer4;
    [SerializeField] List<bool> listPlayerScore = new List<bool>(4);
    public CoreGameController coreGameController;
    public GameObject coreGame;
    public static event Action<int, Collider2D> NoticeDeathPlayer;

    void OnEnable()
    {
        ShowResult.HandleTime += ResetTime;
        HouseStickBall.IncreaseOrDecreaseScoreStickBall += HandleScoreStickBall;
        HouseStickBall.IncreaseOrDecreaseScoreStickBallMap2 += HandleScoreStickBallMap2;
        Invoke("AssignComponent", 0.1f);
        ResetTime();
    }

    private void HandleScoreStickBallMap2(int idPlayer, int score)
    {
        if (idPlayer == 0)
        {
            IncreaseDecreaseScorePlayer1(score, null);
        }
        else if (idPlayer == 1)
        {
            IncreaseDecreaseScorePlayer2(score, null);
        }
        else if (idPlayer == 2)
        {
            IncreaseDecreaseScorePlayer3(score, null);
        }
        else
        {
            IncreaseDecreaseScorePlayer4(score, null);
        }
    }

    private void HandleScoreStickBall(int id, int score, Collider2D collider2D)
    {
        if (id == 0)
        {
            IncreaseDecreaseScorePlayer1(score, collider2D);
        }
        else if (id == 1)
        {
            IncreaseDecreaseScorePlayer2(score, collider2D);
        }
        else if (id == 2)
        {
            IncreaseDecreaseScorePlayer3(score, collider2D);
        }
        else
        {
            IncreaseDecreaseScorePlayer4(score, collider2D);
        }
    }

    void AssignComponent()
    {
        coreGameController = coreGame.transform.GetChild(0).GetComponent<CoreGameController>();
    }

    private void IncreaseDecreaseScorePlayer1(int score, Collider2D collision)
    {
        scorePlayer1 += score < 0 ? score : Mathf.Abs(score);
        if (scorePlayer1 < 0) scorePlayer1 = 0;
        scorePlayer1Text.text = scorePlayer1.ToString();
        if (scorePlayer1 == 0 && MapModeStickBall.Instances.isCountdown == true)
        {
            NoticeDeathPlayer?.Invoke(0, collision);
            listPlayerScore[0] = true;
            CheckAndShowReSultModeCountdown();
        }
        else if(scorePlayer1 == 5 && MapModeStickBall.Instances.isCountdown == false)
        {
            CoreGameController.Instances.HandleShowResult("Player1", null);
        }
    }


    private void IncreaseDecreaseScorePlayer2(int score, Collider2D collision)
    {
        scorePlayer2 += score < 0 ? score : Mathf.Abs(score);
        if (scorePlayer2 < 0) scorePlayer2 = 0;
        scorePlayer2Text.text = scorePlayer2.ToString();
        if (MapModeStickBall.Instances.isCountdown == true && scorePlayer2 == 0)
        {
            NoticeDeathPlayer?.Invoke(1, collision);
            listPlayerScore[1] = true;
            CheckAndShowReSultModeCountdown();
        }
        else if(scorePlayer1 == 5 && MapModeStickBall.Instances.isCountdown == false)
        {
            CoreGameController.Instances.HandleShowResult("Player2", null);
        }
    }

    private void IncreaseDecreaseScorePlayer3(int score, Collider2D collision)
    {
        scorePlayer3 += score < 0 ? score : Mathf.Abs(score);
        if (scorePlayer3 < 0) scorePlayer3 = 0;
        scorePlayer3Text.text = scorePlayer3.ToString();
        if (MapModeStickBall.Instances.isCountdown == true && scorePlayer3 == 0)
        {
            NoticeDeathPlayer?.Invoke(2, collision);
            listPlayerScore[2] = true;
            CheckAndShowReSultModeCountdown();
        }
        else if(scorePlayer1 == 5 && MapModeStickBall.Instances.isCountdown == false)
        {
            CoreGameController.Instances.HandleShowResult("Player3", null);
        }
    }

    private void IncreaseDecreaseScorePlayer4(int score, Collider2D collision)
    {
        scorePlayer4 += score < 0 ? score : Mathf.Abs(score);
        if (scorePlayer4 < 0) scorePlayer4 = 0;
        scorePlayer4Text.text = scorePlayer4.ToString();
        if (MapModeStickBall.Instances.isCountdown == true && scorePlayer4 == 0)
        {
            NoticeDeathPlayer?.Invoke(3, collision);
            listPlayerScore[3] = true;
            CheckAndShowReSultModeCountdown();
        }
        else if(scorePlayer1 == 5 && MapModeStickBall.Instances.isCountdown == false)
        {
            CoreGameController.Instances.HandleShowResult("Player4", null);
        }
    }

    

    private void CheckAndShowReSultModeCountdown()
    {
        int trueCount = 0;
        for (int i = 0; i < listPlayerScore.Count; i++)
        {
            if (listPlayerScore[i])
            {
                trueCount++;
            }
        }

        if (trueCount == 3)
        {
            for (int i = 0; i < listPlayerScore.Count; i++)
            {
                if (!listPlayerScore[i])
                {
                    CoreGameController.Instances.HandleShowResult("Player" + (i + 1), null);
                }
            }
        }
    }

    private void OnDisable()
    {
        ShowResult.HandleTime -= ResetTime;
        HouseStickBall.IncreaseOrDecreaseScoreStickBall -= HandleScoreStickBall;
    }

    private void ResetTime()
    {
        for (int i = 0; i < listPlayerScore.Count; i++)
        {
            listPlayerScore[i] = false;
        }

        if (MapModeStickBall.Instances.isCountdown == true)
            scorePlayer1 = scorePlayer2 = scorePlayer3 = scorePlayer4 = 5;
        else scorePlayer1 = scorePlayer2 = scorePlayer3 = scorePlayer4 = 0;
        scorePlayer1Text.text = scorePlayer1.ToString();
        scorePlayer2Text.text = scorePlayer2.ToString();
        scorePlayer3Text.text = scorePlayer3.ToString();
        scorePlayer4Text.text = scorePlayer4.ToString();
    }

    void Update()
    {
    }
}