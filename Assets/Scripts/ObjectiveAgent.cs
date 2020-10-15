﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveAgent : MonoBehaviour
{
    public PlayerName player;
    public int index;
    private CardSymbol symbol;

    [Space(10)]

    private SpriteRenderer spriteRenderer;
    public ParticleSystem smokeParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (smokeParticleSystem.isEmitting && ObjectiveManager.Instance.objectiveSets[(int)player].set.Length <= index)
        {
            smokeParticleSystem.Stop();
        }

        // If it is the current objective
        if (ObjectiveManager.Instance.currentObjective[(int)player] == index && !ObjectiveManager.Instance.finishObjective[(int)player])
        {
            switch (ObjectiveManager.Instance.objectiveSets[(int)player].set[index])
            {
                case CardSymbol.PINK:
                    spriteRenderer.sprite = ObjectiveManager.Instance.pinkSprite;
                    break;

                case CardSymbol.BLUE:
                    spriteRenderer.sprite = ObjectiveManager.Instance.blueSprite;
                    break;

                case CardSymbol.ORANGE:
                    spriteRenderer.sprite = ObjectiveManager.Instance.orangeSprite;
                    break;
            }

            smokeParticleSystem.Stop();
        }
        // If the objective is hidden
        else if(ObjectiveManager.Instance.currentObjective[(int)player] < index)
        {
            spriteRenderer.sprite = null;
        }
        // If the objective is validated
        else if(ObjectiveManager.Instance.currentObjective[(int)player] > index || ObjectiveManager.Instance.finishObjective[(int)player])
        {
            switch (ObjectiveManager.Instance.objectiveSets[(int)player].set[index])
            {
                case CardSymbol.PINK:
                    spriteRenderer.sprite = ObjectiveManager.Instance.pinkValidatedSprite;
                    break;

                case CardSymbol.BLUE:
                    spriteRenderer.sprite = ObjectiveManager.Instance.blueValidatedSprite;
                    break;

                case CardSymbol.ORANGE:
                    spriteRenderer.sprite = ObjectiveManager.Instance.orangeValidatedSprite;
                    break;
            }
        }
    }
}
