using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LouisCard : MonoBehaviour
{
    public enum Symbol
    {
        Null,
        Orange,
        Pink,
        Blue
    }
    public Symbol symbole;
    public bool isHidden = true; // hide si pas encore le moment de l'obj
    public bool isValidated = false; // valiated quand c'est passé
    public SpriteRenderer spriteRenderer;
    public Sprite visible;
    public Sprite hidden;
    public Sprite validated;
    public bool unhidden = false;
    public bool alreadyValidated = false;

    void Update()
    {
        SpriteVisible();
        SpriteValidated();
    }
    void SpriteVisible()
    {
        if (isHidden == false && unhidden == false)
        {
            spriteRenderer.sprite = visible;
            unhidden = true;
        }
    }
    void SpriteValidated()
    {
        if (isValidated == true && alreadyValidated == false)
        {
            spriteRenderer.sprite = validated;
            alreadyValidated = true;
        }
    }
}
