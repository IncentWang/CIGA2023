﻿using UnityEngine;

public class FishFeelingManager : MonoBehaviour
{
    public float Feeling; //好感度
    public float Coefficient; //系数
    public float Base; // 基础值

    public float Timer;
    public float CooldownTimer;
    public bool Changing;

    public void ChangeFeeling()
    {
        Feeling += Coefficient * Base;
    }

    void Update()
    {
        if (Changing)
        {
            Timer += Time.deltaTime;
        }
        else
        {
            Timer = 0.0f;
        }

        if (Timer > CooldownTimer)
        {
            Timer = 0.0f;
            ChangeFeeling();
        }
    }
}