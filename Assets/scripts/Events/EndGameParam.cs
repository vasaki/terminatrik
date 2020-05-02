using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct EndGameParam : EventParameter
{
    public int score;
    public TimeSpan time;
    public EndGameParam(int score, TimeSpan time)
    {
        this.score = score;
        this.time = time;
    }
}
