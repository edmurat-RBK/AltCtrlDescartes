using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynchroScore
{
    public int score = 50;
    public int scoreDecay = 4;
    public int scoreIncrease = 12;
    private int failStreak = 0;
    public float decayScale = 1.2f;

    public void DecayScore()
    {
        score -= (int)Mathf.Round(scoreDecay + (scoreDecay * (decayScale * failStreak)));
        if(score < 0)
        {
            score = 0;
        }
        failStreak++;
    }

    public void IncreaseScore()
    {
        score += scoreIncrease;
        if(score > 100)
        {
            score = 100;
        }
        failStreak = 0;
    }
}
