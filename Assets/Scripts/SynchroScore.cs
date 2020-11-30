using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SynchroScore : MonoBehaviour
{
    public float score = 0f;
    public int scoreDecay = 12;//4;
    public int scoreIncrease = 4;//12;
    private int failStreak = 0;
    public float decayScale = 1.2f;
    private GameObject[] progressBars;
    private bool scoreUpdate = true;
    [SerializeField] private float progressBarSizeDivider = 10f;
    [SerializeField] private float tLerp = 0.1f;
    private GameObject[] visualEffects;
    private void Start()
    {

        progressBars = GameObject.FindGameObjectsWithTag("ProgressBar");
        visualEffects = GameObject.FindGameObjectsWithTag("ProgressBarVFX");
        foreach (GameObject visualEffect in visualEffects)
        {
            visualEffect.SetActive(false);
        }
    }
    private void Update()
    {
        if (scoreUpdate)
        {
            //scoreUpdate = false;
            foreach (GameObject progressBar in progressBars)
            {
                Debug.Log(progressBar.transform.localScale);
                Vector3 targetScale = new Vector3(progressBar.transform.localScale.x, score / progressBarSizeDivider, progressBar.transform.localScale.z);
                Vector3 targetPosition = new Vector3(score / (progressBarSizeDivider * 2), progressBar.transform.localPosition.y, progressBar.transform.localPosition.z);
                progressBar.transform.localScale = Vector3.Lerp(progressBar.transform.localScale, targetScale, tLerp);
                progressBar.transform.localPosition = Vector3.Lerp(progressBar.transform.localPosition, targetPosition, tLerp);
            }
        }
    }

    public void DecayScore()
    {
        if( score > 0)
        {
            score -= scoreDecay;
        }

        scoreUpdate = true;
        //score -= (int)Mathf.Round(scoreDecay + (scoreDecay * (decayScale * failStreak)));
        //if(score < 0)
        //{
        //    score = 0;
        //}
        //failStreak++;
    }

    public void IncreaseScore()
    {
        if (score == 0)
        {
            foreach (GameObject visualEffect in visualEffects)
            {
                visualEffect.SetActive(true);
                VisualEffect vfx = visualEffect.GetComponent<VisualEffect>();
                if (vfx) vfx.Play();//Si on reussi à recuperer le visual effect on peut le play
            }
        }
        score = score == 0 ? 50 : score + scoreIncrease;
        scoreUpdate = true;
        //score += scoreIncrease;
        //if(score > 100)
        //{
        //    score = 100;
        //}
        //failStreak = 0;
    }
}
