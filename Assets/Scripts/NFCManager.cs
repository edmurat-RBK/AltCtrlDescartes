using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NFCManager : MonoBehaviour
{
    #region Singleton
    public static NFCManager Instance
    {
        get
        {
            if (_instance != null)
                return _instance;

            NFCManager[] managers = FindObjectsOfType(typeof(NFCManager)) as NFCManager[];
            if (managers.Length == 0)
            {
                Debug.LogWarning("NFCManager not present on the scene. Creating a new one.");
                NFCManager manager = new GameObject("NFC Manager").AddComponent<NFCManager>();
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
                Debug.LogError("You can only use one NFCManager. Destroying the new one attached to the GameObject " + value.gameObject.name);
                Destroy(value);
            }
        }
    }
    private static NFCManager _instance = null;
    #endregion
    
    [Serializable]
    public class EncyclopediaEntry
    {
        public string uid;
        public PlayerName player;
        public CardSymbol card;
    }
    public EncyclopediaEntry[] modifiableEncyclopedia;

    public Dictionary<string, EncyclopediaEntry> encyclopedia;

    private void Start()
    {
        encyclopedia = new Dictionary<string, EncyclopediaEntry>();
        foreach(EncyclopediaEntry ee in modifiableEncyclopedia)
        {
            encyclopedia.Add(ee.uid, ee);
        }
    }
}
