using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;
using System.ComponentModel;
using System.Linq;

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
    
    [ReadOnly(true)]
    public string lastUID;

    public string[] whitelistUID;

    public void GetArduinoSerial(string data, UduinoDevice board)
    {
        string device;
        string reader;
        string uid;

        device = data.Split(':')[0];
        reader = data.Split(':')[1];
        uid = data.Split(':')[2];

        if(whitelistUID.Contains(uid))
        {
            Debug.Log("\nDevice : " + device + "\nReader : " + reader + "\nUID : " + uid);
        }
        else
        {
            Debug.LogWarning(data + " is not whitelisted");
        }
    }
}
