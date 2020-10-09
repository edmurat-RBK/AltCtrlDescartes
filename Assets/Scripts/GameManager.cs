﻿using System.Collections;
using System.Collections.Generic;
using Uduino;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance
    {
        get
        {
            if (_instance != null)
                return _instance;

            GameManager[] managers = FindObjectsOfType(typeof(GameManager)) as GameManager[];
            if (managers.Length == 0)
            {
                Debug.LogWarning("GameManager not present on the scene. Creating a new one.");
                GameManager manager = new GameObject("Game Manager").AddComponent<GameManager>();
                _instance = manager;
                return _instance;
            }
            else
            {
                return managers[0];
            }
        }
        set
        {
            if (_instance == null)
                _instance = value;
            else
            {
                Debug.LogError("You can only use one GameManager. Destroying the new one attached to the GameObject " + value.gameObject.name);
                Destroy(value);
            }
        }
    }
    private static GameManager _instance = null;
    #endregion

    public GameState state;
    

    private void Start()
    {
        state = GameState.WAIT_BOTH;
    }

    private void Update()
    {
        switch(state)
        {
            case GameState.WAIT_BOTH:
                if(SlotManager.Instance.cardDown[(int)PlayerName.CASTOR])
                {
                    state = GameState.WAIT_POLLUX;
                }
                else if (SlotManager.Instance.cardDown[(int)PlayerName.POLLUX])
                {
                    state = GameState.WAIT_CASTOR;
                }
                break;

            case GameState.WAIT_CASTOR:
                if (SlotManager.Instance.cardDown[(int)PlayerName.CASTOR])
                {
                    state = GameState.FEEDBACK;
                }
                break;

            case GameState.WAIT_POLLUX:
                if (SlotManager.Instance.cardDown[(int)PlayerName.POLLUX])
                {
                    state = GameState.FEEDBACK;
                }
                break;

            case GameState.FEEDBACK:
                // Compare cards
                CardSymbol successSymbol;
                if(SlotManager.Instance.CheckSuccess(out successSymbol))
                {
                    ObjectiveManager.Instance.Success(successSymbol);
                    Debug.Log("Round successful with symbol : " + (successSymbol == CardSymbol.TRIANGLE ? "TRIANGLE" : successSymbol == CardSymbol.SQUARE ? "SQUARE" : "CIRCLE"));
                }

                // Apply score

                // Send feedback


                if(ObjectiveManager.Instance.BothFinish())
                {
                    state = GameState.END;
                }
                else
                {
                    state = GameState.RESET;
                }
                break;

            case GameState.RESET:
                foreach(SlotAgent sa in SlotManager.Instance.slotAgents)
                {
                    sa.ResetToNextTurn();
                }

                SlotManager.Instance.cardDown[0] = false;
                SlotManager.Instance.cardDown[1] = false;

                state = GameState.WAIT_BOTH;
                break;

            case GameState.END:
                // Stop gameplay
                Debug.LogWarning("Game ended !");
                break;
        }
    }
}
