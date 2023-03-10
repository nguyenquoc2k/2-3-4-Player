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
        // Khai báo mảng chứa các giá trị điểm số
        int[] scores = new int[] { scorePlayer1, scorePlayer2, scorePlayer3, scorePlayer4 };

        // Tìm giá trị lớn nhất trong mảng
        int maxScore = scores.Max();

        // Tìm các biến có giá trị bằng giá trị lớn nhất
        List<string> playersWithMaxScore = Enumerable.Range(1, 4) // Tạo một mảng số từ 1 đến 4
            .Where(i => scores[i - 1] == maxScore) // Lọc ra các số có giá trị bằng giá trị lớn nhất
            .Select(i => $"Player{i}") // Chuyển các số thành tên của người chơi tương ứng
            .ToList(); // Chuyển kết quả thành danh sách (list)

        // In ra kết quả
        if (playersWithMaxScore.Count > 1)
        {
            // Nếu có nhiều hơn một người chơi có điểm số cao nhất
            Debug.Log($"Có nhiều người chơi có điểm số cao nhất: {string.Join(", ", playersWithMaxScore)} với điểm số {maxScore}");
            CoreGameController.Instances.HandleShowResult(playersWithMaxScore[0], playersWithMaxScore[1]);
        }
        else
        {
            // Nếu chỉ có một người chơi có điểm số cao nhất
            Debug.Log($"Người chơi {playersWithMaxScore[0]} có điểm số cao nhất: {maxScore}");
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