using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score 
{
    private  int score = 100;
    public int GetScore()
    {
        return score;
    }

    public void SetScore(int change)
    {
        score += change;
    }

    public void ResetScore()
    {
        score = 100;
    }
}
