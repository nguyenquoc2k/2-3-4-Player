using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreMatching : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scorePlayer1Text, scorePlayer2Text, scorePlayer3Text, scorePlayer4Text;
    private int scorePlayer1, scorePlayer2, scorePlayer3, scorePlayer4;
    public CoreGameController coreGameController;
    public GameObject coreGame;

    void OnEnable()
    {
        ShowResult.HandleTime += ResetTime;
        
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
    }


    private void IncreaseDecreaseScorePlayer2(int score, Collider2D collision)
    {
        scorePlayer2 += score < 0 ? score : Mathf.Abs(score);
        if (scorePlayer2 < 0) scorePlayer2 = 0;
        scorePlayer2Text.text = scorePlayer2.ToString();
    }

    private void IncreaseDecreaseScorePlayer3(int score, Collider2D collision)
    {
        scorePlayer3 += score < 0 ? score : Mathf.Abs(score);
        if (scorePlayer3 < 0) scorePlayer3 = 0;
        scorePlayer3Text.text = scorePlayer3.ToString();
    }

    private void IncreaseDecreaseScorePlayer4(int score, Collider2D collision)
    {
        scorePlayer4 += score < 0 ? score : Mathf.Abs(score);
        if (scorePlayer4 < 0) scorePlayer4 = 0;
        scorePlayer4Text.text = scorePlayer4.ToString();
    }

    private void OnDisable()
    {
        ShowResult.HandleTime -= ResetTime;
       
    }

    private void ResetTime()
    {
        if (SelectMapGameDemo.Instances.modeTimeAndScore == true)
            scorePlayer1 = scorePlayer2 = scorePlayer3 = scorePlayer4 = 0;
        scorePlayer1Text.text = scorePlayer1.ToString();
        scorePlayer2Text.text = scorePlayer2.ToString();
        scorePlayer3Text.text = scorePlayer3.ToString();
        scorePlayer4Text.text = scorePlayer4.ToString();
    }

   
}
