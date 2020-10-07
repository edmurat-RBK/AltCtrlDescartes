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

    public void Trigger(string uid)
    {
        string strPlayer;
        string strCard;

        Player outPlayer;
        playerMapping.TryGetValue(uid, out outPlayer);
        
        switch (outPlayer) 
        {
            case Player.LEFT:
                strPlayer = "left";
                break;

            case Player.RIGHT:
                strPlayer = "right";
                break;

            default:
                strPlayer = "UNKNOWN";
                break;
        }
        

        Card outCard;
        cardMapping.TryGetValue(uid, out outCard);
        
        switch (outCard)
        {
            case Card.TRIANGLE:
                strCard = "triangle";
                break;

            case Card.SQUARE:
                strCard = "square";
                break;

            case Card.CIRCLE:
                strCard = "circle";
                break;

            default:
                strCard = "UNKNOWN";
                break;
        }
        

        Debug.Log("The " + strPlayer + " player has played " + strCard + " card");


    }
}
