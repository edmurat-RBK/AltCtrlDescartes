using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SyncScore : MonoBehaviour
{
    public Slider slider;
    public int rawScore = 50;
    public int streak = 0;

    public void SetMaxSync(int score)
    {
        slider.maxValue = 100;
        slider.value = score;
    }
    public void SetSynch(int score)
    {
        slider.value = score;
    }
}