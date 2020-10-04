using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DifficultyLevel
{
    EASY = 0,
    MEDIUM = 1,
    HARD = 2
}
public class CardList : MonoBehaviour
{
    public LouisCard[] cards;
    public DifficultyLevel difficulty = 0;
    // Start is called before the first frame update
    void Start()
    {
        cards = GetComponentsInChildren<LouisCard>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
