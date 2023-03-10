using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreTimeSoccer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeTopText, timeDownText;
    [SerializeField] private TextMeshProUGUI scorePlayer1Text, scorePlayer2Text, scorePlayer3Text, scorePlayer4Text;
    private int scorePlayer1, scorePlayer2;
    private float timeLeft;
    public CoreGameController coreGameController;
    public GameObject coreGame;
    bool resultShown = false;

    void OnEnable()
    {
        ShowResult.HandleTime += ResetTime;
        BallController.IncreaseScoreSoccerTeam1 += IncreaseScoreTeam1;
        BallController.IncreaseScoreSoccerTeam2 += IncreaseScoreTeam2;
        Invoke("AssignComponent", 0.1f);
        timeLeft = 99;
        scorePlayer1 = scorePlayer2 = 0;
        resultShown = false;
    }

    void AssignComponent()
    {
        coreGameController = coreGame.transform.GetChild(0).GetComponent<CoreGameController>();
    }
    

    private void IncreaseScoreTeam2()
    {
        scorePlayer2++;
        scorePlayer3Text.text = scorePlayer2.ToString();
        scorePlayer4Text.text = scorePlayer2.ToString();
    }

    private void IncreaseScoreTeam1()
    {
        scorePlayer1++;
        scorePlayer1Text.text = scorePlayer1.ToString();
        scorePlayer2Text.text = scorePlayer1.ToString();
    }

    private void OnDisable()
    {
        ShowResult.HandleTime -= ResetTime;
        BallController.IncreaseScoreSoccerTeam1 -= IncreaseScoreTeam1;
        BallController.IncreaseScoreSoccerTeam2 -= IncreaseScoreTeam2;
    }

    private void ResetTime()
    {
        resultShown = false;
        timeLeft = 99;
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
            // Khi ?????m ng?????c ?????n 0, d???ng script
            if (timeLeft <= 0)
            {
                // T???o m???t m???ng ch???a gi?? tr??? c???a 4 bi???n
                int[] scores = { scorePlayer1, scorePlayer2};
                // S???p x???p m???ng gi???m d???n
                Array.Sort(scores);
                Array.Reverse(scores);
                // Ki???m tra n???u gi?? tr??? l???n nh???t xu???t hi???n duy nh???t m???t l???n
                int maxScore = scores[0];
                if (maxScore != scores[1])
                {
                    if (scorePlayer1 == maxScore)
                    {
                        coreGameController.HandleShowResult("Player1", "Player2");
                    }
                    else if (scorePlayer2 == maxScore)
                    {
                        coreGameController.HandleShowResult("Player3", "Player4");
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