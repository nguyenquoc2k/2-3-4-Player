using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ScoreMatching : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scorePlayer1Text, scorePlayer2Text, scorePlayer3Text, scorePlayer4Text;
    private int scorePlayer1, scorePlayer2, scorePlayer3, scorePlayer4;
    public GameObject coreGame;

    void OnEnable()
    {
        ShowResult.HandleTime += ResetTime;
        MatchingGameController.IncreaseScore += HandleScoreStickBall;
        MatchingGameController.HandleEndGame += CheckAndShowResult;
        Invoke("AssignComponent", 0.1f);
        ResetTime();
    }

    private void HandleScoreStickBall(int id)
    {
        if (id == 0)
        {
            IncreaseDecreaseScorePlayer1(1);
        }
        else if (id == 1)
        {
            IncreaseDecreaseScorePlayer2(1);
        }
        else if (id == 2)
        {
            IncreaseDecreaseScorePlayer3(1);
        }
        else
        {
            IncreaseDecreaseScorePlayer4(1);
        }
    }


    private void IncreaseDecreaseScorePlayer1(int score)
    {
        scorePlayer1 += score < 0 ? score : Mathf.Abs(score);
        if (scorePlayer1 < 0) scorePlayer1 = 0;
        scorePlayer1Text.text = scorePlayer1.ToString();
    }


    private void IncreaseDecreaseScorePlayer2(int score)
    {
        scorePlayer2 += score < 0 ? score : Mathf.Abs(score);
        if (scorePlayer2 < 0) scorePlayer2 = 0;
        scorePlayer2Text.text = scorePlayer2.ToString();
    }

    private void IncreaseDecreaseScorePlayer3(int score)
    {
        scorePlayer3 += score < 0 ? score : Mathf.Abs(score);
        if (scorePlayer3 < 0) scorePlayer3 = 0;
        scorePlayer3Text.text = scorePlayer3.ToString();
    }

    private void IncreaseDecreaseScorePlayer4(int score)
    {
        scorePlayer4 += score < 0 ? score : Mathf.Abs(score);
        if (scorePlayer4 < 0) scorePlayer4 = 0;
        scorePlayer4Text.text = scorePlayer4.ToString();
    }

    void CheckAndShowResult()
    {
        // Khai b??o m???ng ch???a c??c gi?? tr??? ??i???m s???
        int[] scores = new int[] { scorePlayer1, scorePlayer2, scorePlayer3, scorePlayer4 };

        // T??m gi?? tr??? l???n nh???t trong m???ng
        int maxScore = scores.Max();

        // T??m c??c bi???n c?? gi?? tr??? b???ng gi?? tr??? l???n nh???t
        List<string> playersWithMaxScore = Enumerable.Range(1, 4) // T???o m???t m???ng s??? t??? 1 ?????n 4
            .Where(i => scores[i - 1] == maxScore) // L???c ra c??c s??? c?? gi?? tr??? b???ng gi?? tr??? l???n nh???t
            .Select(i => $"Player{i}") // Chuy???n c??c s??? th??nh t??n c???a ng?????i ch??i t????ng ???ng
            .ToList(); // Chuy???n k???t qu??? th??nh danh s??ch (list)

        // In ra k???t qu???
        if (playersWithMaxScore.Count > 1)
        {
            // N???u c?? nhi???u h??n m???t ng?????i ch??i c?? ??i???m s??? cao nh???t
            Debug.Log($"C?? nhi???u ng?????i ch??i c?? ??i???m s??? cao nh???t: {string.Join(", ", playersWithMaxScore)} v???i ??i???m s??? {maxScore}");
            CoreGameController.Instances.HandleShowResult(playersWithMaxScore[0], playersWithMaxScore[1]);
        }
        else
        {
            // N???u ch??? c?? m???t ng?????i ch??i c?? ??i???m s??? cao nh???t
            Debug.Log($"Ng?????i ch??i {playersWithMaxScore[0]} c?? ??i???m s??? cao nh???t: {maxScore}");
            CoreGameController.Instances.HandleShowResult(playersWithMaxScore[0], null);
        }
    }


    private void OnDisable()
    {
        ShowResult.HandleTime -= ResetTime;
        MatchingGameController.IncreaseScore -= HandleScoreStickBall;
        MatchingGameController.HandleEndGame -= CheckAndShowResult;
    }

    private void ResetTime()
    {
        scorePlayer1 = scorePlayer2 = scorePlayer3 = scorePlayer4 = 0;
        scorePlayer1Text.text = scorePlayer1.ToString();
        scorePlayer2Text.text = scorePlayer2.ToString();
        scorePlayer3Text.text = scorePlayer3.ToString();
        scorePlayer4Text.text = scorePlayer4.ToString();
    }
}