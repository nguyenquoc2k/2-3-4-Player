using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreAndTimeController : MonoBehaviour
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
        BulletController.IncreaseScorePlayer1 += IncreaseScorePlayer1;
        BulletController.IncreaseScorePlayer2 += IncreaseScorePlayer2;
        BulletController.IncreaseScorePlayer3 += IncreaseScorePlayer3;
        BulletController.IncreaseScorePlayer4 += IncreaseScorePlayer4;
        Invoke("AssignComponent", 0.1f);
        timeLeft = 99;
        scorePlayer1 = scorePlayer2 = scorePlayer3 = scorePlayer4 = 0;
        resultShown = false;
    }

    void AssignComponent()
    {
        coreGameController = coreGame.transform.GetChild(0).GetComponent<CoreGameController>();
    }

    private void IncreaseScorePlayer4()
    {
        scorePlayer4++;
        scorePlayer4Text.text = scorePlayer4.ToString();
    }

    private void IncreaseScorePlayer3()
    {
        scorePlayer3++;
        scorePlayer3Text.text = scorePlayer3.ToString();
    }

    private void IncreaseScorePlayer2()
    {
        scorePlayer2++;
        scorePlayer2Text.text = scorePlayer2.ToString();
    }

    private void IncreaseScorePlayer1()
    {
        scorePlayer1++;
        scorePlayer1Text.text = scorePlayer1.ToString();
    }

    private void OnDisable()
    {
        ShowResult.HandleTime -= ResetTime;
        BulletController.IncreaseScorePlayer1 -= IncreaseScorePlayer1;
        BulletController.IncreaseScorePlayer2 -= IncreaseScorePlayer2;
        BulletController.IncreaseScorePlayer3 -= IncreaseScorePlayer3;
        BulletController.IncreaseScorePlayer4 -= IncreaseScorePlayer4;
    }

    private void ResetTime()
    {
        resultShown = false;
        timeLeft = 99;
        scorePlayer1 = scorePlayer2 = scorePlayer3 = scorePlayer4 = 0;
        
    }

    void Update()
    {
        if (resultShown == false)
        {
            timeLeft -= Time.deltaTime;
            timeTopText.text = ((int)timeLeft).ToString();
            timeDownText.text = ((int)timeLeft).ToString();
            // Khi ?????m ng?????c ?????n 0, d???ng script
            if (timeLeft <= 0)
            {
                // T???o m???t m???ng ch???a gi?? tr??? c???a 4 bi???n
                int[] scores = { scorePlayer1, scorePlayer2, scorePlayer3, scorePlayer4 };
                // S???p x???p m???ng gi???m d???n
                Array.Sort(scores);
                Array.Reverse(scores);
                // Ki???m tra n???u gi?? tr??? l???n nh???t xu???t hi???n duy nh???t m???t l???n
                int maxScore = scores[0];
                if (maxScore != scores[1] && maxScore != scores[2] && maxScore != scores[3])
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
                    else
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