using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController Instances;
    public TextMeshProUGUI txtTimeCountDownTop, txtTimeCountDownDown;
    private bool countdownEnded = false;
    

    void Awake()
    {
        Instances = this;
       
    }

    public void SetTimeCountDown(int count)
    {
        
        StartCoroutine(Countdown(count));
    }

    IEnumerator Countdown(int count)
    {
        while (count > 0)
        {
            txtTimeCountDownTop.text = count.ToString();
            txtTimeCountDownDown.text = count.ToString();

            yield return new WaitForSeconds(1f);
            count--;
        }

        countdownEnded = true;
        txtTimeCountDownTop.text = "0";
        txtTimeCountDownDown.text = "0";
        if (count < 0)
        {
            count = 0;
        }
    }
}