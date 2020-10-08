using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Singleton
    public static InputManager Instance
    {
        get
        {
            if (_instance != null)
                return _instance;

            InputManager[] managers = FindObjectsOfType(typeof(InputManager)) as InputManager[];
            if (managers.Length == 0)
            {
                Debug.LogWarning("InputManager not present on the scene. Creating a new one.");
                InputManager manager = new GameObject("Game Manager").AddComponent<InputManager>();
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
                Debug.LogError("You can only use one InputManager. Destroying the new one attached to the GameObject " + value.gameObject.name);
                Destroy(value);
            }
        }
    }
    private static InputManager _instance = null;
    #endregion

    [Serializable]
    public struct UIDMapping
    {
        public string uid;
        public Player player;
        public Card card;
    }
    public UIDMapping[] uidMap;

    public Dictionary<string, Player> playerMapping;
    public Dictionary<string, Card> cardMapping;



    private void Start()
    {
        playerMapping = new Dictionary<string, Player>();
        cardMapping = new Dictionary<string, Card>();

        foreach(UIDMapping entry in uidMap)
        {
            playerMapping.Add(entry.uid, entry.player);
            cardMapping.Add(entry.uid, entry.card);
        }
    }

    public static SlotState GetReader(NFCReader reader)
    {
        SlotState output = SlotState.EMPTY;
        switch(reader)
        {
            case NFCReader.ALPHA:

                break;

            case NFCReader.BETA:

                break;

            case NFCReader.GAMMA:

                break;

            case NFCReader.DELTA:

                break;

            case NFCReader.EPSILON:

                break;

            case NFCReader.DZETA:

                break;
        }
        return output;
    }
}
