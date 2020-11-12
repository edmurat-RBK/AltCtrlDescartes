using System.Collections;
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

    public Slot[] slots;
    public bool[] cardDown;

    [Space(10)]
    #region graphs
    public Sprite emptySlotSprite;
    public Sprite lockedSlotSprite;
    public Sprite pinkSlotSprite;
    public Sprite blueSlotSprite;
    public Sprite orangeSlotSprite;
    #endregion
    private void Awake()
    {
        slots = new Slot[6];
        for(int i = 0; i<6; i++)
        {
            slots[i] = new Slot();
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
                if (reader.Equals("ALPHA") && !slots[(int)SlotName.ALPHA].locked)
                {
                    slots[(int)SlotName.ALPHA].Update(uid);
                    cardDown[(int)PlayerName.CASTOR] = true;
                }
                else if (reader.Equals("BETA") && !slots[(int)SlotName.BETA].locked)
                {
                    slots[(int)SlotName.BETA].Update(uid);
                    cardDown[(int)PlayerName.CASTOR] = true;
                }
                else if (reader.Equals("GAMMA") && !slots[(int)SlotName.GAMMA].locked)
                {
                    slots[(int)SlotName.GAMMA].Update(uid);
                    cardDown[(int)PlayerName.CASTOR] = true;
                }
            }
            
            if (!cardDown[(int)PlayerName.POLLUX])
            {
                if (reader.Equals("DELTA") && !slots[(int)SlotName.DELTA].locked)
                {
                    slots[(int)SlotName.DELTA].Update(uid);
                    cardDown[(int)PlayerName.POLLUX] = true;
                }
                else if (reader.Equals("EPSILON") && !slots[(int)SlotName.EPSILON].locked)
                {
                    slots[(int)SlotName.EPSILON].Update(uid);
                    cardDown[(int)PlayerName.POLLUX] = true;
                }
                else if (reader.Equals("DZETA") && !slots[(int)SlotName.DZETA].locked)
                {
                    slots[(int)SlotName.DZETA].Update(uid);
                    cardDown[(int)PlayerName.POLLUX] = true;
                }
            }
        }
    }

    public bool CheckFullSuccess(out CardSymbol symbol)
    {
        for(var i=0; i<3; i++)
        {
            if(slots[i].state != SlotState.EMPTY && slots[i].state == slots[i+3].state)
            {
                switch(slots[i].state)
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

    public bool CheckSymbolSuccess(out CardSymbol symbol)
    {
        for (var i = 0; i < 3; i++)
        {
            if (slots[i].state != SlotState.EMPTY)
            {
                for(int j = 3; j < 6; j++)
                {
                    if(slots[i].state == slots[j].state)
                    {
                        switch (slots[i].state)
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
            }
        }
        symbol = CardSymbol.PINK;
        return false;
    }

    public bool CheckSlotSuccess(out int slotIndex)
    {
        for (var i = 0; i < 3; i++)
        {
            if (slots[i].state != SlotState.EMPTY && slots[i+3].state != SlotState.EMPTY)
            {
                slotIndex = i;
                return true;
            }
        }
        slotIndex = -1;
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
