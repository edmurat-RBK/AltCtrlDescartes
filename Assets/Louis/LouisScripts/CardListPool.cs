using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardListPool : MonoBehaviour
{
    public CardList[] cardLists;

    private void Start()
    {
        cardLists = GetComponentsInChildren<CardList>();
    }

}
