﻿using System.Collections;
using System.Collections.Generic;
using Uduino;
using UnityEngine;
using UnityEngine.UI;

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
    public SynchroScore score;
    public Text scoreDisplayCastor;
    public Text scoreDisplayPollux;
    private bool coroutineFeedback;
    

    private void Start()
    {
        state = GameState.WAIT_BOTH;
        score = new SynchroScore();
        coroutineFeedback = false;
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
                if (SlotManager.Instance.cardDown[(int)PlayerName.CASTOR] && !coroutineFeedback)
                {
                    StartCoroutine(SwitchStateFeedback());
                }
                break;

            case GameState.WAIT_POLLUX:
                if (SlotManager.Instance.cardDown[(int)PlayerName.POLLUX] && !coroutineFeedback)
                {
                    StartCoroutine(SwitchStateFeedback());
                }
                break;

            case GameState.FEEDBACK:
                // Compare cards
                CardSymbol successSymbol;
                if(SlotManager.Instance.CheckFullSuccess(out successSymbol))
                {
                    ObjectiveManager.Instance.Success(successSymbol);
                    score.IncreaseScore();
                }
                else
                {
                    score.DecayScore();
                }

                scoreDisplayCastor.text = score.score + "%";
                scoreDisplayPollux.text = score.score + "%";

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
                foreach(Slot s in SlotManager.Instance.slots)
                {
                    s.ResetToNextTurn();
                }

                SlotManager.Instance.cardDown[0] = false;
                SlotManager.Instance.cardDown[1] = false;

                state = GameState.WAIT_BOTH;
                break;

            case GameState.END:
                Debug.LogWarning("Game ended !");
                break;
        }
    }

    IEnumerator SwitchStateFeedback()
    {
        coroutineFeedback = true;
        yield return new WaitForSeconds(3f);
        state = GameState.FEEDBACK;
        coroutineFeedback = false;
    }
}
