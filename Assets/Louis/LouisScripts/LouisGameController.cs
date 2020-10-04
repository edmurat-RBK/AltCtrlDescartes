using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LouisGameController : MonoBehaviour
{
    public CardList player1CardList, player2CardList;
    public bool player1Winned = false, player2Winned = false;
    public int player1ProgressionNumber = 0, player2ProgressionNumber = 0, errorCount = 0;

    void Start()
    {
        InitializedCardList(player1CardList);
        InitializedCardList(player2CardList);
        player1CardList.cards[player1ProgressionNumber].isHidden = false;
        player2CardList.cards[player2ProgressionNumber].isHidden = false;
    }

    void InitializedCardList(CardList cardList)
    {
        foreach (LouisCard card in cardList.cards)
        {
            card.isValidated = false;
            card.isHidden = true;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.Z) && !player1Winned)
        {
//            if(player1CardList.cards[player1ProgressionNumber].isValidated = input)
            player1CardList.cards[player1ProgressionNumber].isValidated = true;
            player1ProgressionNumber++;
            if (player1ProgressionNumber < player1CardList.cards.Length)
            {
                player1CardList.cards[player1ProgressionNumber].isHidden = false;
            }
            else
            {
                player1Winned = true;
                Debug.Log("Player1Win !");
            }
        }
        if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.S) && !player2Winned)
        {
            player2CardList.cards[player2ProgressionNumber].isValidated = true;
            player2ProgressionNumber++;
            if (player2ProgressionNumber < player2CardList.cards.Length)
            {
                player2CardList.cards[player2ProgressionNumber].isHidden = false;
            }
            else
            {
                player2Winned = true;
                Debug.Log("Player2Win !");
            }
        }
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.X))
        {
            errorCount++;
        }
        if (errorCount >= 3)
        {
            errorCount = 0;
            player1ProgressionNumber = 0;
            player2ProgressionNumber = 0;

            InitializedCardList(player1CardList);
            InitializedCardList(player2CardList);
            player1CardList.cards[player1ProgressionNumber].isHidden = false;
            player2CardList.cards[player2ProgressionNumber].isHidden = false;
        }
    }
}
