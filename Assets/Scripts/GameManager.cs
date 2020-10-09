using System.Collections;
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
    public ObjectiveSet[] objectiveSets;

    private void Start()
    {
        objectiveSets = new ObjectiveSet[2];
        for(int i = 0; i<2; i++)
        {
            objectiveSets[i] = ObjectiveManager.Instance.pickRandom();
        }

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
                    Debug.Log("Game state switch to WAIT_POLLUX");
                }
                else if (SlotManager.Instance.cardDown[(int)PlayerName.POLLUX])
                {
                    state = GameState.WAIT_CASTOR;
                    Debug.Log("Game state switch to WAIT_CASTOR");
                }
                break;

            case GameState.WAIT_CASTOR:
                if (SlotManager.Instance.cardDown[(int)PlayerName.CASTOR])
                {
                    state = GameState.FEEDBACK;
                    Debug.Log("Game state switch to FEEDBACK");
                }
                break;

            case GameState.WAIT_POLLUX:
                if (SlotManager.Instance.cardDown[(int)PlayerName.POLLUX])
                {
                    state = GameState.FEEDBACK;
                    Debug.Log("Game state switch to FEEDBACK");
                }
                break;

            case GameState.FEEDBACK:
                state = GameState.RESET;
                Debug.Log("Game state switch to RESET");
                break;

            case GameState.RESET:
                foreach(SlotAgent sa in SlotManager.Instance.slotAgents)
                {
                    if(sa.state != SlotState.EMPTY && !sa.locked)
                    {
                        sa.locked = true;
                        sa.state = SlotState.EMPTY;
                    }
                    else if(sa.state == SlotState.EMPTY && sa.locked)
                    {
                        sa.locked = false;
                    }
                }

                //Reset cardDown array
                SlotManager.Instance.cardDown[0] = false;
                SlotManager.Instance.cardDown[1] = false;

                state = GameState.WAIT_BOTH;
                Debug.Log("Game state switch to WAIT_BOTH");
                break;
        }
    }
}
