using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxScore = 100;
    public int currentScore;
    public int rawScore = 50;

    public SyncScore syncscore;
    void Start()
    {
        currentScore = rawScore;
        syncscore.SetMaxSync(maxScore);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && (currentScore <=100))/*both slots are good */
        {
            IncreaseScore(20);
        }
        else if (Input.GetKeyDown(KeyCode.A) && (currentScore >= 0))/*slots are not good*/
        {
            DecreaseScore(20);
        }
    }
    void IncreaseScore(int score)
    {
        currentScore += score;
        syncscore.SetSynch(currentScore);
    }
    void DecreaseScore(int score)
    {
        currentScore -= score;
        syncscore.SetSynch(currentScore);
    }
}
