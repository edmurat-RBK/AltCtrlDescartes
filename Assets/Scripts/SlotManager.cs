﻿using System.Collections;
using System.Collections.Generic;
using Uduino;
using UnityEngine;

public class SlotManager : MonoBehaviour
{
    #region Singleton
    public static SlotManager Instance
    {
        get
        {
            if (_instance != null)
                return _instance;

            SlotManager[] managers = FindObjectsOfType(typeof(SlotManager)) as SlotManager[];
            if (managers.Length == 0)
            {
                Debug.LogWarning("SlotManager not present on the scene. Creating a new one.");
                SlotManager manager = new GameObject("Slot Manager").AddComponent<SlotManager>();
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
                Debug.LogError("You can only use one SlotManager. Destroying the new one attached to the GameObject " + value.gameObject.name);
                Destroy(value);
            }
        }
    }
    private static SlotManager _instance = null;
    #endregion

    public SlotAgent[] slotAgents;
    public bool[] cardDown;

    private void Awake()
    {
        slotAgents = new SlotAgent[6];
        for(int i = 0; i<6; i++)
        {
            slotAgents[i] = new SlotAgent();
        }

        cardDown = new bool[2] { false, false };
    }

    public void GetData(string data, UduinoDevice board)
    {
        string[] dataSplit = data.Split(':');
        string reader = dataSplit[0]; 
        string uid = dataSplit[1];

        //Debug.Log("Data received from Arduino\n"+data);

        if(GameManager.Instance.state == GameState.WAIT_BOTH || GameManager.Instance.state == GameState.WAIT_CASTOR || GameManager.Instance.state == GameState.WAIT_POLLUX)
        {
            if (!cardDown[(int)PlayerName.CASTOR])
            {
                if (reader.Equals("ALPHA") && !slotAgents[(int)SlotName.ALPHA].locked)
                {
                    slotAgents[(int)SlotName.ALPHA].Update(uid);
                    cardDown[(int)PlayerName.CASTOR] = true;
                }
                else if (reader.Equals("BETA") && !slotAgents[(int)SlotName.BETA].locked)
                {
                    slotAgents[(int)SlotName.BETA].Update(uid);
                    cardDown[(int)PlayerName.CASTOR] = true;
                }
                else if (reader.Equals("GAMMA") && !slotAgents[(int)SlotName.GAMMA].locked)
                {
                    slotAgents[(int)SlotName.GAMMA].Update(uid);
                    cardDown[(int)PlayerName.CASTOR] = true;
                }
            }
            
            if (!cardDown[(int)PlayerName.POLLUX])
            {
                if (reader.Equals("DELTA") && !slotAgents[(int)SlotName.DELTA].locked)
                {
                    slotAgents[(int)SlotName.DELTA].Update(uid);
                    cardDown[(int)PlayerName.POLLUX] = true;
                }
                else if (reader.Equals("EPSILON") && !slotAgents[(int)SlotName.EPSILON].locked)
                {
                    slotAgents[(int)SlotName.EPSILON].Update(uid);
                    cardDown[(int)PlayerName.POLLUX] = true;
                }
                else if (reader.Equals("DZETA") && !slotAgents[(int)SlotName.DZETA].locked)
                {
                    slotAgents[(int)SlotName.DZETA].Update(uid);
                    cardDown[(int)PlayerName.POLLUX] = true;
                }
            }
        }
    }

    public bool CheckSuccess(out CardSymbol symbol)
    {
        for(var i=0; i<3; i++)
        {
            if(slotAgents[i].state != SlotState.EMPTY && slotAgents[i].state == slotAgents[i+3].state)
            {
                switch(slotAgents[i].state)
                {
                    case SlotState.PINK:
                        symbol = CardSymbol.PINK;
                        break;

                    case SlotState.BLUE:
                        symbol = CardSymbol.BLUE;
                        break;

                    case SlotState.ORANGE:
                        symbol = CardSymbol.ORANGE;
                        break;

                    default:
                        throw new System.Exception();
                }
                return true;
            }
        }
        symbol = CardSymbol.PINK;
        return false;
    }

    public void SendFakeInput(SlotName slot, PlayerName player, CardSymbol symbol)
    {
        string send = "";
        switch (slot)
        {
            case SlotName.ALPHA:
                send += "ALPHA";
                break;

            case SlotName.BETA:
                send += "BETA";
                break;

            case SlotName.GAMMA:
                send += "GAMMA";
                break;

            case SlotName.DELTA:
                send += "DELTA";
                break;

            case SlotName.EPSILON:
                send += "EPSILON";
                break;

            case SlotName.DZETA:
                send += "DZETA";
                break;
        }

        send += ":";

        if(player == PlayerName.CASTOR)
        {
            switch (symbol)
            {
                case CardSymbol.PINK:
                    send += "B045D232";
                    break;

                case CardSymbol.BLUE:
                    send += "E42A772A";
                    break;

                case CardSymbol.ORANGE:
                    send += "020BF434";
                    break;
            }
        }
        else if(player == PlayerName.POLLUX)
        {
            switch (symbol)
            {
                case CardSymbol.PINK:
                    send += "14F4CD2B";
                    break;

                case CardSymbol.BLUE:
                    send += "F2EDD433";
                    break;

                case CardSymbol.ORANGE:
                    send += "D0DCFA32";
                    break;
            }
        }

        GetData(send, null);
    }
}
