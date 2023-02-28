using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseGravityController : MonoBehaviour
{
    public static ReverseGravityController Instances;
    public static event Action<bool> ReversePlayer1,ReversePlayer2,ReversePlayer3,ReversePlayer4;
    private bool buttonStatePlayer1 = false,buttonStatePlayer2 = false,buttonStatePlayer3 = false,buttonStatePlayer4 = false;
    private bool move;
    private void Awake()
    {
        Instances = this;
    }

    public void OnMouseDown1()
    {
        buttonStatePlayer1 = !buttonStatePlayer1;
        ReversePlayer1?.Invoke(buttonStatePlayer1);
    }
    public void OnMouseDown2()
    {
        buttonStatePlayer2 = !buttonStatePlayer2;
        ReversePlayer2?.Invoke(buttonStatePlayer2);
    }
    public void OnMouseDown3()
    {
        buttonStatePlayer3 = !buttonStatePlayer3;
        ReversePlayer3?.Invoke(buttonStatePlayer3);
    }
    public void OnMouseDown4()
    {
        buttonStatePlayer4 = !buttonStatePlayer4;
        ReversePlayer4?.Invoke(buttonStatePlayer4);
    }

    public void PointerDown1()
    {
        buttonStatePlayer1 = true;
        ReversePlayer1?.Invoke(buttonStatePlayer1);
    }
    public void PointerUp1()
    {
        buttonStatePlayer1 = false;
        ReversePlayer1?.Invoke(buttonStatePlayer1);
    }
    public void PointerDown2()
    {
        buttonStatePlayer2 = true;
        ReversePlayer2?.Invoke(buttonStatePlayer2);
    }
    public void PointerUp2()
    {
        buttonStatePlayer2 = false;
        ReversePlayer2?.Invoke(buttonStatePlayer2);
    }
    public void PointerDown3()
    {
        buttonStatePlayer3 = true;
        ReversePlayer3?.Invoke(buttonStatePlayer3);
    }
    public void PointerUp3()
    {
        buttonStatePlayer3 = false;
        ReversePlayer3?.Invoke(buttonStatePlayer3);
    }
    public void PointerDown4()
    {
        buttonStatePlayer4 = true;
        ReversePlayer4?.Invoke(buttonStatePlayer4);
    }
    public void PointerUp4()
    {
        buttonStatePlayer4 = false;
        ReversePlayer4?.Invoke(buttonStatePlayer4);
    }
}