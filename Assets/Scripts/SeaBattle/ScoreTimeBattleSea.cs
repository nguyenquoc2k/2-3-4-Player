using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTimeBattleSea : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeTopText, timeDownText;
    [SerializeField] private TextMeshProUGUI scorePlayer1Text, scorePlayer2Text, scorePlayer3Text, scorePlayer4Text;
    private int scorePlayer1, scorePlayer2, scorePlayer3, scorePlayer4;
    private float timeLeft;
    public CoreGameController coreGameController;
    public GameObject coreGame;
    bool resultShown = false;

    void OnEnable()
    {
        ShowResult.HandleTime += ResetTime;
        ShipBattleSea.IncreaseScoreBattleSeaPlayer += HandleScoreBattleSea;
        Invoke("AssignComponent", 0.1f);
        timeLeft = 60;
        scorePlayer1 = scorePlayer2 = scorePlayer3 = scorePlayer4 = 0;
        resultShown = false;
    }

    private void HandleScoreBattleSea(int idBow, int score)
    {
        if (idBow == 0)
        {
            IncreaseScorePlayer1(score);
        }
        else if (idBow == 1)
        {
            IncreaseScorePlayer2(score);
        }
        else if (idBow == 2)
        {
            IncreaseScorePlayer3(score);
        }
        else
        {
            IncreaseScorePlayer4(score);
        }
    }

    void AssignComponent()
    {
        coreGameController = coreGame.transform.GetChild(0).GetComponent<CoreGameController>();
    }

    private void IncreaseScorePlayer1(int score)
    {
        scorePlayer1 += score < 0 ? score : Mathf.Abs(score);
        scorePlayer1Text.text = scorePlayer1.ToString();
    }

    private void IncreaseScorePlayer2(int score)
    {
        scorePlayer2 += score < 0 ? score : Mathf.Abs(score);
        scorePlayer2Text.text = scorePlayer2.ToString();
    }

    private void IncreaseScorePlayer3(int score)
    {
        scorePlayer3 += score < 0 ? score : Mathf.Abs(score);
        scorePlayer3Text.text = scorePlayer3.ToString();
    }

    private void IncreaseScorePlayer4(int score)
    {
        scorePlayer4 += score < 0 ? score : Mathf.Abs(score);
        scorePlayer4Text.text = scorePlayer4.ToString();
    }

    private void OnDisable()
    {
        ShowResult.HandleTime -= ResetTime;
        ShipBattleSea.IncreaseScoreBattleSeaPlayer -= HandleScoreBattleSea;
    }

    private void ResetTime()
    {
        resultShown = false;
        timeLeft = 60;
        scorePlayer1 = scorePlayer2 = 0;
        scorePlayer1Text.text = scorePlayer1.ToString();
        scorePlayer2Text.text = scorePlayer1.ToString();
        scorePlayer3Text.text = scorePlayer2.ToString();
        scorePlayer4Text.text = scorePlayer2.ToString();
    }

    void Update()
    {
        if (resultShown == false)
        {
            timeLeft -= Time.deltaTime;
            timeTopText.text = ((int)timeLeft).ToString();
            timeDownText.text = ((int)timeLeft).ToString();
            // Khi đếm ngược đến 0, dừng script
            if (timeLeft <= 0)
            {
                // Tạo một mảng chứa giá trị của 4 biến
                int[] scores = { scorePlayer1, scorePlayer2, scorePlayer3, scorePlayer4 };
                // Sắp xếp mảng giảm dần
                Array.Sort(scores);
                Array.Reverse(scores);
                // Kiểm tra nếu giá trị lớn nhất xuất hiện duy nhất một lần
                int maxScore = scores[0];
                if (maxScore != scores[1])
                {
                    if (scorePlayer1 == maxScore)
                    {
                        coreGameController.HandleShowResult("Player1", null);
                    }
                    else if (scorePlayer2 == maxScore)
                    {
                        coreGameController.HandleShowResult("Player2", null);
                    }
                    else if (scorePlayer3 == maxScore)
                    {
                        coreGameController.HandleShowResult("Player3", null);
                    }
                    else if (scorePlayer4 == maxScore)
                    {
                        coreGameController.HandleShowResult("Player4", null);
                    }
                }
                else
                {
                    coreGameController.HandleShowResult(null, null);
                }

                resultShown = true;
            }
        }
    }
}